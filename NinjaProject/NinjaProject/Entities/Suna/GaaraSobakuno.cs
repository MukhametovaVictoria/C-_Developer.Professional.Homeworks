using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Gaara
    /// </summary>
    public class GaaraSobakuno : BaseNinjaPrototype
    {
        public GaaraSobakuno() : base() 
        {
            Village = Village.Sunagakure;
            Level = Level.Kage;
            Occupations = new List<Occupation>() { Occupation.Leader, Occupation.Jinchuriki };
            Name = "Gaara Sobakuno";
            Appearance = "Red short hair, green eyes, the face without eyebrows, red costume and a pear-shaped vessel with sand on the back. The height is 166 cm, 16 y.o.";
        }

        private GaaraSobakuno(GaaraSobakuno gaara) : base(gaara) { }
    
        public override object Clone()
        {
            return new GaaraSobakuno(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new GaaraSobakuno(this);
        }
    }
}
