namespace Domain.AggregatesModel.StudentAggregate
{
    public interface IStudentRepository
        : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int id);

        Student Add(Student student);

        Student Update(Student student);

        Task<bool> HasUniqueEmailAsync(string email);

        Rating AddRating(Rating rating);
    }
}