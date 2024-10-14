using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // İkonları temsil eden string listesi
        List<string> icons = new List<string>()
        {
            "!", ",", "b", "k", "v", "w", "z" , "N" , "K", "c", "a", "d", "e", "f", "g", "h", "i" , "j" , "o", "l",
            "!", ",", "b", "k", "v", "w", "z" , "N" , "K", "c", "a", "d", "e", "f", "g", "h", "i" , "j" , "o", "l"
        };

        Random rnd = new Random();
        Button first, second;
        Timer t = new Timer();
        Timer t2 = new Timer();
        int randomindex;

        // İki oyunculu sistem için değişkenler
        int oyuncu = 1; // İlk başta 1. oyuncu başlar
        int oyuncu1Puan = 0;
        int oyuncu2Puan = 0;

        public Form1()
        {
            InitializeComponent();

            t.Tick += T_Tick;
            t.Start();
            t.Interval = 5000; // 5 saniye
            show(); // Oyun başlangıcında butonları göster

            t2.Tick += T2_Tick; // Yanlış eşlemede ikonları gizleyecek zamanlayıcı
        }

        // Oyun başlarken tüm ikonları 5 saniyeliğine gösteriyoruz
        private void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            foreach (Control item in Controls) // Kontrolleri dolaşıyoruz
            {
                if (item is Button) // Sadece butonları işleme al
                {
                    Button btn = item as Button;
                    btn.ForeColor = btn.BackColor; // İkonları gizleme
                }
            }
        }

        // Yanlış eşleştirme durumunda butonları tekrar gizle
        private void T2_Tick(object sender, EventArgs e)
        {
            t2.Stop();
            first.ForeColor = first.BackColor;
            second.ForeColor = second.BackColor;
            first = null;
            second = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // Tüm butonlara rastgele ikon atama
        private void show()
        {
            foreach (Control item in Controls)
            {
                if (item is Button) // Sadece butonları işleme al
                {
                    Button btn = item as Button;
                    randomindex = rnd.Next(icons.Count);
                    btn.Text = icons[randomindex];
                    btn.ForeColor = Color.Black; // İlk başta ikonlar gösteriliyor
                    icons.RemoveAt(randomindex); // Atanan ikonu listeden siliyoruz
                }
            }
        }

        private void Buton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button; // Tıklanan buton

            // İlk butona tıklama
            if (first == null)
            {
                first = btn;
                first.ForeColor = Color.Black;
                return;
            }

            // İkinci butona tıklama
            second = btn;
            second.ForeColor = Color.Black;

            // Eşleşme kontrolü
            if (first.Text == second.Text)
            {
                // İkonlar eşleşiyorsa
                first.ForeColor = Color.Black;
                second.ForeColor = Color.Black;

                // Doğru eşleşen ikonların tekrar gizlenmesini engellemek için null yapıyoruz
                first = null;
                second = null;

                // Eşleşme olduğunda puan güncelleme ve oyuncu değişikliği
                if (oyuncu == 1)
                {
                    oyuncu1Puan++;
                    label1.Text = "1. Oyuncu Puanı: " + oyuncu1Puan;
                }
                else
                {
                    oyuncu2Puan++;
                    label2.Text = "2. Oyuncu Puanı: " + oyuncu2Puan;
                }

                // Oyun bitiş kontrolü
                if (KontrolOyunBitti())
                {
                    // Kazananın belirlenmesi
                    string kazanan;

                    if (oyuncu1Puan > oyuncu2Puan)
                    {
                        kazanan = "1. Oyuncu";
                    }
                    else if (oyuncu2Puan > oyuncu1Puan)
                    {
                        kazanan = "2. Oyuncu";
                    }
                    else
                    {
                        kazanan = "Berabere";
                    }

                    MessageBox.Show(kazanan + (kazanan == "Berabere" ? "!" : " kazandı!"));
                    Application.Exit(); // Oyunu kapatmak için
                }
            }
            else
            {
                // Eşleşme yoksa ikonlar 1 saniye sonra gizlenecek
                t2.Start();
                t2.Interval = 1000;

                // Oyuncu değiştirme
                if (oyuncu == 1)
                {
                    oyuncu = 2;
                    MessageBox.Show("Sıra 2. oyuncuda!");
                }
                else
                {
                    oyuncu = 1;
                    MessageBox.Show("Sıra 1. oyuncuda!");
                }

                label2.Text = "2. Oyuncu Puanı: " + oyuncu2Puan;
            }
        }

        // Oyun bitişini kontrol eden metot
        private bool KontrolOyunBitti()
        {
            // Kontrol etmek için toplam buton sayısını hesaplayın
            int toplamButonSayisi = 0;

            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    toplamButonSayisi++;
                }
            }

            // Eğer tüm butonlar eşleşmişse oyun bitmiştir
            return (oyuncu1Puan + oyuncu2Puan) == (toplamButonSayisi / 2);
        }


    }
}





