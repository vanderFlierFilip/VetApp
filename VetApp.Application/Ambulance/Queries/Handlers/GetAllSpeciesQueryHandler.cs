using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries.Handlers
{
    public class GetAllSpeciesQueryHandler : IRequestHandler<GetAllSpeciesQuery, IEnumerable<SpeciesDto>>
    {
        private readonly IRepository<Species> _speciesRepository;
        public GetAllSpeciesQueryHandler(IRepository<Species> speciesRepository)
        {
            _speciesRepository = speciesRepository;
        }
        public async Task<IEnumerable<SpeciesDto>> Handle(GetAllSpeciesQuery request, CancellationToken cancellationToken)
        {
            var modelResult = await _speciesRepository.GetAll();
            return modelResult.Select(mr => mr.ToDto());
        }
    }
}
