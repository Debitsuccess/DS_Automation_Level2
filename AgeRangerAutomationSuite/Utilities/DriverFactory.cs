using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace AgeRangerWebUi.Utilities
{
    public class DriverFactory
    {
        public static IWebDriver InitiateWebDriver(string browser)
        {
            IWebDriver driver = null;
            if (browser.Equals(CommonConstants.DriverSettings.ChromeBrowser))
            {
                driver = new ChromeDriver(CommonConstants.DriverSettings.BinaryLocation);
            }
            else if (browser.Equals(CommonConstants.DriverSettings.FireFoxBrowser))
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                service.FirefoxBinaryPath = CommonConstants.DriverSettings.BinaryLocation;
                driver = new FirefoxDriver(service);
            }
            else if (browser.Equals(CommonConstants.DriverSettings.FireFoxBrowser))
            {
                driver = new InternetExplorerDriver(CommonConstants.DriverSettings.BinaryLocation);
            }


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(CommonConstants.DriverSettings.DefaultWaitTime);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
