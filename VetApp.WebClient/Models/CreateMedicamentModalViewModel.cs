using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDMJasminka.WebClient.Models
{
    public class CreateMedicamentModalViewModel
    {
        public string ControllerName { get; set; }

        public MedicamentModel Medicament { get; set; }
        
    }
}