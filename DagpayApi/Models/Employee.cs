﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DagpayApi.Helpers;

namespace DagpayApi.Models
{
    public class Employee
    {
        public Employee(string firstName, string lastName, int experience, string department, int cost = 1000, int biweeklySalary = 2000)
        {
            FirstName = firstName;
            LastName = lastName;
            Experience = experience;
            Department = department;
            Cost = cost;
            BiweeklySalary = biweeklySalary;
            DiscountFactor = DeductionHelpers.CalculateDiscountFactor(firstName);
        }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public int BiweeklySalary { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public decimal Deduction 
        {
            get
            {
                return DeductionHelpers.CalculateDeduction(Cost, DiscountFactor);
            }
        }

        [Required]
        public int DiscountFactor { get; set; }

        public List<Dependent> Dependents { get; set; }
    }
}