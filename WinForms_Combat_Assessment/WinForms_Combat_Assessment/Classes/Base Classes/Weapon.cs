﻿using System.Xml.Serialization;

namespace WinForms_Combat_Assessment
{
    [XmlInclude(typeof(Sword))]
    [XmlInclude(typeof(Axe))]
    [XmlInclude(typeof(Mace))]   
    public abstract class Weapon
    {
        public string Name;
        public int Damage;        
        public abstract void Strike(Character target, int strModifer);        
    }
}