using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using TypeEnum = BiblioTechDomain.Enums.Type;

namespace BiblioTechDomain.Extensions
{
    public static class Validation
    {
        public static bool IsValidEmail(this string email)
        {
            return MailAddress.TryCreate(email, out _);
        }

        public static bool IsValidPhone(this string? phone)
        {
            return phone == null || new PhoneAttribute().IsValid(phone);
        }

        public static bool IsValidType(this long typeId)
        {
            foreach (var value in Enum.GetValues(typeof(TypeEnum))) 
                if ((int)value == typeId) 
                    return true;                   

            return false;
        }       
    }
}
