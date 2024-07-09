using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculatorproject
{
    public class Tests
    {
        WebDriver driver;
        IWebElement textBoxNumber1;
        IWebElement textBoxNumber2;
        IWebElement dropdownoperations;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement divResults;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [SetUp]
        public void Setup()
        {
            textBoxNumber1 = driver.FindElement(By.Id("number1"));
            textBoxNumber2 = driver.FindElement(By.XPath("//input[@id='number2']"));
            dropdownoperations = driver.FindElement(By.Id("operation"));
            calculateButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.XPath("//input[@value='Reset']"));
            divResults = driver.FindElement(By.Id("result"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void PerformTestLogic(string firstNumber, string secondNumber,
            string operation, string expected)
        {
            resetButton.Click();

            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxNumber1.SendKeys(firstNumber);
            }
            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxNumber2.SendKeys(secondNumber);
            }
            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropdownoperations).SelectByText(operation);
            }

            calculateButton.Click();

            Assert.That(divResults.Text, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("5", "+ (sum)", "10", "Result: 15")]
        [TestCase("5", "+ (sum)", "5", "Result: 10")]
        public void Test(string firstNumber, string operation, string secondNumber, string expected)
        {

            PerformTestLogic(firstNumber, secondNumber, operation, expected);

        }
    }
}