using AutoMapper;
using MediatR;
using System.Net;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Dtos;
using UdemyCloneMicroservice.Catalog.Api.Repositories;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Extensions;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await _context.Categories.FindAsync(request.Id, cancellationToken);
            if (hasCategory == null)
            {
                return (ServiceResult<CategoryDto>)ServiceResult<CategoryDto>.Error("Category not found",
                    $"The category with id({request.Id}) was not found", HttpStatusCode.NotFound);
            }
            var categoryAsDto = _mapper.Map<CategoryDto>(hasCategory);
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}",
                    async (IMediator mediator, Guid id) =>
                        (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult())
                .WithName("GetByIdCategory");

            return group;
        }
    }
}