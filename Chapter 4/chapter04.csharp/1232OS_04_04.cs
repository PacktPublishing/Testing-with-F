using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SUT = Company.System.BL.Calculators;

namespace Company.System.Tests.BL.Calculators.PriceCalculator
{
    public class GetPrice
    {
        [Test]
        public void ShouldReturnZeroOnEmptyOrder()
        {
            // Arrange
            var priceCalculator = new SUT.PriceCalculator();
            var order = new { };
            
            // Act
            var result = priceCalculator.GetPrice(order);

            // Assert
            Assert.That(result, Is.EqualTo(0), "Expecting zero price when order is empty");
        }
    }
}
