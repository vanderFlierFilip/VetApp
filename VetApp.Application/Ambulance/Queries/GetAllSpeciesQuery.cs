using MediatR;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetAllSpeciesQuery : IRequest<IEnumerable<SpeciesDto>>
    {
    }
}
