# eLEARNING_GROUP3
Online Learning Platform

Group Member

- Tran Dinh Hung
  
- Nguyen Xuan Hau
  
- Phan Quoc Dai

- Pham Dinh Phuong Sang

1) Introduction
Online Learning Platform is a web application developed using ASP.NET CORE MVC. The goal of the project is to provide an online learning platform for students and instructors, optimizing course management, tracking learning progress, and enhancing the learning experience.

***************
2) Key Features
***************
A) Mentee : 
Register and log in.
View and enroll in courses.
Track learning progress and assignments.
Rate and review courses.
Purchase course, Deposit money to wallet
Chat message to mentor


B) Mentor : 
Create and manage courses.
Upload learning materials and lectures.
Monitor student progress.


C) Admin : 
Manage users (mentees and mentors).
Manage course categories.
Approve and moderate course content.


D) AI : 
Review Course content to approve or reject.
Support member 24/7

********************
3) Technologies Used
********************

Programming Language: C#

Framework: ASP.NET CORE MVC

Database: SQL Server

Front-end: HTML, CSS, JavaScript, Bootstrap

Version Control: Git

********************
4) Project Structure
********************

OnlineLearningPlatform/

├── wwwroot/

├── Configurations/

├── Controllers/

├── Data/

├── Enum/

├── Migrations/

├── Models/

├── Repositories/

├── Services/

├── Views/

├── appsettings.json

└── Program.cs


********************
5) Key Directories
********************

wwwroot: Contains JavaScript, CSS, Images files, and other static resources.

Configurations: Contains configurations to config in Program.cs.

Controllers: Contains controllers to handle application logic.

Data: Contains DBContext and SeedConfigurationData.

Migrations: Entity Framework Core Migrations.

Models: Contains classes representing data.

Repositories: Contains logic query database, call DBContext.

Services: Contains business logic, call Repository.

Views: Contains Razor files for rendering content.

appsettings.json: Configs application

Program.cs: Start point

**********************
6) System Requirements
**********************

Visual Studio 2022 (or later).

SQL Server 2019 (or higher).

.NET 8.0 (or higher).
