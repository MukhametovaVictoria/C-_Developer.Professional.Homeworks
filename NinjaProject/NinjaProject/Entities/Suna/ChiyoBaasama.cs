using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Chiyo
    /// </summary>
    public class ChiyoBaasama : BaseNinjaPrototype
    {
        public ChiyoBaasama() : base() 
        {
            Village = Village.Sunagakure;
            Level = Level.Jonin;
            Occupations = new List<Occupation>() { Occupation.Medic, Occupation.Puppeteer };
            Name = "Chiyo-baasama";
            Appearance = "The old woman in a black dress with silver hair gathered in a bun. The height is 149 cm, 73 y.o.";
        }

        private ChiyoBaasama(ChiyoBaasama chiyo) : base(chiyo) { }
    
        public override object Clone()
        {
            return new ChiyoBaasama(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new ChiyoBaasama(this);
        }
    }
}
