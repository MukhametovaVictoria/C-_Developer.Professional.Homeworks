using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Kisame
    /// </summary>
    public class KisameHoshigaki : BaseNinjaPrototype
    {
        public KisameHoshigaki()
        {
            Village = Village.Kirigakure;
            Level = Level.Jonin;
            Occupations = new List<Occupation>() { Occupation.Apostate, Occupation.Mercenary, Occupation.Akatsuki, Occupation.Swordsman };
            Name = "Kisame Hoshigaki";
            Appearance = "The fish-like appearance, grey skin, small eyes, blue hair, sharp teeth. The height is 195 cm, 32 y.o.";
        }

        private KisameHoshigaki(KisameHoshigaki kisame) : base(kisame) { }
    
        public override object Clone()
        {
            return new KisameHoshigaki(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new KisameHoshigaki(this);
        }
    }
}
