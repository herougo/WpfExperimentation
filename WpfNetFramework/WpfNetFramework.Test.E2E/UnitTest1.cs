using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace WpfNetFramework.Test.E2E
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WpfNetFramework\\bin\\Debug\\WpfNetFramework.exe");
            // Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WindowsFormsApp\\bin\\Debug\\WindowsFormsApp.exe");
            // Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WpfStartFromNet9\\bin\\Debug\\net9.0-windows\\WpfStartFromNet9.exe");
            try
            {
                AutomationBase _automation = new UIA3Automation();
                Window _mainWindow = _app.GetMainWindow(_automation);

                var button = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("IncrementButton"))?.AsButton();
                Assert.AreNotEqual(button, null);

                button.Invoke();

                var label = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("CountTextBox"))?.AsTextBox();
                Assert.AreEqual("1", label.Text);
            }
            finally 
            {
                _app.Close();
            }
        }
    }
}
