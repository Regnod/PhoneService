using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVCPhoneServiceWeb.Utils
{
    public class CsvWriter<T>
    {
        private static char _separator;

        public CsvWriter(char separator)
        {
            _separator = separator;
        }

        public CsvWriter()
        {
            _separator = ',';
        }

        public string ToCsv(T[] objects)
        {
            string csv = "";
            var objectProperties = objects[0].GetType().GetProperties().ToList();
            for (int i = 0; i < objectProperties.Count; i++)
            {
                csv += objectProperties[i].Name;
                if (i != objectProperties.Count - 1)
                    csv += _separator;
            }
            csv += "\n";
            foreach (var objectx in objects)
            {
                objectProperties = objectx.GetType().GetProperties().ToList();
                for (int i = 0; i < objectProperties.Count; i++)
                {
                    csv += objectProperties[i].GetValue(objectx)?.ToString();
                    if (i != objectProperties.Count - 1)
                    {
                        csv += _separator;
                    }
                }
                csv += '\n';
            }
            return csv;
        }
        public string ToCsv(T[] objects, string[] selectedProperties)
        {
            string csv = "";
            var objectProperties = objects[0].GetType().GetProperties();
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var property in objectProperties)
            {
                if (selectedProperties.Contains(property.Name))
                    properties.Add(property);
            }
            for (int i = 0; i < properties.Count; i++)
            {
                csv += properties[i].Name;
                if (i != properties.Count - 1)
                    csv += _separator;
            }
            csv += "\n";
            foreach (var objectx in objects)
            {
                objectProperties = objectx.GetType().GetProperties();
                int propertiesCount = 0;
                for (int i = 0; i < objectProperties.Length; i++)
                {
                    if (selectedProperties.Contains(objectProperties[i].Name))
                    {
                        csv += objectProperties[i].GetValue(objectx)?.ToString();
                        propertiesCount += 1;
                        if (propertiesCount != selectedProperties.Length)
                        {
                            csv += _separator;
                        }
                    }
                }
                csv += '\n';
            }
            return csv;
        }
    }
}