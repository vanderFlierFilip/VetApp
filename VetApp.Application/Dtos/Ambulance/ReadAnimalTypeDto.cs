using System.Collections.Generic;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class ReadAnimalTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<ReadAnimalBreedDto> Breeds { get; set; }
    }
}
