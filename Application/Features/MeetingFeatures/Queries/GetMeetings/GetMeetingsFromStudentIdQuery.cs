using Application.Common.Identity;

namespace Application.Features.MeetingFeatures.Queries.GetMeetings
{
    public record GetMeetingsQuery() : IRequest<MeetingsVm>;

    public class GetMeetingsFromStudentIdQueryHandler : IRequestHandler<GetMeetingsQuery, MeetingsVm>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetMeetingsFromStudentIdQueryHandler(IMeetingRepository meetingRepository,
            IMapper mapper,
            IIdentityService identityService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<MeetingsVm> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
        {
            var studentId = _identityService.GetUserId();
            var meetings = await _meetingRepository.GetMeetingsByStudentIdAsync(/*request.Id*/1);
            var meetingDtos = _mapper.Map<List<MeetingDto>>(meetings);

            return new MeetingsVm(meetingDtos);
        }
    }
}