using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumBasicsDemo;
using System;
using TechTalk.SpecFlow;

namespace SpecflowTests.Steps
{
    [Binding]
    public class NWSteps
    {
        private IWebDriver driver;

        [Given(@"I open ""(.*)"" url")]
        public void GivenIOpenUrl(string url)
        {
            driver = new ChromeDriver();
            driver.Url = url;
        }
        
        [When(@"I login with ""(.*)"" username and ""(.*)"" password")]
        public void WhenILoginWithUsernameAndPassword(string login, string password)
        {
            new LoginPage(driver).Login(login, password);
        }
        
        [Then(@"Login is successfull")]
        public void ThenLoginIsSuccessfull()
        {
            Assert.IsTrue(new MainPage(driver).isLoginSuccessfull(), "Login failed");
        }

        [Then(@"Login is failed")]
        public void ThenLoginIsFailed()
        {
            Assert.IsFalse(new MainPage(driver).isLoginSuccessfull(), "Login successfull for invalid credentials");
        }

       
        [When(@"I click All product link")]
        public void IClickAllProductLink()
        {
            driver.FindElement(By.LinkText("All Products")).Click();
        }

        [Then(@"open All product page")]
        public void ThenOpenAllProductPage()
        {
            Assert.IsTrue(new MainPage(driver).AllProductsPageOpen(), "All Product failed");
        }

        [When(@"I click Create new")]
        public void WhenIClickCreateNew()
        {
            driver.FindElement(By.LinkText("Create new")).Click();
        }

        [Then(@"open Product editing page")]
        public void ThenOpenProductEditingPage()
        {
            Assert.IsTrue(new MainPage(driver).isProductEditing(), "Product editing open failed");
        }
        [Then(@"create New Product")]
        public void ThenCreateNewProduct()
        {
            driver.FindElement(By.Id("ProductName")).SendKeys("Test product");
            driver.FindElement(By.XPath("//option[. = 'Beverages']")).Click();
            driver.FindElement(By.XPath("//option[. = 'Tokyo Traders']")).Click();
            driver.FindElement(By.Id("UnitPrice")).SendKeys("1");
            driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("2");
            driver.FindElement(By.Id("UnitsInStock")).SendKeys("3");
            driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("4");
            driver.FindElement(By.Id("ReorderLevel")).SendKeys("5");
            driver.FindElement(By.CssSelector(".btn")).Click();
        }

        [Then(@"I check product exist")]
        public void ThenICheckProductExist()
        {
            driver.FindElement(By.LinkText("Test product")).Click();
            Assert.AreEqual(driver.FindElement(By.Id("ProductName")).GetAttribute("value"), "Test product");
            Assert.AreEqual(driver.FindElement(By.XPath("//option[. = 'Beverages']")).GetAttribute("text"), "Beverages");
            Assert.AreEqual(driver.FindElement(By.XPath("//option[. = 'Tokyo Traders']")).GetAttribute("text"), "Tokyo Traders");
            Assert.AreEqual(driver.FindElement(By.Id("UnitPrice")).GetAttribute("value"), "1,0000");
            Assert.AreEqual(driver.FindElement(By.Id("QuantityPerUnit")).GetAttribute("value"), "2");
            Assert.AreEqual(driver.FindElement(By.Id("UnitsInStock")).GetAttribute("value"), "3");
            Assert.AreEqual(driver.FindElement(By.Id("UnitsOnOrder")).GetAttribute("value"), "4");
            Assert.AreEqual(driver.FindElement(By.Id("ReorderLevel")).GetAttribute("value"), "5");
            driver.FindElement(By.LinkText("All Products")).Click();
        }

        [Then(@"delete new product")]
        public void ThenDeleteNewProduct()
        {
            driver.FindElement(By.XPath("//tr[79]//td[12]//a[1]")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        [Then(@"Logout")]
        public void ThenLogout()
        {
            driver.FindElement(By.XPath(".//*[text()='Logout']")).Click();
            Assert.IsTrue(new MainPage(driver).isLogoutSuccessfull(), "Logout failed");
        }

    }
}
