using GLPIService.Application;
using GLPIService.Application.Common.Interfaces;
using GLPIService.Filters;
using GLPIService.Infrastructure;
using GLPIService.WebApi.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO.Compression;

namespace GLPIService {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services) {

            services.AddApplication();
            services.AddInfrastructure(Configuration);


            // http response compression
            services.AddResponseCompression();

            // http response compression options
            services.Configure<BrotliCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddScoped<ApiExceptionFilter>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // add controllers
            services.AddHttpContextAccessor();
            services.AddControllers();

            // authentication
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.Configure<IISServerOptions>(options => {
                options.AutomaticAuthentication = true;
            });

            // authorization
            services.AddAuthorization(options => {
                options.AddPolicy("BasicUser", policy => {
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("AdminUser", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(@"GG_Gaia_AppAdmin", @"GG_Oitante_AppAdmin");
                });
            });


            // api versioning
            services.AddApiVersioning(x => {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });

            // add meta information for api versions
            services.AddVersionedApiExplorer(p => {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });


            services.AddSwaggerDocument(config => {
                config.PostProcess = document => {
                    document.Info.Version = "v202212.13";
                    document.Info.Title = "GLPI interface";
                    document.Info.Description = "Service to interact with GLPI.";
                    document.Info.Contact = new NSwag.OpenApiContact {
                        Name = "Nvno Gomes"
                    };
                };
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }
            app.UseHsts();

            // compression
            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseRouting();

            // swag
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }).RequireAuthorization("AdminUser");

                endpoints.MapControllers()
                    .RequireAuthorization("BasicUser");
            });
        }
    }
}
