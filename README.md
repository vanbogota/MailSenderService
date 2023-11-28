# About
A service for sending reports to registered users. The report is generated based on a predefined template, and reports
are sent to the specified email addresses according to a specified schedule.

# Stack 
ASP.NET MVC Core, SQL, Web API, Razor Pages, Entity Framework, Quartz, MailKit.

# Description
- independently developed an application following the MVC pattern:

- The report generation is based on a specified template, initially built on the Razor Engine:

- implemented a service for SMTP operation, with SMTP settings stored in a separate configuration file:

- utilized Entity Framework Core Identity along with MSSQL to store data for registered users:

- implemented the necessary controllers for the service: a separate controller for accounts to facilitate user registration and
authentication, a separate controller for managing users (reading from the database, adding to the database, editing, deleting from
the database), a controller for the home page, and a controller for sending messages:

- integrated a task scheduler to send reports on a schedule using the Quartz library:

- used Razor Pages for the UI:
![image](https://github.com/vanbogota/MessageSenderService/assets/84347281/0a86b998-f62e-49aa-af1d-38f2e78c3b90)
