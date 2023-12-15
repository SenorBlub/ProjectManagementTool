using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestContainer
    {


        [TestMethod] 
        public void ValidateCorrectInput()
        {
            ProjectValidator.Validate();
        }

        [TestMethod]
        public void ValidateNoAssignees()
        {

        }

        [TestMethod]
        public void ValidateNoTasks()
        {

        }

        [TestMethod]
        public void ValidateNoGoal()
        {

        }

        [TestMethod]
        public void ValidateNoDescription()
        {

        }

        [TestMethod]
        public void ValidateNoGuid()
        {

        }
    }
}
