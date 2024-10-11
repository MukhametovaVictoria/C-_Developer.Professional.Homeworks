using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Naruto
    /// </summary>
    public class NarutoUzumaki : BaseNinjaPrototype
    {
        public NarutoUzumaki() : base()  
        {
            Occupations = new List<Occupation>() { Occupation.TeamMember, Occupation.Jinchuriki };
            Level = Level.Genin;
            Village = Village.Konohagakure;
            Name = "Naruto Uzumaki";
            Appearance = "Wheat-colored hair, short length, blue eyes, 16 years old, height 166 cm, dressed in orange pants and a jacket.";
        }

        public NarutoUzumaki(NarutoUzumaki naruto) : base(naruto) { }
    
        public override object Clone()
        {
            return new NarutoUzumaki(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new NarutoUzumaki(this);
        }
    }
}
