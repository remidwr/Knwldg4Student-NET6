namespace Domain.AggregatesModel.SectionAggregate
{
    public interface ISectionRepository
        : IRepository<Section>
    {
        Task<IEnumerable<Section>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Section> GetSectionByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}