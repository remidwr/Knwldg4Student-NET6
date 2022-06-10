using Application.Common.ExternalApi.UdemyApi;

namespace Application.Features.UdemyFeatures.Queries.GetCourseList
{
    public class CourseListDto : IMapFrom<CourseListResponse>
    {
        public long Count { get; set; }
        public Uri Next { get; set; }
        public object Previous { get; set; }
        public List<CourseListResultDto> Results { get; set; }
    }

    public class CourseListResultDto : IMapFrom<Result>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Uri Image240X135 { get; set; }
        public string Headline { get; set; }
    }
}