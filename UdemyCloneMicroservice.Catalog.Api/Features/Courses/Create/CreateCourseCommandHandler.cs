namespace UdemyCloneMicroservice.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await _context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found.",
                    $"The Category with id({request.CategoryId}) was not found", HttpStatusCode.NotFound);
            }

            var hasCourse = await _context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists.",
                    $"The Course with name({request.Name}) already exists", HttpStatusCode.BadRequest);
            }

            var newCourse = _mapper.Map<Course>(request);
            newCourse.Created = DateTime.Now;
            newCourse.Id = NewId.NextSequentialGuid(); // index performance
            newCourse.Feature = new Feature()
            {
                Duration = 10, // calculate by course video
                EducatorFullName = "Ahmet Yılmaz", // get by token payload
                Rating = 0
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
        }
    }
}