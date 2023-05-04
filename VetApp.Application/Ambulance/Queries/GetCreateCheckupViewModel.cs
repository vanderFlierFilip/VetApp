using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetCreateCheckupViewModel : IRequest<CreateCheckupViewModel>
    {
        public GetCreateCheckupViewModel(int ownerId, int petId)
        {
            OwnerId = ownerId;
            PetId = petId;
        }

        public int OwnerId { get; }
        public int PetId { get; }
    }
}