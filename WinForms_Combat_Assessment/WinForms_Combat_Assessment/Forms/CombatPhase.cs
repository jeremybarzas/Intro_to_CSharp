﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinForms_Combat_Assessment
{
    public partial class CombatPhase : Form
    {
        private void CombatFormLoad()
        {
            End_Turn.Enabled = false;

            // attack spell item select
            foreach (Weapon weapon in AppManager.Instance.DataManager.CurrentPlayer.Info.Weapons)
                Attack_Selector.Items.Add(weapon.Name);

            foreach (Spell spell in AppManager.Instance.DataManager.CurrentPlayer.Info.Spellbook)
                Spell_Selector.Items.Add(spell.Name);

            foreach (Item item in AppManager.Instance.DataManager.CurrentPlayer.Info.Backpack)
                Item_Selector.Items.Add(item.Name);

            // target select
            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
                if (character.Info.Alive == true)
                    Attack_Target_Selector.Items.Add(character.Info.Name);

            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
                if (character.Info.Alive == true)
                    Spell_Target_Selector.Items.Add(character.Info.Name);

            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
                if (character.Info.Alive == true)
                    Item_Target_Selector.Items.Add(character.Info.Name);

            foreach (var control in Controls)
            {
                var c = control as ComboBox;
                if(c != null)               
                    c.SelectedIndex = 0;
            }

            // updates combat round number
            Combat_Phase_Label.Text = "Combat Round " + AppManager.Instance.DataManager.RoundNumber;
            Current_Player_Name_Label.Text = AppManager.Instance.DataManager.CurrentPlayer.Info.Name + "'s Turn";
            
            // updates health and mana
            Player_Health_Text.Text = AppManager.Instance.DataManager.CurrentPlayer.Info.Health.ToString();
            Player_Mana_Text.Text = AppManager.Instance.DataManager.CurrentPlayer.Info.Mana.ToString();
        }

        private void ConfirmSelections()
        {
            foreach (Weapon weapon in AppManager.Instance.DataManager.CurrentPlayer.Info.Weapons)
            {
                if (Attack_Selector.SelectedItem.Equals(weapon.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.ActiveWeapon = weapon;
                    break;
                }
            }

            foreach (Item item in AppManager.Instance.DataManager.CurrentPlayer.Info.Backpack)
            {
                if (Item_Selector.SelectedItem.Equals(item.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.ActiveItem = item;
                    AppManager.Instance.DataManager.GameRoster[AppManager.Instance.DataManager.GameRoster.IndexOf(AppManager.Instance.DataManager.CurrentPlayer)].RemoveFromBackpack(item);
                    break;
                }
            }

            foreach (Spell spell in AppManager.Instance.DataManager.CurrentPlayer.Info.Spellbook)
            {
                if (Spell_Selector.SelectedItem.Equals(spell.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.ActiveSpell = spell;
                    break;
                }
            }

            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            {
                if (Attack_Target_Selector.SelectedItem.Equals(character.Info.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.AttackTarget = character;
                    break;
                }
            }

            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            {
                if (Item_Target_Selector.SelectedItem.Equals(character.Info.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.ItemTarget = character;
                    break;
                }
            }

            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            {
                if (Spell_Target_Selector.SelectedItem.Equals(character.Info.Name))
                {
                    AppManager.Instance.DataManager.CurrentPlayer.Info.SpellTarget = character;
                    break;
                }
            }           
        }

        private void FillCombatText()
        {            
            bool[] tmpRoster = new bool[AppManager.Instance.DataManager.GameRoster.Count];

            int i = 0;
            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            {                
                tmpRoster[i] = character.Info.Alive;
                i++;
            }

            AppManager.DoActions();

            Combat_Textbox.Text = AppManager.Instance.CombatLog;

            i = 0;
            foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            {
                if(character.Info.Alive != tmpRoster[i])
                {
                    Combat_Textbox.Text += character.Info.Name + " has died.\n\n";
                    AppManager.Instance.DataManager.GameRoster[AppManager.Instance.DataManager.GameRoster.IndexOf(AppManager.Instance.DataManager.CurrentPlayer)].Info.Kills += 1;
                }                
                i++;
            }

            // updates health and mana
            Player_Health_Text.Text = AppManager.Instance.DataManager.CurrentPlayer.Info.Health.ToString();
            Player_Mana_Text.Text = AppManager.Instance.DataManager.CurrentPlayer.Info.Mana.ToString();

            //Character[] tmpRoster = new Character[AppManager.Instance.DataManager.GameRoster.Count];
            //AppManager.Instance.DataManager.GameRoster.CopyTo(tmpRoster);

            //AppManager.DoActions();

            //Combat_Textbox.Text = AppManager.Instance.CombatLog;

            //foreach (Character character in AppManager.Instance.DataManager.GameRoster)
            //{
            //    int i = AppManager.Instance.DataManager.GameRoster.IndexOf(character);

            //    if (character.Info.Alive != tmpRoster[i].Info.Alive)
            //    {
            //        Combat_Textbox.Text += character.Info.Name + " has died.\n\n";
            //    }

            //    int a = tmpRoster.IndexOf(character) + 1;
            //    if (character.Info.Alive == false && tmpRoster[tmpRoster.IndexOf(character) + 1].Info.Alive == true)
            //    {
            //        Combat_Textbox.Text += character.Info.Name + " has died.\n\n";
            //    }
            //}
        }

        private void ConfirmClick()
        {
            Confirm.Enabled = false;
            End_Turn.Enabled = true;

            Attack_Selector.Enabled = false;
            Item_Selector.Enabled = false;
            Spell_Selector.Enabled = false;

            Attack_Target_Selector.Enabled = false;
            Item_Target_Selector.Enabled = false;
            Spell_Target_Selector.Enabled = false;

            ConfirmSelections();
            FillCombatText();
        }

        private void EndTurnClick()
        {
            Character temp = AppManager.Instance.DataManager.CurrentPlayer;

            AppManager.Instance.DataManager.TurnCount += 1;

            AppManager.Instance.DataManager.SetRemainingPlayers();

            if (AppManager.Instance.DataManager.RemainingPlayers == 1)
            {
                AppManager.Instance.DataManager.RoundNumber++;
                AppManager.Instance.DataManager.TurnCount = 0;

                Program.ChangeForm(this, 6);
            }
            else if (AppManager.Instance.DataManager.TurnCount == AppManager.Instance.DataManager.RemainingPlayers)
            {
                AppManager.Instance.DataManager.RoundNumber++;
                AppManager.Instance.DataManager.TurnCount = 0;

                Program.ChangeForm(this, 3);
            }
            else
            {
                if (AppManager.Instance.DataManager.GameRoster[AppManager.Instance.DataManager.GameRoster.IndexOf(AppManager.Instance.DataManager.CurrentPlayer) + 1].Info.Alive == true &&
                   AppManager.Instance.DataManager.CurrentPlayer != AppManager.Instance.DataManager.GameRoster[AppManager.Instance.DataManager.GameRoster.Count - 1])
                {
                    AppManager.Instance.DataManager.SetCurrentPlayer(AppManager.Instance.DataManager.GameRoster.IndexOf(AppManager.Instance.DataManager.CurrentPlayer) + 1);
                }

                Program.ChangeForm(this, 5);
            }
        }

        public CombatPhase()
        {
            InitializeComponent();
        }

        private void Combat_Load(object sender, EventArgs e)
        {
            CombatFormLoad();
        }

        private void To_Main_Menu_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(0);
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            ConfirmClick();
        }

        private void End_Turn_Click(object sender, EventArgs e)
        {
            EndTurnClick();
        }
    }
}