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
        //  { }
        // Tehdään Random-objekti to choose random ikonit neliöihin
        // Webdings fontti on ikoneita, tehdään lista, jossa jokainen ikoni = kirjain esiintyy kahdesti

        Random random = new Random();

        List<string> icons = new List<string>()
        {"M", "M", "S", "S", "T", "T", "i", "i", "a", "a", "t", "t", "7", "7", "H", "H"};
        public Form1()
        {
            InitializeComponent();
        }
    }
}
