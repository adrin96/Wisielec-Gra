using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wisielec
{
    public partial class Form2 : Form
    {
        bool zmienionyTekst = false;
        bool zmienonaKategoria = false;

        public static string KATEGORIA = "";
        public static string NAZWA = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            KATEGORIA = comboBoxKategoria.Text;
            NAZWA = textBoxNazwa.Text;


            if (KATEGORIA.Equals(""))
            {
                MessageBox.Show("Wybierz odpowiednią kategorię!", "UWAGA!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (NAZWA.Equals(""))
            {
                MessageBox.Show("Wprowadź nazwę!", "UWAGA!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (!Regex.IsMatch(NAZWA, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Wprowadź nazwę używając wyłącznie liter!", "UWAGA!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            Console.WriteLine("Wysyłam nazwe: {1} kategori {0}", KATEGORIA, NAZWA);

            this.Hide();
            Form1 form1 = new Form1(KATEGORIA,NAZWA);
            form1.Show();
            }
        }


        private void textBoxNazwa_MouseClick(object sender, MouseEventArgs e)
        {
            //po naciśnięciu na textboxNazwa usuwa tekst
            if (!zmienionyTekst)
            {
                textBoxNazwa.Text = "";
                zmienionyTekst = true;
            }

        }

        //po naciśnięciu na comboBoxKategoria usuwa tekst
        private void comboBoxKategoria_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Tekst z combobox : " + comboBoxKategoria.Text);
            if (!zmienonaKategoria)
            {
                comboBoxKategoria.Text = "";
                zmienonaKategoria = true;
            }
        }
    }
}
