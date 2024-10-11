using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Itachi
    /// </summary>
    public class ItachiUchiha : BaseNinjaPrototype
    {
        public ItachiUchiha() : base()  
        {
            Occupations = new List<Occupation>() { Occupation.Apostate, Occupation.Akatsuki };
            Level = Level.Jonin;
            Village = Village.Konohagakure;
            Name = "Itachi Uchiha";
            Appearance = "Dark hair below shoulders, dark eyes, height 178 cm, age 21, has a sharingan, wears a black cloak with red clouds, wears a ring.";
        }

        public ItachiUchiha(ItachiUchiha itachi) : base(itachi) { }
    
        public override object Clone()
        {
            return new ItachiUchiha(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new ItachiUchiha(this);
        }
    }
}
