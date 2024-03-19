using BiblioTechData.Interfaces;
using BiblioTechDomain.Enums;
using System.Text.Json;

namespace BiblioTechDomain.Bases
{
    public class BaseFilter
    {
        public BaseFilter(string field, string value)
        {
            Field = field;
            Value = value;
        }

        public string Field { get; set; }

        public string Value { get; set; }

        public static IEnumerable<BaseFilter> ConvertToBaseFilter(string json)
        {
            if (string.IsNullOrEmpty(json))
                return Enumerable.Empty<BaseFilter>();

            try
            {
                return JsonSerializer.Deserialize<IEnumerable<BaseFilter>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool ApplyStatusFilter<Model>(Model model, int statusType)
            where Model : IBaseModel
        {
            return statusType == (int)Status.All ? true :
                   statusType == (int)Status.Active ? model.DeletedAt == null :
                   model.DeletedAt != null;
        }
    }
}
