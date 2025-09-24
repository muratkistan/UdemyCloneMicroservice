using Asp.Versioning.Builder;
using UdemyCloneMicroservice.File.Api.Features.File.Delete;
using UdemyCloneMicroservice.File.Api.Features.File.Upload;

namespace UdemyCloneMicroservice.File.Api.Features
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("files").WithApiVersionSet(apiVersionSet).
                UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint().RequireAuthorization();
        }
    }
}