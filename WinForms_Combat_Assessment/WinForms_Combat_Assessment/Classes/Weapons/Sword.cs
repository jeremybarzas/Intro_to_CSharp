﻿namespace WinForms_Combat_Assessment
{
    public class Sword : Weapon
    {
        new public void Strike(Character target, int strModifer)
        {
            target.Health -= Damage * strModifer;
        }

        public Sword() { }

        public Sword(string n, int dmg)
        {
            Name = n;
            Damage = dmg;
        }
    }
}