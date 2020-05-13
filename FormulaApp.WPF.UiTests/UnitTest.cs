using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace FormulaApp.WPF.UiTests
{
    [TestClass]
    public class UnitTest
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string WpfAppId = @"C:\Users\dhindrik\Source\Repos\dhindrik\FormulaApp\FormulaApp.WPF\bin\Debug\FormulaApp.WPF.exe";

        protected static WindowsDriver<WindowsElement> session;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", WpfAppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);
            }
        }

        [TestMethod]
        public async Task Test()
        {
            var btnPrev = session.FindElementByAccessibilityId("btnPrev");
            var btnNext = session.FindElementByAccessibilityId("btnNext");

            btnNext.Click();
            //await Task.Delay(100);
            btnNext.Click();
          //  await Task.Delay(100);
            btnPrev.Click();
          //  await Task.Delay(100);
            btnPrev.Click();
         //   await Task.Delay(100);
            btnPrev.Click();
        }
    }
}
