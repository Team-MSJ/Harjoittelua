using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harjoittelua
{
    public partial class Form1 : Form
    {
        //  {}
        // Tehdään Random-objekti to choose random ikonit neliöihin
        // Webdings fontti on ikoneita, tehdään lista, jossa jokainen ikoni = kirjain esiintyy kahdesti

        Random random = new Random();

        List<string> icons = new List<string>()
        {"M", "M", "S", "S", "T", "T", "i", "i", "a", "a", "t", "t", "7", "7", "H", "H"};

        // Metodi sijoittaa pelin neliöihin merkit satunnaisesti

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconlabel = control as Label;
                if (iconlabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconlabel.Text = icons[randomNumber];
                    iconlabel.ForeColor = iconlabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.White)
                    return;

                clickedLabel.ForeColor = Color.White;
            }
        }
    }
}
