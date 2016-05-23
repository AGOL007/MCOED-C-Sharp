using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCOEDTestBase.Validation
{
    public static class MCOEDValidation
    {
        private static IList<IWebElement> GetMapLayers(IWebDriver driver)
        {
            IWebElement layerContainer = driver.FindElement(By.TagName("svg"));
            IList<IWebElement> layers = layerContainer.FindElements(By.TagName("g"));

            return layers;
        }

        public static bool CheckOprationalLayersOnLoad(IWebDriver driver, List<string> oprationalLayersName)
        {
            int flag = 0;
            IList<IWebElement> layers = GetMapLayers(driver);

            foreach (string name in oprationalLayersName)
            {
                foreach (IWebElement layer in layers)
                {
                    if (layer.GetAttribute("id").ToString().Contains(name))
                    {
                        flag++;
                        break;
                    }
                }
                if (flag == oprationalLayersName.Count)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetPropertyClassFeature(IWebDriver driver, string rgbColor)
        {
            //  Get the map layers
            IList<IWebElement> layers = GetMapLayers(driver);

            //  Get All the point features 
            IList<IWebElement> pointFeaturs = new List<IWebElement>();

            //  Check circle features in each layer
            foreach (IWebElement item in layers)
            {
                //  Check for point features
                IList<IWebElement> features = item.FindElements(By.TagName("circle"));
                if (features.Count > 0)
                {
                    foreach (IWebElement singleFeature in features)
                    {
                        string colorcode = singleFeature.GetAttribute("fill").ToString();
                        if (string.Equals(" "+ colorcode, rgbColor))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        public static bool GetPropertyClassFeature1(IWebDriver driver, string rgbColor)
        {
            //  Get the map layers
            IList<IWebElement> layers = GetMapLayers(driver);

            //  Check circle features in each layer
            foreach (IWebElement item in layers)
            {
                //  Check for point features
                IList<IWebElement> features = item.FindElements(By.TagName("circle"));
                if (features.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
