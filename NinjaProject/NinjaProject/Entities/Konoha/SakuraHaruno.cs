using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Sakura
    /// </summary>
    public class SakuraHaruno : BaseNinjaPrototype
    {
        public SakuraHaruno() : base()  
        {
            Occupations = new List<Occupation>() { Occupation.TeamMember, Occupation.Medic };
            Level = Level.Chunin;
            Village = Village.Konohagakure;
            Name = "Sakura Haruno";
            Appearance = "Pink shoulder length hair, green eyes, 16 y.o., height 161 sm, green diamond-shaped mark on the forehead," +
                "dressed in a red T-shirt, pink skirt, leggings, boots.";
        }

        public SakuraHaruno(SakuraHaruno sakura) : base(sakura) { }
    
        public override object Clone()
        {
            return new SakuraHaruno(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new SakuraHaruno(this);
        }
    }
}
