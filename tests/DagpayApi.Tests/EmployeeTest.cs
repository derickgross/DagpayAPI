using System;
using Xunit;
using DagpayApi.Models;

namespace DagpayApi.Tests
{
    public class EmployeeTest
    {
        [Fact]
        public void CalculatesStandardDeduction()
        {
            // Arrange
            Employee employee = new Employee("Derick", "Gross", 3, "Development");

            // Act

            // Assert
            Assert.Equal("38.46", employee.Deduction.ToString());
        }
    }
}
