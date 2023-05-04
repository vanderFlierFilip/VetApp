using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VDMJasminka.WebClient.Models
{
    public class PetMultiFormModel
    {
        public PetMultiFormModel()
        {
            PetModels = new List<PetMultiFormModel>();
        }

        public List<SelectListItem> SpeciesList { get; set; }
        public List<SelectListItem> AnimalSexOptions { get; set; }
        public PetModel Pet { get; set; }
        public List<PetMultiFormModel> PetModels { get; set; }
    }
}
