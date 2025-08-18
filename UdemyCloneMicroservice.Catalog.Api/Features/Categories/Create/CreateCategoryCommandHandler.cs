using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyCloneMicroservice.Catalog.Api.Repositories;
using UdemyCloneMicroservice.Shared;

namespace UdemyCloneMicroservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        private readonly AppDbContext _context;

        public CreateCategoryCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _context.Categories
                .AnyAsync(c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase), cancellationToken);

            if (existingCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error("Category already exist", $"The category name '{request.Name}' already exists", HttpStatusCode.Conflict);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };
            await _context.Categories.AddAsync(category, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "");
        }
    }
}