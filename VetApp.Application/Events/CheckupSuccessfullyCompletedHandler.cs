using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Notifications;
using VDMJasminka.Core.Ambulance.Events;

namespace VDMJasminka.Application.Events
{
    public class CheckupSuccessfullyCompletedHandler : INotificationHandler<PetCheckupSuccessfullyCompleted>
    {
        private readonly IMediator _mediator;

        public CheckupSuccessfullyCompletedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(PetCheckupSuccessfullyCompleted notification, CancellationToken cancellationToken)
        {
            // send email with report
            //-------
            await _mediator.Publish(new CheckupSuccessfullyCompletedNotification(notification.Owner.OwnerInformation.FullName, notification.Pet.PetInformation.Name), cancellationToken);
        }
    }
}