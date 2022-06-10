using Application.Common.ExternalApi.UdemyApi;

namespace Infrastructure.ExternalApi.UdemyApi
{
    public class UdemyApi : IUdemyApi
    {
        private readonly UdemyClient _client;

        public UdemyApi(UdemyClient client)
        {
            _client = client;
        }

        public async Task<CourseListResponse> GetCourseAsync(string search, int pageNumber, int pageSize, string ordering)
        {
            return await _client.GetCoursesAsync(search, pageNumber, pageSize, ordering);
        }
    }
}