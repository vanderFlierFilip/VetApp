using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.WebClient.Models
{
    public class EditPetModalViewModel
    {
        public PetDto Pet { get; set; }
        public int OwnerId { get; set; }
        public List<SelectListItem> SpeciesList { get; set; }
        public List<SelectListItem> Breeds { get; set; }
        public List<string> AnimalSexList => new List<string> { "M", "Ž" };
        public List<SelectListItem> AnimalSexOptions => AnimalSexList.Select(s => new SelectListItem { Text = s, Value = s }).ToList();
    }
}
