using System.Collections.Generic;
using System.Text;

namespace STROOP.Tabs.BruteforceTab
{

    class JsonObject
    {
        public Dictionary<string, string> valueStrings = new Dictionary<string, string>();
        public Dictionary<string, JsonObject> valueObjects = new Dictionary<string, JsonObject>();
        List<(string, object)> tuples = new List<(string, object)>();
        public string sourceString;
        public void AddValue(string key, string value)
        {
            value = value.Trim();
            valueStrings[key] = value;
            tuples.Add((key, value));
        }
        public void AddObject(string key, JsonObject value)
        {
            valueObjects[key] = value;
            tuples.Add((key, value));
        }

        public static JsonObject GetJsonObject(string input, ref int cursor)
        {
            int cursorAtStart = cursor;
            var valueBuilder = new StringBuilder(input.Length);
            var keyBuilder = new StringBuilder(input.Length);
            bool inString = false;
            bool escape = false;
            bool expectKey = true;
            var arrayStack = 0;

            JsonObject newObject = new JsonObject();
            for (; cursor < input.Length; cursor++)
            {
                var c = input[cursor];
                var currentBuilder = (expectKey ? keyBuilder : valueBuilder);
                currentBuilder.Append(c);
                if (c == '\\')
                {
                    escape = true;
                    continue;
                }
                if (c == '"')
                {
                    if (!escape)
                        inString = !inString;
                }
                else
                {
                    switch (c)
                    {
                        case '{':
                            if (!inString)
                            {
                                cursor++;
                                newObject.AddObject(keyBuilder.ToString(), GetJsonObject(input, ref cursor));
                            }
                            break;
                        case '}':
                            if (!inString)
                            {
                                currentBuilder.Remove(currentBuilder.Length - 1, 1);
                                if (keyBuilder.Length > 0)
                                    newObject.AddValue(keyBuilder.ToString().Trim().Trim('"'), valueBuilder.ToString());
                                newObject.sourceString = "{" + input.Substring(cursorAtStart, cursor - cursorAtStart + 1);
                                return newObject;
                            }
                            break;
                        case ':':
                            if (!inString)
                            {
                                currentBuilder.Remove(currentBuilder.Length - 1, 1);
                                expectKey = false;
                                valueBuilder.Clear();
                            }
                            else
                                currentBuilder.Append(c);
                            break;
                        case ',':
                            if (!inString && arrayStack == 0)
                            {
                                currentBuilder.Remove(currentBuilder.Length - 1, 1);
                                expectKey = true;
                                newObject.AddValue(keyBuilder.ToString().Trim().Trim('"'), valueBuilder.ToString());
                                keyBuilder.Clear();
                            }
                            break;
                        case '[':
                            arrayStack++;
                            break;
                        case ']':
                            arrayStack--;
                            break;
                    }
                }
                escape = false;
            }
            return newObject;
        }
    }
}
