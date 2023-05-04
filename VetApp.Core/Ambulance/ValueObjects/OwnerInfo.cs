using System.Collections.Generic;
using System.Net.Mail;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.ValueObjects
{
    public class OwnerInfo : ValueObject
    {
        public OwnerInfo(string fullName,
                         string address,
                         string location,
                         string phoneNumber,
                         string municipality,
                         string sSN,
                         string additionalInfo,
                         string email)
        {
            FullName = fullName;
            Address = address;
            Location = location;
            PhoneNumber = phoneNumber;
            Municipality = municipality;
            SSN = sSN;
            AdditionalInfo = additionalInfo;
            Email = email;
        }

        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string Location { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Municipality { get; private set; }
        public string SSN { get; private set; }
        public string AdditionalInfo { get; private set; }
        public string Email { get; private set; }

        public void UpdateEmail(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                Email = email.Address;
            }
            catch
            {
                throw new InvalidEmailAddressException();
            }
        }

        public void UpdateLivingAddress(string address, string location, string municipality)
        {
            Address = address;
            Location = location;
            Municipality = municipality;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FullName;
            yield return Address;
            yield return Location;
            yield return PhoneNumber;
            yield return Municipality;
            yield return SSN;
            yield return AdditionalInfo;
            yield return Email;
        }
    }
}