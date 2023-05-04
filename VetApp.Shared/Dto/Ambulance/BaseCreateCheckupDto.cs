using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class BaseCreateCheckupDto
    {
        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int PetId { get; set; }

        public decimal PaidSum { get; set; }
        public DateTime? NextControlCheckup { get; set; }
        public List<CreateCheckupCheckupItemsDto> CheckupItems { get; set; }
    }

    public class CreateCheckupCheckupItemsDto
    {
        [Required]
        public int MedicamentId { get; set; }

        public double? Amount { get; set; }
        public string MedicamentApplication { get; set; }
        public double Price { get; set; }
        public string Uom { get; set; }
    }
}