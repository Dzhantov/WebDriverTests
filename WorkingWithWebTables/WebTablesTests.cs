using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTables
{
    public class WebTablesTests
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
        public void WorkingWithTableElements()
        {

            //locate the table
            IWebElement productsTable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));
            //locate tables' elements and keep them in 1 variable
            ReadOnlyCollection <IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody//tr"));

            //creating CSV file
            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if(File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (IWebElement row in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath(".//td"));

                foreach (var tData in tableData)
                {
                    string data = tData.Text;
                    string[] productInfo = data.Split("\n");

                    File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");

                }
            }

            Assert.IsTrue(File.Exists(path));
            Assert.IsTrue(new FileInfo(path).Length > 0);
        }
    }
}