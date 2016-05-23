using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium;
using Utilities;
using MCOEDTestBase.SeleniumActions;
using System.Threading;

namespace AutomationTestBase
{
    /// <summary>
    /// Test Base class for all the test cases
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        // Configurable values

        private static string baseURL = null;
        private static string seleniumCommonFilesFolder = null;
        protected static MCOEDOperatingSystem operatingSystem;
        protected static MCOEDClientBrowser browser;
        protected IWebDriver driver;

        // Username and Password
        protected static string userName = null;
        protected static string password = null;

        /// <summary>
        /// Invoke the test first time
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            // Get the settings from the app.config.
            baseURL = ConfigurationManager.AppSettings["BaseURL"];
            seleniumCommonFilesFolder = ConfigurationManager.AppSettings["SeleniumCommonFilesFolder"];

            //  User Credentials
            userName = ConfigurationManager.AppSettings["UserName"]; 
            password = ConfigurationManager.AppSettings["Password"];


            // Get the browser from the app.config.
            if (!Enum.TryParse(ConfigurationManager.AppSettings["OperatingSystem"], out operatingSystem))
                throw new Exception("Unknown operating system specified in app.config.");

            // Get the browser from the app.config.
            if (!Enum.TryParse(ConfigurationManager.AppSettings["Browser"], out browser))
                throw new Exception("Unknown browser specified in app.config.");

            //  Launch browser
            TimeSpan timeSpan = new TimeSpan(0, 0, 30);
            switch (browser)
            {
                case MCOEDClientBrowser.Chrome:
                    driver = new ChromeDriver(seleniumCommonFilesFolder, new ChromeOptions(), timeSpan);
                    break;
                case MCOEDClientBrowser.Firefox:
                    driver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), timeSpan);
                    break;
                case MCOEDClientBrowser.InternetExplorer:
                    driver = new InternetExplorerDriver(seleniumCommonFilesFolder, new InternetExplorerOptions(), timeSpan);
                    CommonUtility.DeleteIECookiesAndData();
                    Thread.Sleep(2000);
                    break;
                case MCOEDClientBrowser.Safari:
                    driver = new SafariDriver();
                    break;
                default:
                    throw new Exception("Unknown browser");
            }

            driver.Navigate().GoToUrl("about:blank");
            driver.Manage().Window.Maximize();

            // Go to the url.
            driver.Navigate().GoToUrl(baseURL);
        }

        [TestCleanup]
        public virtual void TearDown()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }

}