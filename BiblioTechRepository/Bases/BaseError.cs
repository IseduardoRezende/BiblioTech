using BiblioTechDomain.Enums;

namespace BiblioTechDomain.Bases
{
    public class BaseError
    {
        public BaseError(string field, object? value, Error error)
        {
            Field = field;
            Value = value;
            Error = Enum.GetName(error)!;
        }

        public string Field { get; set; }

        public object? Value { get; set; }

        public string Error { get; set; }
    }
}
