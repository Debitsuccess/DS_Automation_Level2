using AgeRangerWebUi.PageFactoryObjects;
using AgeRangerWebUi.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AgeRangerWebUi.Steps
{
    [Binding]
    public class AgeRangerUITestSteps : BaseClass
    {
        [Given(@"I am on Age Ranger Home Page")]
        public void GoToHomePage()
        {
            Driver.Navigate().GoToUrl(CommonConstants.ApplicationSettings.BaseUrl);
            Sleep(2000);
        }

        [When(@"I click Add New Person button")]
        public void AddNewPerson()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            pageObject.AddPerson.Click();
            Sleep(1000);
        }

        [When(@"I Submit the form")]
        public void SubmitForm()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            pageObject.SubmitButton.Click();
            Sleep(1000);
        }

        [When(@"I confirm the action$")]
        public void ConfirmAction()
        {
            var pageObject = new AgeRangerMainPage(Driver);
            pageObject.OkButton.Click();
            Sleep(1000);
        }

        [When(@"I enter (.*), (.*) and (.*) in the form")]
        public void EnterNewPersonDetails(String firstName, String lastName, String age)
        {
            var pageObject = new AgeRangerMainPage(Driver);
            if (!firstName.Equals("NoChange"))
            {
                pageObject.FirstName.Clear();
                pageObject.FirstName.SendKeys(firstName);
            }
            if (!lastName.Equals("NoChange"))
            {
                pageObject.LastName.Clear();
                pageObject.LastName.SendKeys(lastName);
            }
            if (!age.Equals("NoChange"))
            {
                pageObject.Age.Clear();
                pageObject.Age.SendKeys(age.ToString());
            }
        }

        [When(@"I delete created person (.*), (.*) and (.*)")]
        [Then(@"I delete created person (.*), (.*) and (.*)")]
        public void DeletePerson(String firstName, String lastName, String age)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            string firstLastName = (firstName + ' ' + lastName);
            bool userFound = false;
            IList<IWebElement> tableRow = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()))
                {
                    pageObject.DeletePerson.Click();
                    Sleep(2000);
                    ConfirmAction();
                    userFound = true;
                    break;
                }
            }
            Assert.True(userFound, "User not Found.");
        }

        [Then(@"I delete updated person (.*), (.*), (.*), (.*), (.*) and (.*)")]
        public void DeletePerson(String oldFirstName, String oldLastName, String oldAge, String firstName, String lastName, String age)
        {
            if (firstName.Equals("NoChange"))
            {
                firstName = oldFirstName;
            }
            if (lastName.Equals("NoChange"))
            {
                lastName = oldLastName;
            }
            if (age.Equals("NoChange"))
            {
                age = oldAge;
            }

            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            string firstLastName = (firstName + ' ' + lastName);
            bool userFound = false;
            IList<IWebElement> tableRow = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()))
                {
                    pageObject.DeletePerson.Click();
                    Sleep(2000);
                    ConfirmAction();
                    userFound = true;
                    break;
                }
            }
            Assert.True(userFound, "User not Found.");
        }

        [Given(@"I created a new person with (.*), (.*) and (.*)")]
        public void CreateNewPerson(String firstName, String lastName, String age)
        {
            GoToHomePage();
            AddNewPerson();
            EnterNewPersonDetails(firstName, lastName, age);
            SubmitForm();
            ConfirmAction();
        }

        public void SearchUsingFirstName(String firstName)
        {
            var pageObject = new AgeRangerMainPage(Driver);
            pageObject.SearchTextField.Clear();
            pageObject.SearchTextField.SendKeys(firstName);
            pageObject.SearchTextField.SendKeys(Keys.Enter);
            Sleep(2000);
        }

        [When(@"I update the (.*), (.*) and (.*) with (.*), (.*) and (.*)")]
        public void UpdatePerson(String oldFirstName, String oldLastName, String oldAge, String firstName, String lastName, String age)
        {
            //Search using First Name
            SearchUsingFirstName(oldFirstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (oldFirstName + " " + oldLastName);
            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(oldAge.ToString()))
                {
                    row.FindElements(By.TagName("a"))[0].Click();
                    Sleep(2000);
                    EnterNewPersonDetails(firstName, lastName, age);
                    break;
                }
            }
        }

        [Then(@"I should see (.*) and (.*) in the People view with correct (.*) instead of (.*), (.*) and (.*)")]
        public void UserExistVerification(String newFirstName, String newLastName, String newAge, String oldFirstName, String oldLastName, String oldAge)
        {
            if (newFirstName.Equals("NoChange"))
            {
                newFirstName = oldFirstName;
            }
            if (newLastName.Equals("NoChange"))
            {
                newLastName = oldLastName;
            }
            if (newAge.Equals("NoChange"))
            {
                newAge = oldAge;
            }

            // Search using First Name
            SearchUsingFirstName(newFirstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (newFirstName + " " + newLastName);
            bool userFound = false;

            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(newAge.ToString()))
                {
                    userFound = true;
                    break;
                }
            }

            Assert.True(userFound, "User not exists.");
        }

        [Then(@"I should see (.*) and (.*) in the People view with correct (.*) and the correct (.*)")]
        public void VerifyPerson(String firstName, String lastName, String age, String ageGroup)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            string firstLastName = (firstName + " " + lastName);
            bool userFound = false;
            IList<IWebElement> tableRow = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            foreach (IWebElement row in tableRow)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()) && row.Text.Contains(ageGroup))
                {
                    userFound = true;
                    break;
                }
            }
            Assert.True(userFound, "User not exists.");
        }

        [Then(@"I should not see (.*), (.*) and (.*) record anymore")]
        public void UserNotExistVerification(String firstName, String lastName, String age)
        {
            // Search using First Name
            SearchUsingFirstName(firstName);

            var pageObject = new AgeRangerMainPage(Driver);
            IList<IWebElement> tableRows = pageObject.PeopleTable.FindElements(By.TagName("tr"));
            string firstLastName = (firstName + " " + lastName);
            bool userFound = false;

            foreach (IWebElement row in tableRows)
            {
                if (row.Text.Contains(firstLastName) && row.Text.Contains(age.ToString()))
                {
                    userFound = true;
                    break;
                }
            }

            Assert.False(userFound, "User exists.");
        }
    }
}