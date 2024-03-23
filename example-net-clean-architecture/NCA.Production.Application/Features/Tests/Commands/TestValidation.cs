using NCA.Common.Application.Validators;

namespace NCA.Production.Application.Features.Tests.Commands
{
    public class TestValidation
    {
        public class Command : IRequest<Unit>
        {
            public string Parameter1 { get; set; } = null!;
            public string Parameter2 { get; set; } = null!;
            public string Parameter3 { get; set; } = null!;
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            public CommandHandler() { }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                return Task.FromResult(Unit.Value);
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            // csharpier-ignore
            public CommandValidator()
            {
                RuleFor(p => p.Parameter1)
                    .NotNull().WithMessage("Cannot be null.")
                    .NotEmpty().WithMessage("Cannot be blank.")
                    ;

                RuleFor(p => p.Parameter2)
                    .NotNull().WithMessage("Cannot be null.").WithErrorCode("ParameterNotNull")
                    .NotEmpty().WithMessage("Cannot be blank.").WithErrorCode("ParameterNotBlank")
                    ;

                RuleFor(p => p.Parameter3)
                    .NotNull().WithError(new Error("ParameterNotNull", "Cannot be null."))
                    .NotEmpty().WithError(new Error("ParameterNotNull", "Cannot be null."))
                    ;
            }
        }
    }
}
