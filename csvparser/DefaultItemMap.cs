using System.Linq;

namespace CSV.Parser
{
    public class DefaultItemMap<T> : ICSVItemParser<T> where T : new()
    {
        public T Map(string[] line, string[] headers)
        {
            var item = new T();
            var itemTypeProperties = item.GetType().GetProperties();

            var mapHeaderValue = headers.Zip(line, (h, i) => new { h, i }).ToDictionary(item => item.h.ToLower() , item => item.i);

            foreach (var propertyInfo in itemTypeProperties)
            {
                //Conversion to property type needs to be done.
                propertyInfo.SetValue(item, mapHeaderValue[propertyInfo.Name.ToLower()]);
            }

            return item;
        }
    }
}
