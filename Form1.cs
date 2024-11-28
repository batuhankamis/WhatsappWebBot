using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WhatsappWebMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private IWebDriver driver;
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Tarayıcıyı Başlat";
            button2.Text = "Mesaj Gönderimini Başlat";
            button2.Enabled = false;
            // Form yüklenirken çalışacak kodlar
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // WhatsApp linki
            string whatsappLink = "https://web.whatsapp.com/";

            // ChromeDriver'ı başlat
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-maximized");
            driver = new ChromeDriver(chromeOptions);

            try
            {
                // WhatsApp linkine gidin
                driver.Navigate().GoToUrl(whatsappLink);

                // Sayfanın yüklenmesini bekleyin
                Thread.Sleep(5000); // İsteğe bağlı olarak sayfanın yüklenmesini bekleyebilirsiniz
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını göster
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string mesajDizi = "90telno#Mesaj ~ 90telno#Mesaj"; // TelefonNo#Mesaj~TelefonNo2#Mesaj2.......
                string[] mesajlar = mesajDizi.Split('~');
                int kayitSayisi = 1;
                foreach (var item in mesajlar)
                {
                    string[] numaraVeIcerik = item.Split('#');


                    IWebElement YeniSohbet = driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/div[3]/header[1]/header[1]/div[1]/span[1]/div[1]/span[1]/div[1]/div[1]/span[1]"));
                    YeniSohbet.Click();
                    Thread.Sleep(2000); // İsteğe bağlı olarak sayfanın yüklenmesini bekleyebilirsiniz
                    IWebElement AramaAlani = driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/span[1]/div[1]/span[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/p[1]"));
                    Thread.Sleep(2000); // İsteğe bağlı olarak sayfanın yüklenmesini bekleyebilirsiniz
                    AramaAlani.SendKeys(numaraVeIcerik[0]); //Telefonu arıyor
                    Thread.Sleep(2000); // İsteğe bağlı olarak sayfanın yüklenmesini bekleyebilirsiniz
                    AramaAlani.SendKeys(OpenQA.Selenium.Keys.Enter);
                    Thread.Sleep(2000); // İsteğe bağlı olarak sayfanın yüklenmesini bekleyebilirsiniz
                    IWebElement body = driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/div[4]/div[1]/footer[1]/div[1]/div[1]/span[2]/div[1]/div[2]/div[1]/div[1]/div[1]/p[1]"));
                    body.SendKeys(numaraVeIcerik[1]);
                    body.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Enter);
                    body.SendKeys(numaraVeIcerik[2]);
                    body.SendKeys(OpenQA.Selenium.Keys.Enter);

                    kayitSayisi++;
                }

                // İşlem başarılı mesajı
                MessageBox.Show("Tüm Mesajlar Gönderildi! (" + kayitSayisi + " adet)");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını göster
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Uygulama kapatıldığında tarayıcıyı kapat
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}



