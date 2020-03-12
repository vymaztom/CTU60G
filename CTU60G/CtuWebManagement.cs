using System.Data;
using System.Threading;
using CTU60G.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CTU60G
{
    public class CTUWebManagement
    {
        private IWebDriver driver;
        private readonly WorkerOptions options;

        public CTUWebManagement(WorkerOptions options)
        {
            this.options = options;
            driver = new ChromeDriver(options.SeleniumDriverPath);
            //driver.Manage().Window.Minimize();
        }

        public void LogIn()
        {

            driver.Navigate().GoToUrl("https://60ghz.ctu.cz/prihlaseni");
            IWebElement element = driver.FindElement(By.Id("loginform-email"));
            element.SendKeys(options.CTULogin);
            element = driver.FindElement(By.Id("loginform-password"));
            element.SendKeys(options.CTUPass);
            element.Submit();
            Thread.Sleep(3000);

        }

        public void RegisterFixedStations(WirelessUnit antennaA, WirelessUnit antennaB, string name)
        {
            driver.Navigate().GoToUrl("https://60ghz.ctu.cz/vytvorit-fs");

            // connection name
            driver.FindElement(By.Id("station-a-name")).SendKeys(name);

            // gps A
            driver.FindElement(By.Id("station-a-lng")).SendKeys(antennaA.Lon);
            driver.FindElement(By.Id("station-a-lat")).SendKeys(antennaA.Lat);

            //gps B
            driver.FindElement(By.Id("station-b-lng")).SendKeys(antennaB.Lon);
            driver.FindElement(By.Id("station-b-lat")).SendKeys(antennaB.Lat);

            driver.FindElement(By.CssSelector("#coordinates-form > div > div:nth-child(7) > button")).Click();

            Thread.Sleep(3000);

            // Antenna A
            #region -A- Antenna technical

            //station antena volume [dB]
            driver.FindElement(By.Id("station-a-antenna_volume")).SendKeys("42");

            //station antena channel width [Mhz]
            driver.FindElement(By.Id("station-a-channel_width")).SendKeys("2160");

            //station antena power [dB]
            driver.FindElement(By.Id("station-a-power")).SendKeys("8");

            //station antena mean frequency [Mhz]
            driver.FindElement(By.Id("stationfs-a-frequency")).SendKeys(antennaA.Freq);

            // station antenna modulation selector
            driver.FindElement(By.Id("stationfs-a-ratio_signal_interference"));

            // antenna mac addres
            driver.FindElement(By.Id("station-a-macaddress")).SendKeys(antennaA.MacAddr.Replace(":", string.Empty));

            #endregion

            #region -B- Antenna technical

            //station antena volume [dB]
            driver.FindElement(By.Id("station-b-antenna_volume")).SendKeys("42");

            //station antena channel width [Mhz]
            driver.FindElement(By.Id("station-b-channel_width")).SendKeys("2160");

            //station antena power [dB]
            driver.FindElement(By.Id("station-b-power")).SendKeys("8");

            //station antena mean frequency [Mhz]
            driver.FindElement(By.Id("stationfs-b-frequency")).SendKeys(antennaB.Freq);

            // station antenna modulation selector
            driver.FindElement(By.Id("stationfs-b-ratio_signal_interference"));

            // antenna mac addres
            driver.FindElement(By.Id("station-b-macaddress")).SendKeys(antennaB.MacAddr.Replace(":", string.Empty));
            #endregion

            driver.FindElement(By.XPath("/html/body/main/div/form/div/div[3]/button")).Click();
            Thread.Sleep(3000);

            // empty table has 3 rows
            int overlapTableItems = driver.FindElement(By.XPath("/html/body/main/div/div[1]/div[4]/div[2]/div[1]/table")).FindElements(By.TagName("tr")).Count;
            if (overlapTableItems > 3) throw new ConstraintException("overlaping with another site");

            driver.FindElement(By.XPath("/html/body/main/div/div[2]/a[2]")).Click();
        }

        public void RegisterMultipointSite(WirelessSite site)
        {

        }
    }
}