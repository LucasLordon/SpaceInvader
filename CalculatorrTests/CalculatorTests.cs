using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculatorr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculatorr.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddTest()
        {
            //Arrange
            int x = 8;
            int y = 12;
            Calculator calc = new Calculator();

            //Act
            int z = calc.Add(x, y);

            //Assert
            Assert.AreEqual(x + y, z);
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            //Arrange
            int x = 8;
            int y = 12;
            Calculator calc = new Calculator();

            //Act
            int z = calc.Multiply(x, y);

            //Assert
            Assert.AreEqual(x * y, z);
        }
    }
}