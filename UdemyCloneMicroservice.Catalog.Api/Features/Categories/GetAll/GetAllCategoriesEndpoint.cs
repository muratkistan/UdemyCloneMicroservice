using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyCloneMicroservice.Catalog.Api.Features.Categories.Dtos;
using UdemyCloneMicroservice.Catalog.Api.Repositories;
using UdemyCloneMicroservice.Shared;
using UdemyCloneMicroservice.Shared.Extensions;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoriesQuery : IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryQueryHandler
        : IRequestHandler<GetAllCategoriesQuery, ServiceResult<List<CategoryDto>>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync(cancellationToken: cancellationToken);
            var categoriesAsDto = _mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
        }
    }

    public static class GetAllCategoriesEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/",
                    async (IMediator mediator) =>
                        (await mediator.Send(new GetAllCategoriesQuery())).ToGenericResult())
                .WithName("GetAllCategory")
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}