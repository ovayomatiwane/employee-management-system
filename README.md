Task Description
1. Management often needs to hire consultants to lessen the workload over the festive periods.
2. You are asked to assist the management team with a consultant’s role-based salary calculation
system.
3. Management assigns consultants to tasks and every task has its own duration.
4. A consultant can be assigned multiple tasks but cannot work more than 12 hours a day.
5. Consultant’s salary is calculated based on the role they were assigned to.
6. Consultant’s roles are categorized into the following levels and each role has a Rate per hour:

a. Consultant Level 1
b. Consultant Level 2

7. Management needs to be able to capture the total hours worked for each consultant for a specific
day.
8. Changing the hourly rate or changing the consultant role should not affect previously captured
hours.
9. Management needs to be able to do the following:

a. Create and Edit consultants and assign a Role.
b. Capture/Store consultant Profile Image.
c. Create and Edit Employee Roles.
d. Change Employee Role rate per hour.
e. Create Tasks.
f. Assign consultant to one or more Tasks.
g. A Task can have multiple consultants assigned.
h. View total due to consultant over a specific timeframe.
i. Capture hours worked for a consultant for a specific Task.

Instructions
1. Create a .Net Core API using C# and SQL Server database for data storage.
2. Data should be accessed via a Rest API that uses an ORM (EntityFramework or Nhibernate).
3. There should be Authentication on the API


Requirements to run the API on your local environment.
1. Have Dotnet 8 installed on your local machine
2. In the appsettings.json file - replace the connection string value with your local server, this is where your database will be created. ("Serve=[LOCALSERVER];Database=EmployeesManagementDb;Trusted_Connection=true;TrustServerCertificate=true;")
3. In Visual Studio, with the solution open - navigate to the Package manager console tab and run the command "update-database"
4. When this is done you can start up the application and an instance of the Swagger UI will start up.
5. When running the application for the first time, create a  new user with your details (remember email and password) Endpont: /api/Auth/register
6. Use the email and password to login and store the token generated (returned). Endpoint: api/Auth/login
7. You can use that token on your request header, it will expire after 60 minutes then require a new one to be generated.
8. You can now call the different endpoints
