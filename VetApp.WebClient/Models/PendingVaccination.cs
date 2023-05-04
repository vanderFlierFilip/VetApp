using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDMJasminka.WebClient.Models
{
    public class PendingVaccination
    {
        public string OwnerName { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string PetName { get; set; }
        public string VaccineType { get; set; }
        public DateTime Date { get; set; }
    }
}