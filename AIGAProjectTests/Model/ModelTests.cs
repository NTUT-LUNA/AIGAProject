using Microsoft.VisualStudio.TestTools.UnitTesting;
using AIGAProject.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model.Tests
{
    [TestClass()]
    public class ModelTests
    {
        Model model;

        [TestInitialize()]
        public void TestInitialize()
        {
            model = new Model();
        }

        [TestMethod()]
        public void LoadMapTest()
        {
            model.LoadMap();
            Assert.Fail();
        }
    }
}