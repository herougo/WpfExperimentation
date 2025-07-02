using FlaUI.Core;
using FlaUI.UIA3;
using FlaUI.Core.AutomationElements;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WpfNetFramework\\bin\\Debug\\WpfNetFramework.exe");
            // Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WindowsFormsApp\\bin\\Debug\\WindowsFormsApp.exe");
            Application _app = Application.Launch("D:\\Users\\Henri\\Doccuments\\GitHub\\WpfExperimentation\\WpfNetFramework\\WpfStartFromNet9\\bin\\Debug\\net9.0-windows\\WpfStartFromNet9.exe");
            try
            {
                AutomationBase _automation = new UIA3Automation();
                Window? _mainWindow = _app.GetMainWindow(_automation);

                var button = _mainWindow?.FindFirstDescendant(cf => cf.ByAutomationId("IncrementButton"))?.AsButton();
                Assert.AreNotEqual(button, null);

                button?.Invoke();

                var label = _mainWindow?.FindFirstDescendant(cf => cf.ByAutomationId("CountTextBox"))?.AsTextBox();
                Assert.AreEqual("1", label?.Text);
            }
            finally
            {
                _app.Close();
            }
        }
    }
}