namespace Application.Features.MeetingFeatures.Queries.GetMeetingById
{
    public record GetMeetingByIdQuery(int Id) : IRequest<MeetingDetailedDto>;

    public class GetMeetingByIdRequestHandler : IRequestHandler<GetMeetingByIdQuery, MeetingDetailedDto>
    {
        private readonly IMapper _mapper;
        private readonly IMeetingRepository _meetingRepository;

        public GetMeetingByIdRequestHandler(IMapper mapper,
            IMeetingRepository meetingRepository)
        {
            _mapper = mapper;
            _meetingRepository = meetingRepository;
        }

        public async Task<MeetingDetailedDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetByIdAsync(request.Id);

            if (meeting == null)
            {
                throw new NotFoundException(nameof(Meeting), request.Id);
            }

            return _mapper.Map<MeetingDetailedDto>(meeting);
        }
    }
}