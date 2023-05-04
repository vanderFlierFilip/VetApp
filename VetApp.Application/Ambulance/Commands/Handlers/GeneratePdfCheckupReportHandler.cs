using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Common;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    internal sealed class GeneratePdfCheckupReportHandler : IRequestHandler<GeneratePdfCheckupReport, byte[]>
    {
        private readonly IPdfService _pdfService;
        private readonly IRepository<Owner> _ownerRepository;

        public GeneratePdfCheckupReportHandler(IPdfService pdfService, IRepository<Owner> ownerRepository)
        {
            _pdfService = pdfService;
            _ownerRepository = ownerRepository;
        }

        public async Task<byte[]> Handle(GeneratePdfCheckupReport request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);
            var pet = owner.GetPet(request.PetId);
            var checkups = pet.Checkups.Select(checkup => checkup.ToDto()).ToList();

            return await Task.FromResult(_pdfService.GenerateMedicalRecordPdf(request.Template, request.RecordType));
        }
    }
}