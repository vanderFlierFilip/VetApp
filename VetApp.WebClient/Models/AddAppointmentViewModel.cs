using System;

namespace VDMJasminka.WebClient.Models
{
    public class AddAppointmentViewModel
    {
        public int? OwnerId { get; set; }
        public int? PetId { get; set; }
        public string AppointmentType { get; set; }
        public DateTime? Day { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Description { get; set; }
        public OwnerAndPetTypeaheadViewModel TypeaheadViewModel { get; set; }
    }
}
