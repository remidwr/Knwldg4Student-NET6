namespace Application.Common.ExternalApi.UdemyApi
{
    public interface IUdemyApi
    {
        Task<CourseListResponse> GetCourseAsync(string search, int pageNumber, int pageSize, string ordering);
    }
}