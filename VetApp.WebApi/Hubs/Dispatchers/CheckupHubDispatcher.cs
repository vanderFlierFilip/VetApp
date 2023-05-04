using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Notifications;

namespace VDMJasminka.WebApi.Hubs.Dispatchers
{
    public class CheckupHubDispatcher : INotificationHandler<CheckupSuccessfullyCompletedNotification>
    {
        private readonly IHubContext<CheckupHub> _hubContext;

        public CheckupHubDispatcher(IHubContext<CheckupHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(CheckupSuccessfullyCompletedNotification notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("RecieveMessage", notification.OwnerName + " " + notification.PetName, cancellationToken);
        }
    }
}