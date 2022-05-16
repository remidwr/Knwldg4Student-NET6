namespace Application.Features.StudentFeatures.Commands.CreateStudent
{
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public CreateStudentCommandValidator(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;

            RuleFor(s => s.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.");

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");

            RuleFor(s => s.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(320).WithMessage("Email must not exceed 320 characters.")
                .EmailAddress().WithMessage("It must be an email.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _studentRepository.HasUniqueEmailAsync(email);
        }
    }
}