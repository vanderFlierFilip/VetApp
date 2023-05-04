using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDMJasminka.WebClient.Models
{
    public class MedicamentModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Uom { get; set; }
        public double Price { get; set; }
        public short MinimalAmount { get; set; }
        public bool Material { get; set; }
    }
}