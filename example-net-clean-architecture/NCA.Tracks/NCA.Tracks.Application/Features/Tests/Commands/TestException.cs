using MediatR;

namespace NCA.Production.Application.Features.Tests.Commands
{
    public class TestException
    {
        public class Command : IRequest<Unit> { }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            public CommandHandler() { }

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new Exception("Test Exception");
            }
        }
    }
}
