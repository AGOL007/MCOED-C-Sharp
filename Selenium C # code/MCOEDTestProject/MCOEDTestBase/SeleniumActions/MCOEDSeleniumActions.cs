using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCOEDTestBase.SeleniumActions
{
    public static class MCOEDSeleniumActions
    {
        #region Login

        /// <summary>
        /// Enter the User login crdentials
        /// </summary>
        /// <param name="driver">Selenium web driver</param>
        /// <param name="userName">Username</param>
        /// <param name="password">User Password</param>
        public static void EnterUserCredentials(IWebDriver driver, string userName, string password)
        {
            //  Wait for login page
            CommonUtility.FindElementDynamically(driver, "sign-in-box", "Id");
            IWebElement loginPanel = driver.FindElement(By.Id("sign-in-box"));

            //  Enter username
            IWebElement mcoedEmail = loginPanel.FindElement(By.Id("email"));
            mcoedEmail.SendKeys(userName);

            //  Enter password
            IWebElement mcoedPwd = loginPanel.FindElement(By.Id("password"));
            mcoedPwd.SendKeys(password);

            //  Click on Login button
            IWebElement loginButton = loginPanel.FindElement(By.ClassName("btnLogin"));
            loginButton.Click();

        }
        #endregion

        #region Splash screen
        public static void ClickSplashCloseIcon(IWebDriver driver)
        {
            IWebElement splashCloseIcon = null;
            try
            {
                splashCloseIcon = driver.FindElement(By.ClassName("splashEnablecloseButton"));
            }
            catch (Exception)
            {

                if (driver is InternetExplorerDriver || driver is ChromeDriver)
                {
                    CommonUtility.FindElementDynamically(driver, "splashEnablecloseButton");
                }

                Thread.Sleep(7000);
                splashCloseIcon = driver.FindElement(By.ClassName("splashEnablecloseButton"));
            }

            splashCloseIcon.Click();
        }

        public static void ClickOkQuickTipsInfo(IWebDriver driver)
        {
            CommonUtility.FindElementDynamically(driver, "alert-content");
            if (driver is ChromeDriver || driver is InternetExplorerDriver)
                Thread.Sleep(1000);

            IWebElement quickInfoPanel = driver.FindElement(By.ClassName("alert-content"));
            IWebElement okBtnQuickTipInfo = quickInfoPanel.FindElement(By.ClassName("blue-button1"));

            okBtnQuickTipInfo.Click();
        }
        #endregion

        #region Advance Search

        public static void ClickOnSearchIcon(IWebDriver driver)
        {
           Thread.Sleep(2000);
           CommonUtility.FindElementDynamically(driver, "esriCTHeaderAdvanceSearch");

            IWebElement AdvancesearchIcon = driver.FindElement(By.ClassName("esriCTHeaderAdvanceSearch"));
            AdvancesearchIcon.Click();
        }

      // Returns the ColorCode value of all Property class legend.
        public static string PropertyClassFeatureGetLegendColor(IWebDriver driver, string featureCatogery)
        {
            string colorCode = null;

            IWebElement legendPanel = driver.FindElement(By.ClassName("findParcelTitlepaneContentDiv"));
            IList<IWebElement> legendList = legendPanel.FindElements(By.ClassName("parcelContainer"));

            foreach (IWebElement item in legendList)
            {
                IWebElement legend = item.FindElement(By.TagName("label"));
                if (string.Equals(legend.Text, featureCatogery))
                {
                    IWebElement colorLegend = item.FindElement(By.ClassName("parcelLegend"));
                    colorCode = colorLegend.GetAttribute("style").Split(':')[1].TrimEnd(new Char[] { ';' });
                    return colorCode;
                }
            }

            return colorCode;
        }

        //Click Property class checkbox
        public static void ClickPropertyClassLegend(IWebDriver driver, string featureCatogery, bool propertyClass = true)
        {
            IList<IWebElement> legendPanel = driver.FindElements(By.ClassName("findParcelTitlepaneContentDiv"));
            IList<IWebElement> legendList = null;

            if (propertyClass)
            {
                legendList = legendPanel[0].FindElements(By.ClassName("parcelContainer"));
            }
            else
            {
                legendList = legendPanel[1].FindElements(By.ClassName("parcelContainer"));
            }

            foreach (IWebElement item in legendList)
            {
                IWebElement legend = item.FindElement(By.TagName("label"));
                if (string.Equals(legend.Text, featureCatogery))
                {
                    IWebElement CheckLegend = item.FindElement(By.TagName("input"));
                    CheckLegend.Click();
                    Thread.Sleep(5000);
                    break;
                }
            }
        }

        //Click to uncheck DeselectAllCheckBox
        public static void ClickOnDeSelectAllCheckBox(IWebDriver driver, bool PropertyClassCheck = true)
        {
            CommonUtility.FindElementDynamically(driver, "checkAllTypes");
            Thread.Sleep(1000);

            IList<IWebElement> headerCheckboxLabel = driver.FindElements(By.ClassName("checkAllTypes"));

            if (PropertyClassCheck)
            {
                headerCheckboxLabel[0].Click();
            }
            else
            {
                headerCheckboxLabel[1].Click();
            }
            Thread.Sleep(5000);
        }
  
        #endregion

        #region Common

        public static void ClickOnAdvanceSearchTab(IWebDriver driver, string tabName)
        {
            CommonUtility.FindElementDynamically(driver, "advanceSearchPanel");

            IWebElement advanceSearchPanel = driver.FindElement(By.ClassName("advanceSearchPanel"));
            IList<IWebElement> searchTabs = advanceSearchPanel.FindElements(By.ClassName("dijitTitlePaneTitleFocus"));;

            foreach (IWebElement item in searchTabs)
            {
                if (string.Equals(item.Text, tabName))
                {
                    item.Click();
                    break;
                }
            }
        }
        
        // Click view detail grid icon
        public static void ClickOnAdvanceSearchViewGridIcon(IWebDriver driver, string tabName)
        {
            CommonUtility.FindElementDynamically(driver, "advanceSearchPanel");
            IWebElement advanceSearchPanel = driver.FindElement(By.ClassName("advanceSearchPanel"));
            IList<IWebElement> searchTabIcons = advanceSearchPanel.FindElements(By.ClassName("dijitTitlePaneTitle"));

            foreach(IWebElement item in searchTabIcons)
            {
                string actualTabName = item.FindElement(By.ClassName("dijitTitlePaneTitleFocus")).Text;
                if (string.Equals(actualTabName, tabName))
                {
                    IWebElement viewGrid = item.FindElement(By.ClassName("divContentGrid"));
                    viewGrid.Click();
                    Thread.Sleep(2000);
                    break;
                }
            }

        }

        public static void ClickParcelFromGrid(IWebDriver driver, int rowIndex = 0, int cellIndex = 1)
        {
            CommonUtility.FindElementDynamically(driver, "dgrid-content");

            IWebElement gridContainer = driver.FindElement(By.ClassName("dgrid-content"));
            IList<IWebElement> gridRecord = gridContainer.FindElements(By.TagName("tr"));

           if(gridRecord != null && gridRecord.Count > 0)
           {
               IList<IWebElement> gridCell = gridRecord[rowIndex].FindElements(By.TagName("td"));
               gridCell[cellIndex].Click();

               Thread.Sleep(3000);
           }        

        }

        public static void ClickOnFeature(IWebDriver driver, string featureId = "selectedPolygonLayer_layer", string featureTagName = "path", int featureIndex = 0)
        {
            IWebElement layerContainer = driver.FindElement(By.TagName("svg"));
            IWebElement layerByID = layerContainer.FindElement(By.Id(featureId));

            IList<IWebElement> selectedFeatures = layerByID.FindElements(By.TagName(featureTagName));
            selectedFeatures[featureIndex].Click();

            Thread.Sleep(1000);
        }
        #endregion
    }
}
