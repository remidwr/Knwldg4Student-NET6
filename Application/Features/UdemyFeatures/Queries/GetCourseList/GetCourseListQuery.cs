using Application.Common.ExternalApi.UdemyApi;

namespace Application.Features.UdemyFeatures.Queries.GetCourseList
{
    public class GetCourseListQuery : IRequest<CourseListDto>
    {
        public GetCourseListQuery(int pageNumber, int pageSize, string? search, string? ordering)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Search = search;
            Ordering = ordering;
        }

        public string? Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Ordering { get; set; }
    }

    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, CourseListDto>
    {
        private readonly IMapper _mapper;
        private readonly IUdemyApi _udemyApi;

        public GetCourseListQueryHandler(IMapper mapper,
            IUdemyApi udemyApi)
        {
            _mapper = mapper;
            _udemyApi = udemyApi;
        }

        public async Task<CourseListDto> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = await _udemyApi.GetCourseAsync(request.Search, request.PageNumber, request.PageSize, request.Ordering);
            var courseDto = _mapper.Map<CourseListDto>(courses);

            return courseDto;
        }
    }
}