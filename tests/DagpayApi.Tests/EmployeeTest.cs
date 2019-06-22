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
            decimal expectedDeduction = 38.46M;

            // Act

            // Assert
            Assert.Equal(expectedDeduction, employee.Deduction);
        }
    }
}
