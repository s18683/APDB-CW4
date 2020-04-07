using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExV2.Services;

namespace ExV2.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDbService service)
        {
            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string bodyS = "";
                string method = httpContext.Request.Method.ToString();
                string queryString = httpContext.Request?.QueryString.ToString();
                string path = httpContext.Request.Path; //"api/students"
                
                

                using (StreamReader reader
                 = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyS = await reader.ReadToEndAsync();
                }

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "requestsLog.txt"),true))
                {
                    outputFile.WriteLine("Metoda: " + method + "\nSciezka: " + path + "\nBody: " + bodyS + "\nQueryString: " + queryString);
                    
                }
               

            }

            await _next(httpContext);
        }
    }
}