using Kvantron.Utils.Logs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvantron.Dnn.ExecutionProvider;

/// <summary>
/// Класс для реализации сервера с поддержкой OpenAPI
/// </summary>
internal class Server
{
        public static HttpClient client = new HttpClient();
        public static Thread th;

        public static void Start()
        {
            th = new Thread(new ThreadStart(() =>
            {


                var builder = WebApplication.CreateBuilder();

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddRazorPages();
                builder.Services.AddLogging(c => c.ClearProviders());
                var app = builder.Build();
                //  app.Logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.None);

                app.Urls.Add("http://0.0.0.0:7002");
                // if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    Process.Start("explorer.exe", "http://localhost:7002/swagger/index.html");
                }

                app.UseStaticFiles();
                app.MapRazorPages();
                app.MapControllers();

                Logger.AddEvent(5001, "Запущен сервер Kestrel");
                app.RunAsync();
            }));

            th.Start();
            th.Name = "Kestrel Server";
        }
    }


