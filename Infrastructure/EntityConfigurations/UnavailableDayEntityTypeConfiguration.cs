namespace Infrastructure.EntityConfigurations
{
    public class UnavailableDayEntityTypeConfiguration
        : IEntityTypeConfiguration<UnavailableDay>
    {
        public void Configure(EntityTypeBuilder<UnavailableDay> unavailableDayConfiguration)
        {
            unavailableDayConfiguration.ToTable("UnavailableDays");

            unavailableDayConfiguration.HasKey(ud => ud.Id);

            unavailableDayConfiguration.HasIndex(ud => new { ud.DayOff, ud.StudentId }, "UK_Table_DayOff_StudentId")
                .IsUnique();

            unavailableDayConfiguration.Property(ud => ud.DayOff)
                .HasColumnType("date");

            unavailableDayConfiguration.Property(ud => ud.Recursive)
                .HasDefaultValue(false)
                .IsRequired();

            unavailableDayConfiguration.Property(ud => ud.Interval)
                .HasDefaultValue(0)
                .IsRequired();

            unavailableDayConfiguration.HasOne(ud => ud.Student)
                .WithMany(s => s.UnavailableDays)
                .HasForeignKey(ud => ud.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnavailableDays_Students");
        }
    }
}