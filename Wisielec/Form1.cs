using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wisielec
{
    public partial class Form1 : Form
    {

        Dictionary<int, Label> dictionaryLabels = new Dictionary<int, Label>();

        List<char> listLiterki = new List<char>();

        public static int LICZNIK_BLEDOW = 0;    
        

        /// <param name="kategoria"> Kategoria slowa do odgadnięcia </param>
        /// <param name="nazwa"> Nazwa slowa do odgadnięcia </param>
        public Form1(string kategoria, string nazwa)
        {
            InitializeComponent();

            
            labelKategoria.Text = kategoria;
            textBox2.Text = nazwa.ToUpper();

            //Slowo zagadka
            for (int i = 0; i < nazwa.Count(); i++)
            {
                listLiterki.Add(nazwa.ToUpper().ElementAt(i));
                Console.WriteLine("Litera: {0} , z {1}", listLiterki[i], nazwa.ElementAt(i));
            }
            Console.WriteLine("Długość słowa nowego: {0} powstałego na podstawie {1}", listLiterki.Count(), nazwa.Count());
            textBoxIleLiter.Text = listLiterki.Count().ToString();

            //labelki
            inicjalizacjaSlownikLabelkow();

            int licznik_ile_literek = 0;
            Console.WriteLine("Ile jest labelkow: " + dictionaryLabels.Count);

            //wyswietla tyle "_" ile jest liter w slowie
            foreach (KeyValuePair<int, Label> item in dictionaryLabels)
            {
                if (licznik_ile_literek < listLiterki.Count())
                {
                    item.Value.Text = "_";
                    licznik_ile_literek++;
                }
                else
                {
                    item.Value.Text = "";
                }
                  
            }
        }

        private void inicjalizacjaSlownikLabelkow()
        {

            dictionaryLabels.Add(1, label1);
            dictionaryLabels.Add(2, label2);
            dictionaryLabels.Add(3, label3);
            dictionaryLabels.Add(4, label4);
            dictionaryLabels.Add(5, label5);
            dictionaryLabels.Add(6, label6);
            dictionaryLabels.Add(7, label7);
            dictionaryLabels.Add(8, label8);
            dictionaryLabels.Add(9, label9);
            dictionaryLabels.Add(10,label10);
            dictionaryLabels.Add(11,label11);
            dictionaryLabels.Add(12,label12);
        }


        /// <summary>
        /// Będzie wywoływane za każdym razem po naciśnięciu przycisku :P 
        /// </summary>
        public void czyKoniec(string literaDoSprawdzenia)
        {
            string slowo = "";

            bool czyJestLitera = false;

            int pozycja = 0;
            foreach (char item in listLiterki)
            {
                pozycja++;
                if (item.ToString().Equals(literaDoSprawdzenia))
                {
                    dictionaryLabels.ElementAt(pozycja-1).Value.Text = literaDoSprawdzenia;
                    czyJestLitera = true;

                }
            }


            //jesli nie ma litery do dodaje licznik++ i nowy obrazek
            if (!czyJestLitera)
            {
                Console.WriteLine("Czy mam dostęp do liter : " + listLiterki[0] + "  i " + listLiterki[listLiterki.Count - 1]);
                LICZNIK_BLEDOW++;
                switch (LICZNIK_BLEDOW)
                {
                    case 1:
                        pictureBoxMain.Image = Properties.Resources.wisielec_1;
                        break;
                    case 2:
                        pictureBoxMain.Image = Properties.Resources.wisielec_2;
                        break;
                    case 3:
                        pictureBoxMain.Image = Properties.Resources.wisielec_3;
                        break;
                    case 4:
                        pictureBoxMain.Image = Properties.Resources.wisielec_4;
                        break;
                    case 5:
                        pictureBoxMain.Image = Properties.Resources.wisielec_5;
                        break;
                    case 6:
                        pictureBoxMain.Image = Properties.Resources.wisielec_6;
                        break;
                    case 7:
                        pictureBoxMain.Image = Properties.Resources.wisielec_7;
                        break;
                    case 8:
                        pictureBoxMain.Image = Properties.Resources.wisielec_8;
                        gameOver();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //budujemy slowo na podstawie liter odgadniętych
                foreach (KeyValuePair<int, Label> item in dictionaryLabels)
                {
                    slowo += item.Value.Text;
                }

                if (textBox2.Text.Equals(slowo) && LICZNIK_BLEDOW != 8)
                {
                    MessageBox.Show("BRAWO URATOWAŁEŚ SAMOBÓJCĘ !!! ", "Koniec gry!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    restartGry();
                }
            }

        
            labelLicznik.Text = ""+LICZNIK_BLEDOW +"/8";
        }

        private void gameOver()
        {
            int licznikLabelekDoWyswietlenia = 0;
            foreach (KeyValuePair<int, Label> item in dictionaryLabels)
            {
                if (licznikLabelekDoWyswietlenia < textBox2.Text.Count())
                {
                    item.Value.ForeColor = Color.Black;
                    item.Value.Text = listLiterki[licznikLabelekDoWyswietlenia].ToString();
                    licznikLabelekDoWyswietlenia++;
                }
                else break;
               
            }
            labelLicznik.Text = "" + LICZNIK_BLEDOW + "/8";
            MessageBox.Show("Niestety nie udało Ci się uratować samóbujcy!", "Koniec gry!",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);


            
            restartGry();
        }

        private void restartGry()
        {
   
            DialogResult dialogResult = MessageBox.Show("Czy chcesz zagrać jeszcze raz?", "Pytanie",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                    LICZNIK_BLEDOW = 0;

                    labelLicznik.Text = "" + LICZNIK_BLEDOW;
                    pictureBoxMain.Image = null;

                    this.Hide();
                    Form2 form2 = new Form2();
                    form2.Show();
                    break;

                case DialogResult.No:
                    Application.Exit();
                    break;

                default:
                    break;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #region Przyciski A-Z
        private void buttonQ_Click(object sender, EventArgs e)
        {
            buttonQ.ForeColor = Color.Red;
            czyKoniec("Q");
            buttonQ.Enabled = false;
        }

        private void buttonW_Click(object sender, EventArgs e)
        {
            czyKoniec("W");
            buttonW.Enabled = false;
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            czyKoniec("E");
            buttonE.Enabled = false;
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "A";
            czyKoniec("A");
            buttonA.Enabled = false;
        }

        private void buttonT_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "T";
            czyKoniec("T");
            buttonT.Enabled = false;

        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "Y";
            czyKoniec("Y");
            buttonY.Enabled = false;
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "U";
            czyKoniec("U");
            buttonU.Enabled = false;
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "I";
            czyKoniec("I");
            buttonI.Enabled = false;
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "O";
            czyKoniec("O");
            buttonO.Enabled = false;
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "P";
            czyKoniec("P");
            buttonP.Enabled = false;
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "R";
            czyKoniec("R");
            buttonR.Enabled = false;
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "S";
            czyKoniec("S");
            buttonS.Enabled = false;
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "D";
            czyKoniec("D");
            buttonD.Enabled = false;

        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "F";
            czyKoniec("F");
            buttonF.Enabled = false;
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "G";
            czyKoniec("G");
            buttonG.Enabled = false;
        }

        private void buttonH_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "H";
            czyKoniec("H");
            buttonH.Enabled = false;
        }

        private void buttonJ_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "J";
            czyKoniec("J");
            buttonJ.Enabled = false;
        }

        private void buttonK_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "K";
            czyKoniec("K");
            buttonK.Enabled = false;
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "L";
            czyKoniec("L");
            buttonL.Enabled = false;
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "Z";
            czyKoniec("Z");
            buttonZ.Enabled = false;
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "X";
            czyKoniec("X");
            buttonX.Enabled = false;
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "C";
            czyKoniec("C");
            buttonC.Enabled = false;
        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "V";
            czyKoniec("V");
            buttonV.Enabled = false;

        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "B";
            czyKoniec("B");
            buttonB.Enabled = false;
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "N";
            czyKoniec("N");
            buttonN.Enabled = false;
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "M";
            czyKoniec("M");
            buttonM.Enabled = false;
        }
        #endregion





    }
}
