using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UcakSavarOyunu.Properties;

namespace UcakSavarOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox ucakSavar = new PictureBox();
        PictureBox savasUcak = new PictureBox();    
        PictureBox mermi=new PictureBox();

        ArrayList mermiler = new ArrayList();
        ArrayList ucaklar = new ArrayList();

        private void Form1_Load(object sender, EventArgs e)
        {
            ucakSavar.Image = Resources.ucakSavarResim6;
            ucakSavar.Width = 152; ;
            ucakSavar.Height = 69;
            this.Controls.Add(ucakSavar);
            ucakSavar.Location = new Point(350, 420);

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ucaklar.Add(UcakUret());

            foreach(PictureBox item in ucaklar)
            {
                savasUcakHareket(item);
            }
        }

        private void savasUcakHareket(PictureBox savasUcak)
        {
            savasUcak.Image = Resources.savasUcakResim2;
            int x = savasUcak.Location.X;
            int y = savasUcak.Location.Y;

            y += 5;

            savasUcak.Location = new Point(x, y);
            this.Controls.Add(savasUcak);
        }

        private object UcakUret()
        {
            PictureBox savasUcak = new PictureBox();
            savasUcak.Image = Resources.savasUcakResim2;
            savasUcak.Width = 212;
            savasUcak.Height = 212;

            Random random = new Random();

            int savasUcakBaslat = random.Next(1000);

            savasUcak.Location = new Point(savasUcakBaslat, 0);

            timer3.Enabled = true;

            return savasUcak;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ucakSavarHareket(e);

            if(e.KeyCode == Keys.Space)
            {
                mermiler.Add(mermiUret());
                timer2.Enabled = true;
            }
            if(e.KeyCode == Keys.Escape)
            {
                MessageBox.Show("Skorunuz = " + sayi + " Tebrikler ");
                Close();
            }
        }

        private object mermiUret()
        {
            PictureBox mermi = new PictureBox();
            mermi.Image = Resources.mermi5;
            mermi.Width = 43;
            mermi.Height = 110;

            mermi.Location = new Point(ucakSavar.Location.X, ucakSavar.Location.Y);

            this.Controls.Add(mermi);

            return mermi;
        }

        private void ucakSavarHareket(KeyEventArgs e)
        {
           
            int ucakSavarKonumX = ucakSavar.Location.X;
            int ucakSavarKonumY = ucakSavar.Location.Y;

            if (e.KeyCode == Keys.Right)
                ucakSavarKonumX += 8;
            else if (e.KeyCode == Keys.Left)
                ucakSavarKonumX -= 8;

            ucakSavar.Location=new Point (ucakSavarKonumX, ucakSavarKonumY);
            this.Controls.Add(ucakSavar);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach(PictureBox item2 in mermiler)
            {
                mermiHareketEdiyor(item2);
            }
        }

        private void mermiHareketEdiyor(PictureBox mermi)
        {
            mermi.Image = Resources.mermi5;
            int mx= mermi.Location.X;
            int my = mermi.Location.Y;

            my=my - 5;

            mermi.Location=new Point (mx,my);
            this.Controls.Add(mermi);
        }

        PictureBox kaldirilanUcaklar = new PictureBox();
        PictureBox kaldirilanMermiler = new PictureBox();
        int sayi = 0;

        private void timer3_Tick(object sender, EventArgs e)
        {
            foreach(PictureBox item1 in mermiler)
            {
                foreach(PictureBox item2 in ucaklar)
                {
                    if (item1.Bounds.IntersectsWith(item2.Bounds))
                    {
                        this.Controls.Remove(item1);
                        this.Controls.Remove(item2);
                        kaldirilanMermiler = item1;
                        kaldirilanUcaklar = item2;
                        sayi++; 

                        label1.Text=sayi.ToString();
                    }
                }
            }
            mermiler.Remove(kaldirilanMermiler);
            ucaklar.Remove(kaldirilanUcaklar);
        }
    }
}
