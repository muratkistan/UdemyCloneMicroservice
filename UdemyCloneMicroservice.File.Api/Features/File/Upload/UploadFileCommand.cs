using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;
}