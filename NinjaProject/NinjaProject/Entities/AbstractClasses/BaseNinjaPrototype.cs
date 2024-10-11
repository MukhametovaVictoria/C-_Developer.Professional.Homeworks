using Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using NinjaProject;

namespace Entities.AbstractClasses
{
    /// <summary>
    /// Абстракция шиноби
    /// </summary>
    public abstract class BaseNinjaPrototype: IMyCloneable<BaseNinjaPrototype>, ICloneable
    {
        public BaseNinjaPrototype()
        {
            Village = Village.Konohagakure;
            Occupations = new List<Occupation>() { Occupation.ANBUmember };
            Level = Level.Jonin;
            Name = "Tenzo";
            Appearance = "Hidden";
        }
        public BaseNinjaPrototype(BaseNinjaPrototype ninja)
        {
            this.SetVillage(ninja.Village);
            this.SetOccupation(ninja.Occupations);
            this.SetLevel(ninja.Level);
            this.SetName(ninja.Name);
            this.SetAppearance(ninja.Appearance);
        }

        public Village Village { get; protected set; }
        public List<Occupation> Occupations { get; protected set; }
        public Level Level { get; protected set; }
        public string Name { get; protected set; }
        public string Appearance { get; set; }

        protected BaseNinjaPrototype SetVillage(Village village)
        {
            Village = village;
            return this;
        }
        
        protected BaseNinjaPrototype SetOccupation(List<Occupation> occupations)
        {
            Occupations = occupations;
            return this;
        }

        protected BaseNinjaPrototype SetLevel(Level level)
        {
            Level = level;
            return this;
        }

        protected BaseNinjaPrototype SetName(string name)
        {
            Name = name;
            return this;
        }

        protected BaseNinjaPrototype SetAppearance(string appearance)
        {
            Appearance = appearance;
            return this;
        }

        public abstract object Clone();
        public abstract BaseNinjaPrototype MyClone();

        public void PrintInfo()
        {
            Console.WriteLine
                (
                    $"{Name} parameters:\n" +
                    $"* Village - {Enum.GetName(typeof(Village), Village)}\n" +
                    $"* Level - {Enum.GetName(typeof(Level), Level)}\n" +
                    $"* Occupations - {Helper.OccupationsToString(Occupations)}\n" +
                    $"* Appearance: {Appearance}.\n"
                );
        }
    }
}
