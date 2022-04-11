namespace Application.Features.StudentFeatures.Queries.GetStudents
{
    public class StudentDto : IMapFrom<Student>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AverageRating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>()
                .ForMember(
                    dest => dest.AverageRating,
                    opt => opt.MapFrom(
                        x => x.Ratings.Count > 0
                        ? x.Ratings.Average(r => r.Stars)
                        : 0));
        }
    }
}