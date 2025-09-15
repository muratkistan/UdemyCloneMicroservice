namespace UdemyCloneMicroservice.File.Api.Features.File.Upload
{
    public record UploadFileCommandResponse(string FileName, string FilePath, string OriginalFileName);
}