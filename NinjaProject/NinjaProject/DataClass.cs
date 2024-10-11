using Enums;
using System.Collections.Generic;

namespace NinjaProject
{
    public static class DataClass
    {
        public static Dictionary<Village, string> Villages = new Dictionary<Village, string>()
        {
            {Village.Konohagakure, "The village hidden in leaves" },
            {Village.Kirigakure, "The village hidden in fog" },
            {Village.Sunagakure, "The village hidden in sand" }
        };

        public static Dictionary<Village, List<string>> Ninja = new Dictionary<Village, List<string>>()
        {
            {Village.Konohagakure, new List<string>(){ "Naruto Uzumaki", "Itachi Uchiha", "Kakashi Hatake", "Sakura Haruno"} },
            {Village.Kirigakure, new List<string>() { "Zabuza Momochi", "Kisame Hoshigaki" } },
            {Village.Sunagakure, new List<string>() { "Gaara Sobakuno", "Temari Sobakuno", "Chiyo-baasama" } }
        };
    }
}
