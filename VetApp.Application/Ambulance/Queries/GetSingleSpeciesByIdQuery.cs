using MediatR;
using VDMJasminka.Core;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetSingleSpeciesByIdQuery : IRequest<Species>
    {
        public GetSingleSpeciesByIdQuery(int speciesId)
        {
            SpeciesId = speciesId;
        }
        public int SpeciesId { get; }
    }
}
