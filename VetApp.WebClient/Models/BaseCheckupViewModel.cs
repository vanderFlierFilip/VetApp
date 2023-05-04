using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.WebClient.Models
{
    public class BaseCheckupViewModel
    {
        public int OwnerId { get; set; }
        public int PetId { get; set; }
        public double? PaidSum { get; set; }
        public string CheckupType { get; set; }
        public CreateOwnerDto Owner { get; set; }
        public PetDto Pet { get; set; }
        public CheckupDetailViewModel CheckupDetailViewModel { get; set; }

    }


}
