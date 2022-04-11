namespace Application.Features.StudentFeatures.Queries.GetStudents
{
    public class StudentsVm
    {
        protected StudentsVm()
        {
            Students = new List<StudentDto>();
        }

        public StudentsVm(IList<StudentDto> students)
            : this()
        {
            Students = students;
        }

        public IList<StudentDto> Students { get; private set; }
    }
}