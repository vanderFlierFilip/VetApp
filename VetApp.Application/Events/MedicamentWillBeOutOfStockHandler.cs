using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.Events;

namespace VDMJasminka.Application.Events
{
    internal class MedicamentWillBeOutOfStockHandler : INotificationHandler<CheckupItemOutOfStock>
    {
        public Task Handle(CheckupItemOutOfStock notification, CancellationToken cancellationToken)
        {
            // Handle notification for out of stock event
            return Unit.Task;
        }
    }
}