# Dagpay

Welcome to Dagpay, a custom app that allows users to track payroll benefits deductions for employees and their dependents.  Dagpay consists of a React single page app and C# Azure RESTful JSON web API supported by an Azure SQL database.

## Dagpay front-end client

Dagpay React is hosted on Heroku:

**_http://dagpay.herokuapp.com/_**

View the code for Dagpay React here:

**_https://github.com/derickgross/Dagpay-React_**

The client has four sections:

**Total Deductions** - the sum of deductions for all employees and dependents, which is updated as new beneficiaries are added.

**Beneficiaries** - a list of all employees, their individual deductions, and the sum of deductions for their dependents (click an employee to view individual deductions for dependents).

**Add New Employee** - provide a unique numeric Employee Id, First Name, and Last Name

**Add New Dependent** - provide a First Name and Last Name, and select an associated employee

## Dagpay API

Use the following endpoints to interact with the Dagpay C# Azure Functions API:

**_https://dagpayapi.azurewebsites.net/api/employee_** - GET to get all employees (and their dependents), and POST to add a new employee

**_https://dagpayapi.azurewebsites.net/api/dependent_** - POST to add a new dependent, GET to get all dependents



_* Based on total deductions of $1000/year for each employee and $500/year for each dependent, Dagpay calculates the portion owed for each of 26 biweekly pay periods in a year. Beneficiaries whose first names begin with 'A' receive a 10% discount._
