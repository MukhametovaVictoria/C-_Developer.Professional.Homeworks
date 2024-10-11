using Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NinjaProject
{
    public static class Helper
    {
        public static string OccupationsToString(List<Occupation> occupations)
        {
            return string.Join(", ", occupations.Select(x => Enum.GetName(typeof(Occupation), x)).ToList());
        }
    }
}
