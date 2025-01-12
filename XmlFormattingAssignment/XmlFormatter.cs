using System.Collections;
using System.Reflection;
using System.Text;

namespace XmlFormattingAssignment
{
    public static class XmlFormatter
    {
        public static string ConvertToXML(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Input object cannot be null");

            Type objType = obj.GetType();
            string XML = $"<{objType.Name}>";

            foreach (PropertyInfo pi in objType.GetProperties())
            {
                // Skip indexer properties
                if (pi.GetIndexParameters().Length > 0)
                    continue;

                Type type = pi.PropertyType;
                object? value = pi.GetValue(obj);
                if (value == null)
                {
                    XML += $"<{pi.Name}></{pi.Name}>";
                }
                else if (type == typeof(DateTime) || Nullable.GetUnderlyingType(type) == typeof(DateTime))
                {
                    XML += $"<{pi.Name}>{pi.GetValue(obj)}</{pi.Name}>";
                }
                else if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
                {
                    if (Convert.ToString(value) == "0")
                    {
                        XML += $"<{pi.Name}></{pi.Name}";
                    }
                    else
                    {
                        XML += $"<{pi.Name}>{pi.GetValue(obj)}</{pi.Name}>";
                    }
                }
                else if (typeof(IEnumerable).IsAssignableFrom(type)) // Handle collections
                {
                    XML += $"<{pi.Name}>";

                    if (value is IEnumerable collection)
                    {
                        foreach (var item in collection)
                        {
                            Type itemType = item.GetType();
                            if (itemType.IsPrimitive || itemType == typeof(string) || itemType == typeof(decimal) || itemType == typeof(DateTime))
                            {
                                if (Convert.ToString(item) == "0")
                                {
                                    XML += $"<{itemType.Name}></{itemType.Name}>";
                                }
                                else
                                {
                                    XML += $"<{itemType.Name}>{item}</{itemType.Name}>";
                                }

                            }
                            else
                            {
                                XML += $"<{itemType.Name}>";
                                XML += Visit(item); // Serialize each item in the collection
                                XML += $"</{itemType.Name}>";
                            }

                        }
                    }

                    XML += $"</{pi.Name}>";
                }
                else
                {
                    XML += $"<{pi.Name}>";
                    XML += Visit(value);
                    XML += $"</{pi.Name}>";
                }
            }

            XML += $"</{objType.Name}>";

            return XML;
        }

        private static string Visit(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Input object cannot be null");

            Type objType = obj.GetType();
            string XML = "";

            foreach (PropertyInfo pi in objType.GetProperties())
            {
                // Skip indexer properties
                if (pi.GetIndexParameters().Length > 0)
                    continue;

                Type type = pi.PropertyType;
                object? value = pi.GetValue(obj);
                if (value == null)
                {
                    XML += $"<{pi.Name}></{pi.Name}>";
                }
                else if (type == typeof(DateTime) || Nullable.GetUnderlyingType(type) == typeof(DateTime))
                {
                    XML += $"<{pi.Name}>{pi.GetValue(obj)}</{pi.Name}>";
                }
                else if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
                {
                    if (Convert.ToString(value) == "0")
                    {
                        XML += $"<{pi.Name}></{pi.Name}";
                    }
                    else
                    {
                        XML += $"<{pi.Name}>{pi.GetValue(obj)}</{pi.Name}>";
                    }
                }
                else if (typeof(IEnumerable).IsAssignableFrom(type)) // Handle collections
                {
                    XML += $"<{pi.Name}>";

                    if (value is IEnumerable collection)
                    {
                        foreach (var item in collection)
                        {
                            Type itemType = item.GetType();
                            if (itemType.IsPrimitive || itemType == typeof(string) || itemType == typeof(decimal) || itemType == typeof(DateTime))
                            {
                                if (Convert.ToString(item) == "0")
                                {
                                    XML += $"<{itemType.Name}></{itemType.Name}>";
                                }
                                else
                                {
                                    XML += $"<{itemType.Name}>{item}</{itemType.Name}>";
                                }

                            }
                            else
                            {
                                XML += $"<{itemType.Name}>";
                                XML += Visit(item); // Serialize each item in the collection
                                XML += $"</{itemType.Name}>";
                            }

                        }
                    }

                    XML += $"</{pi.Name}>";
                }
                else
                {
                    XML += $"<{pi.Name}>";
                    XML += Visit(value);
                    XML += $"</{pi.Name}>";
                }
            }

            return XML;
        }
    }
}