# About
A web service for sending reports to registered users. The report is generated based on a predefined template, and reports
are sent to the specified email addresses according to a specified schedule.

# Stack 
ASP.NET MVC Core, SQL, Web API, Razor Pages, Entity Framework, Quartz, MailKit.

# Presentation
- Application following the MVC pattern, with using the Razor Pages:

![start page](https://github.com/vanbogota/mysite/blob/master/assets/2/startpage.gif)

- With form validation:

![2](https://github.com/vanbogota/mysite/blob/master/assets/2/registration_validation.gif)

- Registred users can get the report:

![send report](https://github.com/vanbogota/mysite/blob/master/assets/2/login_send.gif)
  
- The report generation is based on a specified template, initially built on the Razor Engine;

- Implemented a service for SMTP operation, with SMTP settings stored in a separate configuration file;

- Utilized Entity Framework Core Identity along with MSSQL to store data for registered users;

- Implemented the necessary controllers for the service: a separate controller for accounts to facilitate user registration and
authentication, a separate controller for managing users (reading from the database, adding to the database, editing, deleting from
the database), a controller for the home page, and a controller for sending messages;

- Integrated a task scheduler to send reports on a schedule using the Quartz library;






