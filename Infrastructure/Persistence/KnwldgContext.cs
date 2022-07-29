using Infrastructure.EntityConfigurations;

namespace Infrastructure.Persistence
{
    public partial class KnwldgContext : DbContext, IUnitOfWork
    {
        public KnwldgContext()
        {
        }

        public KnwldgContext(DbContextOptions<KnwldgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<StudentMeeting> StudentMeetings { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<UnavailableDay> UnavailableDays { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("French_CI_AI");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeetingStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RatingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SectionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentMeetingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UnavailableDayEntityTypeConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}