using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCOEDFunctionalTest;
using MCOEDTestBase.SeleniumActions;
using System.Threading;
using MCOEDTestBase.Validation;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace MCOEDFunctionalTest
{
    [TestClass()]
    public class FindParcel : TestProjectTestBase
    {
        //private TestContext context;
        //public TestContext TestContext
        //{
        //    get { return context; }
        //    set { context = value; }
        //}

        [TestMethod()]
        public void VerifyLayersOnLoad()
        {        
            //  Get the layers name from the config
            List<string> oprationalLayersName = new List<string>();
            oprationalLayersName = JS_SeleniumActions.GetOprationalLayersName(driver);

            //  Validate result
            bool check = MCOEDValidation.CheckOprationalLayersOnLoad(driver, oprationalLayersName);
            Assert.IsTrue(check);
        }

        [TestMethod()]
        //[DataSource("System.Data.OleDb", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\\\CSSLESXI20-VM22\\Users\\deepum\\Desktop\\MCOED Project\\TestMCOED.xlsx;Persist Security Info=False;Extended Properties='Excel 12.0 Xml;HDR=YES'", "MCOED$", DataAccessMethod.Sequential), TestMethod()]
        public void ValidatePropertyClass()
        {
            //string propertyClassLegendName = context.DataRow["PropertyClasses"].ToString();
            //  Click on advance search icon
            MCOEDSeleniumActions.ClickOnSearchIcon(driver);

            // Click to Unselect DeSelectALL checkbox
            MCOEDSeleniumActions.ClickOnDeSelectAllCheckBox(driver);

            // Click Ok button to close Info message pop up
            MCOEDSeleniumActions.ClickOkQuickTipsInfo(driver);

            // Get color code from property class panel
            string rgbColorValue = MCOEDSeleniumActions.PropertyClassFeatureGetLegendColor(driver, "Industrial");
            MCOEDSeleniumActions.ClickPropertyClassLegend(driver, "Industrial");

            // Validate the layer color with property class legend color
            Assert.IsTrue(MCOEDValidation.GetPropertyClassFeature(driver, rgbColorValue));  
        }

        [TestMethod()]
        public void ValidateSiteSpecificZone()
        {
            //  Click on advance search icon
            MCOEDSeleniumActions.ClickOnSearchIcon(driver);

            //  Click on tab
            MCOEDSeleniumActions.ClickOnAdvanceSearchTab(driver, "Property Class");
            Thread.Sleep(1000);

            //  Click on tab
            MCOEDSeleniumActions.ClickOnAdvanceSearchTab(driver, "Site Specific Incentives");

            // Click to Uncheck DeSelectALL checkbox
            MCOEDSeleniumActions.ClickOnDeSelectAllCheckBox(driver, false);

            // Click Ok button to close Info message pop up
            MCOEDSeleniumActions.ClickOkQuickTipsInfo(driver);

            MCOEDSeleniumActions.ClickPropertyClassLegend(driver, "Urban Enterprise Zone (UEZ)", false);

            MCOEDSeleniumActions.ClickOnAdvanceSearchViewGridIcon(driver, "Site Specific Incentives");
            
            MCOEDSeleniumActions.ClickParcelFromGrid(driver);

            MCOEDSeleniumActions.ClickOnFeature(driver);
        }
        
        //  Method which executes when the test script start execution
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            //  Enter the username and password
            MCOEDSeleniumActions.EnterUserCredentials(driver, userName, password);

            //  Click on splash close icon
            MCOEDSeleniumActions.ClickSplashCloseIcon(driver);

            //  Click on Quick Tips 'Ok' button
            MCOEDSeleniumActions.ClickOkQuickTipsInfo(driver);

        }

        //  Method which executes when testing script finishes execution
        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }
    }
}
