using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCoreAuthDemo.Extensions
{
    internal class EnvironmentDisplay
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;

        public EnvironmentDisplay(RequestDelegate next, IHostingEnvironment env, IConfiguration config)
        {
            _next = next;
            _env = env;
            _config = config;
        }
        
        public string EnvironmentName
        {
            get
            {
                return _env.EnvironmentName;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _config.GetValue<bool>("EnvironmentDisplay", false);
            }
        }

        public async Task Invoke(HttpContext context)
        {
            if (!IsEnabled)
            {
                await _next(context);
                return;
            }

            string NewHeadContent = AddHead();
            string NewBodyContent = AddBody();
            var ExistingBody = context.Response.Body;

            using (MemoryStream NewBody = new MemoryStream())
            {
                context.Response.Body = NewBody;
                await _next(context);
                context.Response.Body = ExistingBody;

                if (!context.Response.ContentType.StartsWith("text/html"))
                {
                    await context.Response.WriteAsync(new StreamReader(NewBody).ReadToEnd());
                    return;
                }

                NewBody.Seek(0, SeekOrigin.Begin);
                string NewContent = new StreamReader(NewBody).ReadToEnd();
                NewContent = NewContent
                    .Replace("</head>", NewHeadContent + "</head>")
                    .Replace("</body>", NewBodyContent + "</head>");
                await context.Response.WriteAsync(NewContent);
            }
        }

        private string AddHead()
        {
            return 
                @"<style>
                    .AspNetEnv { background-color: red; }
                    .ApsNetEnv_Development { background-color: yellow; }
                    .ApsNetEnv_Staging { background-color: blue; }
                    .ApsNetEnv_Production { background-color: green; }
                </style>";
        }

        private string AddBody()
        {
            return
                $"<div class=\"AspNetEnv AspNetEnv_{_env.EnvironmentName}\" id=\"AspNetEnvIndicator\">EnvironmentDiv</div>";
        }
    }
}