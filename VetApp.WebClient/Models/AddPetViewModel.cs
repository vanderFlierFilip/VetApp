using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VDMJasminka.WebClient.Models
{
    public class AddPetViewModel
    {
        public int OwnerId { get; set; }
        public PetModel Pet { get; set; }
        public List<SelectListItem> AnimalSexOptions { get; set; }
        public List<SelectListItem> Breeds { get; set; }
        public List<SelectListItem> SpeciesList { get; set; }
    }
}