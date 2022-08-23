using Newtonsoft.Json.Serialization;
namespace IIRS.Utilities.ContractResolver
{
    public class LowerCasePropertyNames : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
