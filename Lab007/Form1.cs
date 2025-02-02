using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Net;

namespace SileniumApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Height = 400;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl(@"https://google.com");
                IWebElement el = driver.FindElement(By.Name("q"));
            try
            {
                el.SendKeys("Hello world");
                el.SendKeys(OpenQA.Selenium.Keys.Enter);
                el = driver.FindElement(By.CssSelector("#rso > div:nth-child(1) > div > div > div > div.kb0PBd.cvP2Ce.A9Y9g.jGGQ5e > div > div > span > a > h3"));
                el.Click();
                el = driver.FindElement(By.CssSelector("#mw-content-text > div.mw-content-ltr.mw-parser-output > ul:nth-child(23) > li:nth-child(2) > a:nth-child(1)"));
                el.Click();
                el = driver.FindElement(By.CssSelector("#mw-content-text > div.mw-content-ltr.mw-parser-output > blockquote:nth-child(6) > div > p"));
                label1.Text = el.Text;
            }
            catch (WebDriverTimeoutException)
            {
                MessageBox.Show("Превышено время ожидания. Элемент не был найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                driver.Quit(); 
            }
          
           
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"https://youtube.com");
            IWebElement el;
            el = driver.FindElement(By.TagName("input"));
            el.SendKeys("Топлес");
            el.SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                wait.Until(d => d.Url.Contains("results?search_query="));

            System.Threading.Thread.Sleep(2000);

            string partialLinkText = "lsxp-bGb0y4"; 
            string xpathExpression = $"//a[contains(@href, '{partialLinkText}')]";

            IWebElement videoLink = wait.Until(d => d.FindElement(By.XPath(xpathExpression)));

            if (videoLink != null)
            {
                videoLink.Click();

                wait.Until(d => d.Url.Contains("watch?v="));

                MessageBox.Show("Видео успешно открыто!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Видео не найдено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                { driver.Quit();}
 
           


        }

        private void button3_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(@"https://google.com");
            IWebElement el = driver.FindElement(By.Name("q"));
            el.SendKeys("лес");
            el.SendKeys(OpenQA.Selenium.Keys.Enter);
            el = driver.FindElement(By.XPath("//*[@id=\"hdtb-sc\"]/div/div[1]/div[1]/div/div[2]/a/div"));
            el.Click();
            el = driver.FindElement(By.XPath("/html/body/div[3]/div/div[15]/div/div[2]/div[2]/div/div/div/div/div[1]/div/div/div[6]/div[2]/h3/a/div/div/div/g-img/img"));
            el.Click();
           string img = el.GetAttribute("src");
            if (string.IsNullOrEmpty(img))
            {
                img = el.GetAttribute("data-src"); 
            }
            if (!string.IsNullOrEmpty(img))
            {
              
                LoadImageFromBase64(img); 
            }
            else
            {
                MessageBox.Show("Не удалось получить URL изображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            driver.Quit();


        }



        public void LoadImageFromBase64(string base64String)
        {
            try
            {
             
                string base64Data = base64String.Substring(base64String.IndexOf(',') + 1);
                byte[] imageData = Convert.FromBase64String(base64Data);

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(ms);
                    pictureBox1.Image = image;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
                }
            }
            catch (FormatException formatEx)
            {
                MessageBox.Show($"Ошибка формата Base64: {formatEx.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
