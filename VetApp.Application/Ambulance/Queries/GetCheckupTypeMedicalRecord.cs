using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.Requests;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetCheckupTypeMedicalRecord : IRequest<List<CheckupDto>>
    {
        public GetCheckupTypeMedicalRecord(int petId, MedicalRecordsParameters pararmeters)
        {
            PetId = petId;
            Pararmeters = pararmeters;
        }

        public int PetId { get; }
        public MedicalRecordsParameters Pararmeters { get; }
    }
}