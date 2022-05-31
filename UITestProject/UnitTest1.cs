using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace UITestProject

{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            IWebDriver webDriver = new ChromeDriver();

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));

            webDriver.Navigate().GoToUrl("https://edlus.lmt.lv/");
            webDriver.Manage().Window.Maximize();

            //Accept seleccted cookies 
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//html//body//section//div//div[2]//div//div[2]//button[2]"))).Click();
                   
            Thread.Sleep(800);   

            string title = webDriver.Title;
            Assert.AreEqual("LMT elektroniskā darbalaika uzskaite – EDLUS", title);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("funkcijas"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("sakums"))).Click();

            //5. and 6. verify visibility and click “Pieteikties konsultācijai”
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("kas-ir-edlus-pieteikties"))).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("kas-ir-edlus"))).Click();

            //set focus to second browser tab
            List<String> tabs2 = new List<String>(webDriver.WindowHandles);
            webDriver.SwitchTo().Window(tabs2[1]);

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("companyId2"))).SendKeys("Engineered Cat Robots Inc.");
            // here we dont have to wait for other fields to become visible
            webDriver.FindElement(By.Id("nameId2")).SendKeys("Jane");
            webDriver.FindElement(By.Id("surnameId2")).SendKeys("Doe");
            webDriver.FindElement(By.Id("phoneId2")).SendKeys("123123456");
            webDriver.FindElement(By.Id("emailId2")).SendKeys("ecr@ecr.org");
            webDriver.FindElement(By.Id("commentId2")).SendKeys("Quick brown fox jumps over the lazy dog");

            Actions action = new Actions(webDriver);
            action.MoveToElement(webDriver.FindElement(By.Id("checkbox-atruna"))).Click().Perform();

            webDriver.Quit();
        }
    }
}
