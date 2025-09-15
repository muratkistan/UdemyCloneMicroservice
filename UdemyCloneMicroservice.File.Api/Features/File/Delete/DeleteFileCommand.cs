using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}