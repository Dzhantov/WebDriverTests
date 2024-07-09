using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DropdownManipulations
{
    public class Tests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void ExtractInformationFromDropddownOptions()
        {
            //creating the file
            string path = System.IO.Directory.GetCurrentDirectory() + "/manufacturers.txt";
            //map the dropdown element
            SelectElement dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

            IList<IWebElement> options = dropdown.Options;
            //var options = dropdown.Options;

            List<string> optionsAsString = new List<string>();

            foreach (var option in options)
            {
                optionsAsString.Add(option.Text);

            }
            optionsAsString.RemoveAt(0);

            foreach (var option in optionsAsString)
            {
                dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));
                dropdown.SelectByText(option);
                if(driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {option} has no products");
                }
                else
                {
                    //Create the table element
                    IWebElement productsTable = driver.FindElement(By.ClassName("productListingData"));

                    //Fetch all table rows
                    File.AppendAllText(path, $"\n\n The manufacturer {option} products are listed below --\n");

                    IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody/tr"));

                    foreach (var row in tableRows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }
                }
            }
            Assert.Pass();
        }
    }
}