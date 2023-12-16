using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LethalSDK.Utils
{
    public static class TypeExtensions
    {
        public static readonly Dictionary<removeType, string> regexes = new Dictionary<removeType, string>
        {
            {removeType.Normal, "[^a-zA-Z0-9 ,.!?_-]"},
            {removeType.Serializable, "[^a-zA-Z0-9 .!_-]"},
            {removeType.Keyword, "[^a-zA-Z0-9._-]"},
            {removeType.Path, "[^a-zA-Z0-9 ,.!_/-]"},
            {removeType.SerializablePath, "[^a-zA-Z0-9 .!_/-]"}
        };
        public static string RemoveNonAlphanumeric(this string input)
        {
            return Regex.Replace(input, regexes[removeType.Normal], "");
        }
        public static string[] RemoveNonAlphanumeric(this string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], regexes[removeType.Normal], "");
            }
            return input;
        }
        public static string RemoveNonAlphanumeric(this string input, removeType removeType = removeType.Normal)
        {
            return Regex.Replace(input, regexes[removeType], "");
        }
        public static string[] RemoveNonAlphanumeric(this string[] input, removeType removeType = removeType.Normal)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], regexes[removeType], "");
            }
            return input;
        }
        public static string RemoveNonAlphanumeric(this string input, int removeType = 0)
        {
            return Regex.Replace(input, regexes[(removeType)removeType], "");
        }
        public static string[] RemoveNonAlphanumeric(this string[] input, int removeType = 0)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = Regex.Replace(input[i], regexes[(removeType)removeType], "");
            }
            return input;
        }
        public enum removeType
        {
            Normal = 0,
            Serializable = 1,
            Keyword = 2,
            Path = 3,
            SerializablePath = 4
        }
    }
}
