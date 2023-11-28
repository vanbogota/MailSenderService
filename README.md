# About
A web service for sending reports to registered users. The report is generated based on a predefined template, and reports
are sent to the specified email addresses according to a specified schedule.

# Stack 
ASP.NET MVC Core, SQL, Web API, Razor Pages, Entity Framework, Quartz, MailKit.

# Description
- Application following the MVC pattern;
- Starting page:

![1](https://github.com/vanbogota/MessageSenderService/assets/84347281/a8d47f43-6505-4562-881b-5437306fd45f)

- Registration page:

![2](https://github.com/vanbogota/MessageSenderService/assets/84347281/c08951ac-0c25-444e-b41c-4128b8929e68)

- Loged in user page:

![3](https://github.com/vanbogota/MessageSenderService/assets/84347281/34853621-2bce-4223-8b92-e24db59c4089)
  
- The report generation is based on a specified template, initially built on the Razor Engine;

- Implemented a service for SMTP operation, with SMTP settings stored in a separate configuration file;

- Utilized Entity Framework Core Identity along with MSSQL to store data for registered users;

- Implemented the necessary controllers for the service: a separate controller for accounts to facilitate user registration and
authentication, a separate controller for managing users (reading from the database, adding to the database, editing, deleting from
the database), a controller for the home page, and a controller for sending messages;

- Integrated a task scheduler to send reports on a schedule using the Quartz library;






