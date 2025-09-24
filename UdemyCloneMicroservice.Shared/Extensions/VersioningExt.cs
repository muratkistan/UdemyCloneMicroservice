using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Shared.Extensions
{
    public static class VersioningExt
    {
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                //options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(),
                //    new QueryStringApiVersionReader(), new UrlSegmentApiVersionReader());
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static ApiVersionSet AddVersionSetExt(this WebApplication app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .HasApiVersion(new ApiVersion(1, 2))
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();
            return apiVersionSet;
        }
    }
}