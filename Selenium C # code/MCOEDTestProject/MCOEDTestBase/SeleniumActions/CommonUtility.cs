using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCOEDTestBase.SeleniumActions
{
    public static class CommonUtility
    {
        // Time Out
        private static int timeOut = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOut"]);

        public static void FindElementDynamically(IWebDriver driver, string strParameter, string elementLocater = "ClassName")
        {
            switch (elementLocater)
            {
                case "ClassName":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.ClassName(strParameter)));
                    break;

                case "Id":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.Id(strParameter)));
                    break;

                case "TagName":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.TagName(strParameter)));
                    break;

                case "XPath":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath(strParameter)));
                    break;

                case "CssSelector":
                    new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.CssSelector(strParameter)));
                    break;
            }
        }

        /// <summary>
        /// Wait to display the map
        /// </summary>
        /// <param name="driver"></param>
        public static void WaitToDisplayMap(IWebDriver driver)
        {
            IWebElement splashScreenCloaseIcon = null;

            int i = 0;
            do
            {
                splashScreenCloaseIcon = driver.FindElement(By.ClassName("splashEnablecloseButton"));
                Thread.Sleep(1000);
                i++;
            } while (!(splashScreenCloaseIcon.Displayed) && (i < timeOut));
        }

        public static void DeleteIECookiesAndData()
        {
            Process cleanProcess = new Process();
            cleanProcess.StartInfo.UseShellExecute = false;
            cleanProcess.StartInfo.RedirectStandardOutput = true;
            cleanProcess.StartInfo.FileName = "RunDll32.exe";
            cleanProcess.StartInfo.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 2";
            cleanProcess.Start();
            cleanProcess.StandardOutput.ReadToEnd();
            cleanProcess.WaitForExit();
        }
    }
}
