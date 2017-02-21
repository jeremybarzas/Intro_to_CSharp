﻿using System.Collections.Generic;

namespace WinForms_Combat_Assessment
{
    public class Potion : Item
    {
        public int m_healingPower;
        public int m_manaPower;

        public override List<int> Use(Character target)
        {
            List<int> values = new List<int>();

            target.Info.Health += m_healingPower;
            target.Info.Mana += m_manaPower;

            values.Add(m_healingPower);
            values.Add(m_manaPower);
            values.Add(0);
            values.Add(0);

            return values;
        }

        public Potion() { }

        public Potion(string n, int heal, int mana)
        {
            Name = n;
            m_healingPower = heal;
            m_manaPower = mana;
        }      
    }
}