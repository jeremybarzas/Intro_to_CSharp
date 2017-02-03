﻿using System;

namespace WinForms_Combat_Assessment
{
    class Spell : ICastable
    {
        private string m_name;
        private int m_damage;
        private int m_manaCost;

        public Spell(string n, int dmg, int mana)
        {
            m_name = n;
            m_damage = dmg;
            m_manaCost = mana;
        }

        public bool Cast(Character target)
        {
            if (target.Mana >= m_manaCost)
            {
                target.Health -= m_damage;
                return true;
            }
            else
            {
                Console.WriteLine("Not enough mana!!!\n");
                return false;
            } 
        }
    }
}