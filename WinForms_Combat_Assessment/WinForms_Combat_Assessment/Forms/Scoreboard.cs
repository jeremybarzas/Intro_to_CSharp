﻿using System;
using System.Windows.Forms;

namespace WinForms_Combat_Assessment
{
    public partial class Scoreboard : Form
    {
        public Scoreboard()
        {
            InitializeComponent();
        }

        private void Scoreboard_Load(object sender, EventArgs e)
        {
            GameManager.Instance.DataManager.GameRoster.Sort((a, b) => -1 * (a.Score.CompareTo(b.Score)));

            foreach (Character character in GameManager.Instance.DataManager.GameRoster)
            {
                Player_Name_Text.Text += " " + character.Name + "\n\n";                
                Kills_Text.Text += "    " + character.Kills + "\n\n";
                Score_Text.Text += "    " + character.Score + "\n\n";

                if (character.Alive == true)
                    Status_Text.Text += "   Alive" + "\n\n";
                else
                    Status_Text.Text += "   Dead" + "\n\n";          
            }
        }

        private void To_Main_Menu_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(this, 0);
        }

        private void Start_Game_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(this, 4);
        }

        private void Save_Game_Click(object sender, EventArgs e)
        {           
            GameSerialization<DataManager>.Serialize("GameSave", GameManager.Instance.DataManager);
        }
    }
}