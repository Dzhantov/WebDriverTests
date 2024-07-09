using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
    public class HandlingFormInputsTests
    {
        WebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void FormFields()
        {

            //click on "My Account" button
            driver.FindElements(By.XPath("//span[@class='ui-button-text']"))[2].Click();

            //click "Continue" button
            driver.FindElement(By.LinkText("Continue")).Click();

            //click on one of the radio buttons
            driver.FindElement(By.XPath("//input[@type='radio'][@value='m']")).Click();

            driver.FindElement(By.XPath("//*[@id=\"bodyContent\"]/form/div/div[2]/table/tbody/tr[2]/td[2]/input")).SendKeys("Teodor");
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='lastname']")).SendKeys("Dzhantov");
            driver.FindElement(By.Id("dob")).SendKeys("07/09/2024");

            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            string email = "test" + randomNumber.ToString() + "@abv.bg";

            driver.FindElement(By.Name("email_address")).SendKeys(email);

            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='company']")).SendKeys("Example company");

            //fill address fields 

            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='street_address']")).SendKeys("example street");
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='suburb']")).SendKeys("example suburb");
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='postcode']")).SendKeys("5800");
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='city']")).SendKeys("Pleven");
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='state']")).SendKeys("Pleven");

            //select from Dropdown
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            //fill contacts fields
            driver.FindElement(By.XPath("//td[@class='fieldValue']//input[@name='telephone']")).SendKeys("089898989898989");

            driver.FindElement(By.XPath("//input[@name='newsletter']")).Click();

            //fill password fields
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("secret1");
            driver.FindElement(By.XPath("//input[@name='confirmation']")).SendKeys("secret1");

            driver.FindElements(By.XPath("//span[@class='ui-button-icon-primary ui-icon ui-icon-person']//following-sibling::span"))[1].Click();




            //assert message for successfull registration

            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='bodyContent']//h1")).Text, "Your Account Has Been Created!");

            //click logout button
            driver.FindElement(By.LinkText("Log Off")).Click();
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Created Successfully!");
        }
    }
}