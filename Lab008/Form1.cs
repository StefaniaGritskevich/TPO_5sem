using System;
using System.Drawing;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Xml.Linq;






namespace SileniumApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            try {
              
           
            driver.Navigate().GoToUrl(@"https://aliexpress.ru/");
            IWebElement element = driver.FindElement(By.Id("SearchText"));
            Thread.Sleep(3000);
            element.SendKeys("Сумка");
            element.SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            element = wait.Until(d => d.FindElement(By.CssSelector("div.snow-ali-kit_Informer__flexAligner__8lq3ka > button")));
      
            element.Click();

            element = wait.Until(d => d.FindElement(By.CssSelector(".RedFilter_LeftPanel__tagsContainer__1do33 > div > div:nth-child(5) > label")));
         
            element.Click();
            Thread.Sleep(3000);


                var allSpans = wait.Until(d => d.FindElements(By.CssSelector(".snippet-element_Element__item__1nwok2")));
                var spansBYN = allSpans.Where(span => span.Text.Contains("бесплатно")).Take(5).ToList();
                if (spansBYN.Count == 5)
                {
                        MessageBox.Show("Все элементы удовлетворяют условию.Тест пройден!");
                }
                else
                {
                    MessageBox.Show("Не все span-элементы с бесплатной доставкой. Тест не пройден");
                   
                }


            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Элемент не найден.");
            }
            catch (WebDriverTimeoutException)
            {
                MessageBox.Show("Тайм-аут: элементы не загружены за отведенное время.");
            }
            finally
            {                
                 driver.Quit();
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            string mainhref;
            try
            {
                driver.Navigate().GoToUrl(@"https://aliexpress.ru/");
                Thread.Sleep(2000);

                IWebElement button = driver.FindElement(By.XPath("//*[text()='Прекрасно']"));
                button.Click();

                IWebElement searchBox = driver.FindElement(By.Id("SearchText"));
                searchBox.SendKeys("doctor who");
                searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                var elems = driver.FindElements(By.CssSelector(".product-snippet_ProductSnippet__name__1llogj"));
                Thread.Sleep(6000);
                if (elems.Count > 0)
                {
                    IWebElement firstel = elems[0];
                    Thread.Sleep(3000);
                    firstel.Click();
                    mainhref = firstel.GetAttribute("href");

                }
                else
                {
                    MessageBox.Show("Товары не найдены.");
                    return;
                }

                Thread.Sleep(3000);

                var windowHandles = driver.WindowHandles;

                driver.SwitchTo().Window(windowHandles[windowHandles.Count - 1]);



                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement element = wait.Until(d => d.FindElement(By.CssSelector("#buyNowButton > div > div:nth-child(1) > button")));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element); 
                Thread.Sleep(3000);
                element = wait.Until(d => d.FindElement(By.CssSelector(".RedHeader_RedHeader__searchRowAligner__bos8u > nav:nth-child(3) > ul > li:nth-child(2) > button > div > a")));
                js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);


                element = wait.Until(d => d.FindElement(By.CssSelector(".SnowShoppingcartProductList_Product__imageBlock__nib2w > div.SnowShoppingcartProductList_ProductImage__container__v6doi > a")));


                string elementhref = element.GetAttribute("href");
                string idbox = FindId(elementhref);
                string mainid = FindId(mainhref);

                if (mainid == idbox)
                {
                    MessageBox.Show("Продукт верно добавлен в корзину. Тест пройден!");
                }
                else
                {
                    MessageBox.Show("Неверный продукт добавлен в корзину. Тест НЕ пройден!");
                }






                string FindId(string href)
                {
                    string idValue = string.Empty;

                    int idIndex = elementhref.IndexOf("id=");
                    if (idIndex != -1)
                    {

                        idIndex += 3;

                        int endIndex = elementhref.IndexOf("&", idIndex);
                        if (endIndex == -1) endIndex = elementhref.Length;


                        idValue = elementhref.Substring(idIndex, endIndex - idIndex);
                    }
                    return idValue;
                }
            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Элемент не найден.");
            }
            catch (WebDriverTimeoutException)
            {
                MessageBox.Show("Тайм-аут: элементы не загружены за отведенное время.");
            }
            finally
            {

                 driver.Quit();
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
              
                driver.Navigate().GoToUrl(@"https://aliexpress.ru/");
                IWebElement element = driver.FindElement(By.Id("SearchText"));
                Thread.Sleep(3000);
                element.SendKeys("Сумка");
                element.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(3000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                element = wait.Until(d => d.FindElement(By.CssSelector("div.snow-ali-kit_Informer__flexAligner__8lq3ka > button")));

                element.Click();

                element = wait.Until(d => d.FindElement(By.CssSelector(".RedFilter_PriceBlock__title__zi4e7 > div:nth-child(2) > div > label:nth-child(2) > input")));
         
                element.Click();
          

                element.SendKeys("30");
                element.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(3000);

                var allSpans = wait.Until(d => d.FindElements(By.CssSelector(".snippet-element_Element__item__1nwok2")));
                var spansBYN = allSpans.Where(span => span.Text.Contains("BYN")).Take(5).ToList();

                if (spansBYN.Count > 0)
                {
                    bool testFailed = false; 

                    foreach (var span in spansBYN)
                    {
                        
                        string textValue = span.Text.Replace("BYN", "").Trim();


                        if (double.TryParse(textValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                        {
                            if (value > 30)
                            {
                                MessageBox.Show($"Значение: {value} больше 30. Тест не пройден.");
                                testFailed = true; 
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Не удалось преобразовать значение: {textValue} в double.");
                        }
                    }

                    if (!testFailed)
                    {
                        MessageBox.Show("Все значения меньше или равны 30. Тест пройден.");
                    }
                }
                else
                {
                    MessageBox.Show("Не найдены span-элементы с 'BYN'.");
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
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();

            try
            {

                driver.Navigate().GoToUrl(@"https://aliexpress.ru/");
                Thread.Sleep(1000);
                IWebElement button = driver.FindElement(By.XPath("//*[text()='Прекрасно']"));
                button.Click();
                Thread.Sleep(1000);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement element = wait.Until(d => d.FindElement(By.CssSelector(".ShipToHeaderItem_ShipToHeaderItem__redHeaderContainer__1tq58 > div > div:nth-child(1) > div")));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);

                element = wait.Until(d => d.FindElement(By.CssSelector(".snow-ali-kit_Input__inputFieldWrapper__1aiyxh.snow-ali-kit_Input-M__rightLayout__1yvnrk > input")));
                 js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);
                element.SendKeys("YER");

                IWebElement YER = driver.FindElement(By.XPath("//*[text()='YER']"));
                YER.Click();

                Thread.Sleep(3000);




                var allSpans = wait.Until(d => d.FindElements(By.CssSelector(".snippet-element_Element__item__1nwok2")));
                var spansBYN = allSpans.Where(span => span.Text.Contains("YER")).Take(5).ToList();

                if (spansBYN.Count == 5)
                {

                    MessageBox.Show("Валюта изменена корректно. Тест пройден!");
                }
                else
                {
                    MessageBox.Show("Валюта изменена НЕ корректно. Тест НЕ пройден!");
                }







            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Элемент не найден.");
            }
            catch (WebDriverTimeoutException)
            {
                MessageBox.Show("Тайм-аут: элементы не загружены за отведенное время.");
            }
            finally
            {

                driver.Quit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            IWebDriver driver = new ChromeDriver();

            try
            {

                driver.Navigate().GoToUrl(@"https://www.ticketpro.by/");
                Thread.Sleep(1000);
                IWebElement button = driver.FindElement(By.XPath("//*[text()='Соглашаюсь']"));
                button.Click();


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                IWebElement element = wait.Until(d => d.FindElement(By.CssSelector(".header__content > div.header__right > div.header__login > div > div > div > a")));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", element);

                element = wait.Until(log => log.FindElement(By.Id("loginform-email")));
                element.Click();
                element.SendKeys("gritskevitchstefania@gmail.com");

                element = wait.Until(log => log.FindElement(By.Id("loginform-password")));
                element.Click();
                element.SendKeys("Pa$$w0rd");
                element = wait.Until(log => log.FindElement(By.TagName("button")));
                element.Click();
                var cookies = driver.Manage().Cookies.AllCookies;

                foreach (var cookie in cookies)
                {
                    MessageBox.Show($"Name: {cookie.Name}, Value: {cookie.Value}");
                }


            }
            catch (NoSuchElementException)
            {
                MessageBox.Show("Элемент не найден.");
            }
            catch (WebDriverTimeoutException)
            {
                MessageBox.Show("Тайм-аут: элементы не загружены за отведенное время.");
            }
            finally
            {

               // driver.Quit();
            }
        }
        }
}
