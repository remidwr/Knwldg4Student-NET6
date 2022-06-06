using Application.Common.Identity;

namespace Application.Features.MeetingFeatures.Queries.GetMeetings
{
    public record GetMeetingsQuery() : IRequest<MeetingsVm>;

    public class GetMeetingsFromStudentIdQueryHandler : IRequestHandler<GetMeetingsQuery, MeetingsVm>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IStudentRepository _studentRepository;

        public GetMeetingsFromStudentIdQueryHandler(IMeetingRepository meetingRepository,
            IMapper mapper,
            IIdentityService identityService,
            IStudentRepository studentRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        public async Task<MeetingsVm> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
        {
            var studentId = _identityService.GetUserId();
            var student = await _studentRepository.GetByIdAsync(studentId);
            var meetings = await _meetingRepository.GetMeetingsByStudentIdAsync(student.Id/*request.Id*/);
            var meetingDtos = _mapper.Map<List<MeetingDto>>(meetings);

            return new MeetingsVm(meetingDtos);
        }
    }
}