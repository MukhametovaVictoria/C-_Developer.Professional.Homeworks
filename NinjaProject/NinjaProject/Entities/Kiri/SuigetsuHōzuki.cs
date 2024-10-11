using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Suigetsu
    /// </summary>
    public class SuigetsuHōzuki : BaseNinjaPrototype
    {
        public SuigetsuHōzuki()
        {
            Village = Village.Kirigakure;
            Level = Level.Jonin;
            Occupations = new List<Occupation>() { Occupation.Apostate, Occupation.Mercenary, Occupation.Swordsman };
            Name = "Suigetsu Hōzuki";
            Appearance = "White hair, purple eyes, sharp teeth, the height is 177cm, 17 y.o. He wears a purple, sleeveless shirt with blue pants, sandals and a belt around his waist with water bottles attached to it.";
        }

        private SuigetsuHōzuki(SuigetsuHōzuki zabuza) : base(zabuza) { }
    
        public override object Clone()
        {
            return new SuigetsuHōzuki(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new SuigetsuHōzuki(this);
        }
    }
}
