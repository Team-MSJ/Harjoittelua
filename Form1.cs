using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Harjoittelua
{
    public partial class Form1 : Form
    {
        // firstClicked viittaa ensin klikattuun labeliin, mutta se on null, jos mitään ei ole vielä klikattu

        Label firstClicked = null;
        Label secondClicked = null;

        int points = 0;

        
        // SumOfPoints.Text = points.ToString();
 

        // {}
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

        // Jokaisen labelin klikkaus käsitellään tällä metodilla

        private void label1_Click(object sender, EventArgs e)
        {
            // timer on käynnissä kahden eri ikonin valinnan jälkeen, joten timerin ollessa
            // päällä, klikkaukset ignoorataan

            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.White)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.White;

                    return;
                }

                // Jos pelaaja pääsee tänne asti, timer ei ole päällä ja firstClicked 
                // ei ole null, tämän täytyy olla toinen klikkaus, asetetaan väri valkoiseksi

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.White;

                // Tarkistetaan, onko pelaaja voittanut

                CheckForWinner();

                // jos ikonit ovat samat, resetoidaan arvot, jotta ikonit jäävät näkyviin

                if (firstClicked.Text == secondClicked.Text)
                {
                    points += 100;
                    SumOfPoints.Text = points.ToString();
                    VoiceForMatching();
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                points -= 10;
                SumOfPoints.Text = points.ToString();
                VoiceForUnmatching();

                // Tähän kohtaan koodi äänen käynnistyksestä, kun on valittu eri ikonit

                timer1.Start();

                // clickedLabel.ForeColor = Color.White;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ensin pysäytetään timer ja piilotetaan ikonit
            // sen jälkeen resetoidaan firstClicked ja secondClicked jotta seuraavan 
            // kerran kun ruutua klikataan, ohjelma tietään sen olevan ensimmäinen klikkaus

            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void VoiceForUnmatching()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"Resources/R2D2-do.wav";
            player.Load();
            player.Play();
        }

        private void VoiceForMatching()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"Resources/R2D2-hey-you.wav";
            player.Load();
            player.Play();
        }

        // Tarkistetaan jokainen ikoni nähdäksemme, onko sillä pari, vertaamalla
        // sen foreground väriä background väriin. Jos kaikilla ikoneilla on pari, 
        // pelaaja voittaa

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                    
                }
            }

            // Jos luuppi ei palannut, ei se löytänyt mätsäämättömiä ikoneita
            // pelaaja voitti

            MessageBox.Show("Miten sä ton teit?", "Onnea nyt sitten");
            Close();
        }
    }
}
