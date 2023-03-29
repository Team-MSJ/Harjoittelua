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

        // {}
        // Tehdään Random-objekti to choose random ikonit neliöihin
        // Webdings fontti on ikoneita, tehdään lista, jossa jokainen ikoni = kirjain esiintyy kahdesti

        Random random = new Random();

        List<string> icons = new List<string>()
        {"a", "a", "3", "3", "4", "4", "5", "5", "6", "6", "7", "7", "8", "8", "9", "9"};


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
                    // Oikeasta valinnasta eli parista saa 100 pistettä, ja soitetaan oma ääni
                    points += 100;
                    SumOfPoints.Text = points.ToString();
                    VoiceForMatching();
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Väärästä valinnasta eli epäparista vähennetään 40 pistettä ja soitetaan oma ääni
                points -= 40;
                SumOfPoints.Text = points.ToString();
                VoiceForUnmatching();

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

        private void VoiceForGameEndWinner()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"Resources/congratulations-winner.wav";
            player.Load();
            player.Play();
        }

        private void VoiceForGameEndLooser()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = @"Resources/haha.wav";
            player.Load();
            player.Play();
        }

        // Tarkistetaan jokainen ikoni nähdäksemme, onko sillä pari, vertaamalla
        // sen foreground väriä background väriin. Jos kaikilla ikoneilla on pari, 
        // pelaaja voittaa

        private bool CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return false;
                    
                }
            }

            // Jos luuppi ei palannut, ei se löytänyt mätsäämättömiä ikoneita
            // pelaaja voitti
            VoiceForGameEndWinner();
            timer2.Stop();
            MessageBox.Show("Miten sä ton teit? Onnea nyt sitten");
            Close();           
            return true;
        }

        // This integer variable keeps track of the 
        // remaining game time.       
        int timeLeft = 60;
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                //Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " sec ";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox.
                VoiceForGameEndLooser();
                timer2.Stop();
                timeLabel.Text = "!!!!!!";
                MessageBox.Show("Aika loppui looseri!");
                Close();
            }
        }
        private void startti_Click(object sender, EventArgs e)
        {
            timer2.Start();
            timeLeft = timeLeft - 1;
            timeLabel.Text = timeLeft + " sec ";
        }
    }
}
