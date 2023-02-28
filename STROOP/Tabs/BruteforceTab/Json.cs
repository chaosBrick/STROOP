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
            int lastValueStart = -1;
            var keyBuilder = new StringBuilder(input.Length);
            bool inString = false;
            bool escape = false;
            var arrayStack = 0;

            string GetValueString(int curseYou) => input.Substring(lastValueStart, curseYou - lastValueStart).Trim();
            string GetKeyString() => keyBuilder.ToString().Trim().Trim('"');

            JsonObject newObject = new JsonObject();
            for (; cursor < input.Length; cursor++)
            {
                var c = input[cursor];
                if (lastValueStart == -1)
                    keyBuilder.Append(c);
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
                                newObject.AddObject(GetKeyString(), GetJsonObject(input, ref cursor));
                            }
                            break;
                        case '}':
                            if (!inString)
                            {
                                var valueString = GetValueString(cursor);
                                if (keyBuilder.Length > 0)
                                    newObject.AddValue(GetKeyString(), valueString);
                                newObject.sourceString = valueString;
                                return newObject;
                            }
                            break;
                        case ':':
                            if (!inString)
                            {
                                keyBuilder.Remove(keyBuilder.Length - 1, 1);
                                lastValueStart = cursor + 1;
                            }
                            else
                                keyBuilder.Append(c);
                            break;
                        case ',':
                            if (!inString && arrayStack == 0)
                            {
                                newObject.AddValue(GetKeyString(), GetValueString(cursor));
                                lastValueStart = -1;
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
