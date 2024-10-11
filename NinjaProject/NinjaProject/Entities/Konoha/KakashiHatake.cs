using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Kakashi
    /// </summary>
    public class KakashiHatake : BaseNinjaPrototype
    {
        public KakashiHatake() : base()  
        {
            Occupations = new List<Occupation>() { Occupation.TeamMember, Occupation.Leader, Occupation.Sensei };
            Level = Level.Jonin;
            Village = Village.Konohagakure;
            Name = "Kakashi Hatake";
            Appearance = "Ash-blond hair, dark eyes, 181 cm tall, 30 years old, has a sharingan, wears a mask and the standard Hidden Leaf Village uniform.";
        }

        public KakashiHatake(KakashiHatake kakashi) : base(kakashi) { }
    
        public override object Clone()
        {
            return new KakashiHatake(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new KakashiHatake(this);
        }
    }
}
