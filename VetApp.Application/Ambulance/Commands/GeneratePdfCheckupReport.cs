using MediatR;
using System;
using VDMJasminka.Shared.Constants;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class GeneratePdfCheckupReport : IRequest<byte[]>
    {
        public int OwnerId { get; }
        public int PetId { get; }
        public string Template { get; }
        public MedicalRecordType RecordType { get; }
        public DateTime ToDate { get; }

        public GeneratePdfCheckupReport(int ownerId, int petId, string template, MedicalRecordType recordType, DateTime? toDate)
        {
            OwnerId = ownerId;
            PetId = petId;
            Template = template;
            RecordType = recordType;
            ToDate = toDate ?? DateTime.Now;
        }
    }
}
