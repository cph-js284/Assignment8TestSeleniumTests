using System;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    public class Tests
    {
        IWebDriver wdriver;
        DirectoryInfo DI;
        string rootDir;
        
        [SetUp]
        public void Setup()
        {
           DI = new DirectoryInfo(Directory.GetCurrentDirectory()); 
           //really?! - .dll is place in bin/debug/netcoreapp2.2/
           rootDir = DI.Parent.Parent.Parent.FullName;
           wdriver = new FirefoxDriver(rootDir);
         
        }

        //navigate to home
        [Test]
        public void NavigateToHome_ReturnsHomeTitle()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080");
            string expected = "Home Page - mariospizzamongo";

            //waiting for page to load
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var actual = wdriver.Title;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/NavigateToHome_ReturnsHomeTitle.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
        }


        [Test]
        public void NavigateToPrivacy_ReturnsPrivacyTitle()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Home/Privacy");
            string expected = "Privacy Policy - mariospizzamongo";

            //waiting for page to load
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var actual = wdriver.Title;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/NavigateToPrivacy_ReturnsPrivacyTitle.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void NavigateToOrders_ReturnsOrdersTitle()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order");
            string expected = "Orders - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var actual = wdriver.Title;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/NavigateToOrders_ReturnsOrdersTitle.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void ShowAllOrderButtonNavigatesToPage_ReturnsAllOrdersPage()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order");
            string expected = "Orders - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var actual = wdriver.Title;
            //verify correct page
            Assert.AreEqual(expected,actual);

            //clicking the button on page
            wdriver.FindElement(By.Id("showallordersbutton_id")).Click();

            expected = "Show orders - mariospizzamongo";
            //waiting for page to load 
            wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            actual = wdriver.Title;


            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/ShowAllOrderButtonNavigatesToPage_ReturnsAllOrdersPage.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);


        }


        [Test]
        public void PlaceOrderButtonNavigatesToPage_ReturnsPlaceOrdersPage()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order");
            string expected = "Orders - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var actual = wdriver.Title;
            //verify correct page
            Assert.AreEqual(expected,actual);

            //clicking the button on page
            wdriver.FindElement(By.Id("placeorderbutton_id")).Click();

            expected = "Place order - mariospizzamongo";
            //waiting for page to load 
            wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            actual = wdriver.Title;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/PlaceOrderButtonNavigatesToPage_ReturnsPlaceOrdersPage.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);

        }


        [Test]
        public void PlaceOrderWithOnlyCustomerName_ReturnsUserErrorMessage()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";
            string userInput = "Random Customer Name";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            IWebElement customerNameField = wdriver.FindElement(By.Id("customername_id"));
            customerNameField.SendKeys(userInput);

            wdriver.FindElement(By.Id("submitorderbutton_id")).Click();

            string expectedErrMsg = "The Select pizza field is required.";
            var actualErrMsg = wdriver.FindElement(By.XPath("//span[@data-valmsg-for='OrderCSV']")).Text;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/PlaceOrderWithOnlyCustomerName_ReturnsUserErrorMessage.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expectedErrMsg,actualErrMsg);

        }


        [Test]
        public void AddOnePizzaToList_ReturnsCurrentOrderUpdatedWithNewPizza()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            selectElement.SelectByIndex(2);

            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            var actual = wdriver.FindElement(By.Id("currentorders_id")).Text;
            expected = "Pizza type 2";

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/AddOnePizzaToList_ReturnsCurrentOrderUpdatedWithNewPizza.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
            

        }

        [Test]
        public void AddManyPizzaToList_ReturnsCurrentOrderUpdatedWithNewPizza()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            //add a pizza to list
            selectElement.SelectByIndex(2);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(2);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(5);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(1);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();


            var actual = wdriver.FindElement(By.Id("currentorders_id")).Text;
            expected = "Pizza type 2\nPizza type 2\nPizza type 5\nPizza type 1";

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/AddManyPizzaToList_ReturnsCurrentOrderUpdatedWithNewPizza.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
        }


        [Test]
        public void PlaceOrderWithOnlyPizza_ReturnsUserErrorMessage()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            //add a pizza to list
            selectElement.SelectByIndex(2);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();


            wdriver.FindElement(By.Id("submitorderbutton_id")).Click();


            string expectedErrMsg = "The CustomerName field is required.";
            var actualErrMsg = wdriver.FindElement(By.XPath("//span[@data-valmsg-for='CustomerName']")).Text;

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/PlaceOrderWithOnlyPizza_ReturnsUserErrorMessage.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expectedErrMsg,actualErrMsg);
        }

        [Test]
        public void PlaceCorrectOrderWithOnePizza_ReturnsOrderAccepted()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";
            String userInput = "Some other Customer";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            //input customer name
            IWebElement customerNameField = wdriver.FindElement(By.Id("customername_id"));
            customerNameField.SendKeys(userInput);
            
            //select pizza from dropdown
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            //add a pizza to list
            selectElement.SelectByIndex(4);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();


            //submit order
            wdriver.FindElement(By.Id("submitorderbutton_id")).Click();

            //waiting for page to load 
            var wait2 = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait2.Until(d => d.Title.Contains("Orders - mariospizzamongo"));


            expected = "New order placed in system for customer : "+ userInput;
            var actual = wdriver.FindElement(By.Id("orderaccepted_id")).GetAttribute("innerHTML");

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/PlaceCorrectOrderWithOnePizza_ReturnsOrderAccepted.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
            
        }

        [Test]
        public void PlaceCorrectOrderWithManyPizzas_ReturnsOrderAccepted()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";
            String userInput = "Some other Rando";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            //input customer name
            IWebElement customerNameField = wdriver.FindElement(By.Id("customername_id"));
            customerNameField.SendKeys(userInput);
            
            //select pizza from dropdown
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            //add a pizza to list
            selectElement.SelectByIndex(4);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(1);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(3);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //submit order
            wdriver.FindElement(By.Id("submitorderbutton_id")).Click();

            //waiting for page to load 
            var wait2 = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait2.Until(d => d.Title.Contains("Orders - mariospizzamongo"));


            expected = "New order placed in system for customer : "+ userInput;
            var actual = wdriver.FindElement(By.Id("orderaccepted_id")).GetAttribute("innerHTML");

            //document result
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/PlaceCorrectOrderWithManyPizzas_ReturnsOrderAccepted.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expected,actual);
            
        }

        //since these test are not guaranteed to be executed in order, I have to start the removeorder test with 
        //by placing a fresh order in the system, so most of this test is reuse from the above testcode
        //This might not be the customer that ends up getting deleted since i just pick the first one from the list
        [Test]
        public void RemoveOrderFromSystem_ReturnsSystemWithOneLessOrder()
        {
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/PlaceOrder");
            string expected = "Place order - mariospizzamongo";
            String userInput = "Some customer to be deleted";

            //waiting for page to load 
            var wait = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains(expected));
            
            //input customer name
            IWebElement customerNameField = wdriver.FindElement(By.Id("customername_id"));
            customerNameField.SendKeys(userInput);
            
            //select pizza from dropdown
            var selectListPizza = wdriver.FindElement(By.Id("pizzaselect_id"));
            var selectElement = new SelectElement(selectListPizza);
            //add a pizza to list
            selectElement.SelectByIndex(4);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(1);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //add a pizza to list
            selectElement.SelectByIndex(3);
            wdriver.FindElement(By.Id("addpizzabutton_id")).Click();

            //submit order
            wdriver.FindElement(By.Id("submitorderbutton_id")).Click();

            //waiting for page to load 
            var wait2 = new WebDriverWait(wdriver, TimeSpan.FromSeconds(10));
            wait2.Until(d => d.Title.Contains("Orders - mariospizzamongo"));


            ///////////////////////////////////////////////////////////////////////
            //deleting order from system
            wdriver.Navigate().GoToUrl("http://localhost:8080/Order/ShowAllOrders");

            expected = wdriver.FindElement(By.Id("ordercount_id")).GetAttribute("innerHTML");
            int expnumber = int.Parse(expected) - 1;

            //SS before delete
            Screenshot ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/Before_RemoveOrderFromSystem_ReturnsSystemWithOneLessOrder.png", ScreenshotImageFormat.Png);


            //click the remove button
            wdriver.FindElement(By.Id("removebutton_id1")).Click();

            var actual = wdriver.FindElement(By.Id("ordercount_id")).GetAttribute("innerHTML");
            int actnumber = int.Parse(actual);

            //document result
            ss = ((ITakesScreenshot)wdriver).GetScreenshot();
            ss.SaveAsFile(rootDir+ "/Screenshots/After_RemoveOrderFromSystem_ReturnsSystemWithOneLessOrder.png", ScreenshotImageFormat.Png);

            wdriver.Close();
            Assert.AreEqual(expnumber,actnumber);
            
        }



    }
}