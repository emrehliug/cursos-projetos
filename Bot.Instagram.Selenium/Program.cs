using OpenQA.Selenium;
using prmToolkit.Selenium;
using prmToolkit.Selenium.Enum;
using System;
using System.Configuration;
using System.Threading;

namespace Bot.Instagram.Selenium
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver webD = WebDriverFactory.CreateWebDriver(Browser.Chrome, @"D:\Projetos2020\Bots\Driver");

            try
            {
                webD.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                webD.LoadPage("https://www.instagram.com/accounts/login/");
                webD.SetText(By.Name("username"), ConfigurationManager.AppSettings["usuario"]);
                webD.SetText(By.Name("password"), ConfigurationManager.AppSettings["senha"]);

                webD.Submit(By.TagName("button"));
                Thread.Sleep(TimeSpan.FromSeconds(10));

                webD.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                webD.LoadPage("https://www.instagram.com/ionicclub/");

                IWebElement btnSeguir = null;

                try
                {
                    btnSeguir = webD.FindElement(By.XPath("//button[contains(text(), 'Seguir')]"));
                    btnSeguir.Click();
                }
                catch (NoSuchElementException ex)
                {
                    Console.WriteLine("Já esta seguindo usuario!");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                //webD.Close();
                //webD.Dispose();
            }

            Console.ReadKey();
        }
    }
}
