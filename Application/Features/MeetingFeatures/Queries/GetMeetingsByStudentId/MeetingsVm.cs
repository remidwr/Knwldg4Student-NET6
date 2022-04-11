namespace Application.Features.MeetingFeatures.Queries.GetMeetingsByStudentId
{
    public class MeetingsVm
    {
        protected MeetingsVm()
        {
            Meetings = new List<MeetingDto>();
        }

        public MeetingsVm(List<MeetingDto> meetings)
            : this()
        {
            Meetings = meetings;
        }

        public IList<MeetingDto> Meetings { get; private set; }
    }
}