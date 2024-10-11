using Entities.AbstractClasses;
using Enums;
using System.Collections.Generic;

namespace Entities
{
    // <summary>
    /// Temari
    /// </summary>
    public class TemariSobakuno : BaseNinjaPrototype
    {
        public TemariSobakuno() : base() 
        {
            Village = Village.Sunagakure;
            Level = Level.Jonin;
            Occupations = new List<Occupation>() { Occupation.TeamMember };
            Name = "Temari Sobakuno";
            Appearance = "Blond hair, dark eyes, she wears black long dress and big fan, the height is 165cm, 19 y.o.";
        }

        private TemariSobakuno(TemariSobakuno temari) : base(temari) { }
    
        public override object Clone()
        {
            return new TemariSobakuno(this);
        }

        public override BaseNinjaPrototype MyClone()
        {
            return new TemariSobakuno(this);
        }
    }
}
