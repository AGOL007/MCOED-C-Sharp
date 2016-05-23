using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCOEDTestBase.SeleniumActions
{
    public static class JS_SeleniumActions
{
        public static List<string> GetOprationalLayersName(IWebDriver driver)
        {
            List<string> layersName = new List<string>();
            int oprationalLayerCount = 0;

            oprationalLayerCount = Convert.ToInt32(((IJavaScriptExecutor)driver).ExecuteScript("return appGlobals.configData.OperationalLayers.length"));

            for (int i = 0; i < oprationalLayerCount; i++)
            {
                string layerName = Convert.ToString((((IJavaScriptExecutor)driver)).ExecuteScript("return appGlobals.configData.OperationalLayers[" + i + "].LayerName"));
                layersName.Add(layerName);
            }

            return layersName;
        }
    }
}
