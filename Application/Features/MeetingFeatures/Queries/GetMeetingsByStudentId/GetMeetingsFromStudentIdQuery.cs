namespace Application.Features.MeetingFeatures.Queries.GetMeetingsByStudentId
{
    public class GetMeetingsFromStudentIdQuery : IRequest<MeetingsVm>
    {
        public int Id { get; set; }
    }

    public class GetMeetingsFromStudentIdQueryHandler : IRequestHandler<GetMeetingsFromStudentIdQuery, MeetingsVm>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public GetMeetingsFromStudentIdQueryHandler(IMeetingRepository meetingRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _meetingRepository = meetingRepository;
        }

        public async Task<MeetingsVm> Handle(GetMeetingsFromStudentIdQuery request, CancellationToken cancellationToken)
        {
            var meetings = await _meetingRepository.GetMeetingsByStudentIdAsync(request.Id);
            var meetingDtos = _mapper.Map<List<MeetingDto>>(meetings);

            return new MeetingsVm(meetingDtos);
        }
    }
}