using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class PendingCheckupDto
    {
        public ReadOwnerDto Owner { get; set; }
        public string PetName { get; set; }
        public string VaccineType { get; set; }
        public DateTime Date { get; set; }
    }
}