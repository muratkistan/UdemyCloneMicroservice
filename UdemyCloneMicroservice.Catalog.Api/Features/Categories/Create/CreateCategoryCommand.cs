using MediatR;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
}