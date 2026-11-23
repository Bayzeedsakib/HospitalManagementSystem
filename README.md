# 🏥 Hospital Management System

A role-based hospital management web application built with **ASP.NET Core MVC**, **Entity Framework Core** and **SQL Server**. It uses a clean **3-tier architecture** (Presentation → Business Logic → Data Access) and supports three roles: **Admin**, **Doctor** and **Patient**.

![.NET](https://img.shields.io/badge/ASP.NET_Core_MVC-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core_9-68217A?style=flat-square&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-BE161D?style=flat-square)

## ✨ Features

| Module | What it does |
|---|---|
| 🔑 **Authentication** | Session-based login and logout; each role is sent to its own dashboard |
| 🏢 **Departments** | Create, view, edit, delete and search departments |
| 👨‍⚕️ **Doctors** | Doctor profiles with specialization, qualification, department, available hours and max patients per day, plus a **Doctor Dashboard** |
| 🧑 **Patients** | Patient records with contact details, gender, date of birth, blood group and address |
| 📅 **Appointments** | Book appointments by date and time with a serial number, status and problem description |
| 💊 **Prescriptions** | Symptoms, diagnosis, advice, next visit date, and multiple **medicines** with dosage, duration and instructions |

Every module has search, list, detail, create, edit and delete-with-confirmation views.

## 👥 Roles

After login, the user's role is stored in the session and they are redirected to their own area:

| Role | Redirected to |
|---|---|
| **Admin** (`RoleId = 1`) | Admin dashboard |
| **Doctor** (`RoleId = 2`) | Doctor dashboard |
| **Patient** (`RoleId = 3`) | Appointments |

Admin-only pages (such as the admin dashboard and creating doctors) check the session role and send other users to an **Access Denied** page.

## 🧱 Architecture

```
HospitalManagementSystem.sln
│
├── Hospital.Web/        ← Presentation layer (ASP.NET Core MVC)
│   ├── Controllers/       Admin, Auth, Department, Doctor, Patient, Appointment, Prescription
│   └── Views/             Razor views for every module
│
├── BLL/                 ← Business logic layer
│   ├── Services/          One service per module
│   ├── DTOs/              Data transfer objects
│   └── MapperConfig.cs    AutoMapper profiles (Entity ↔ DTO)
│
└── DAL/                 ← Data access layer
    ├── EF/                HospitalDbContext + entity tables
    └── Repositories/      One repository per entity
```

Controllers talk only to **services**, services talk only to **repositories**, and AutoMapper converts between entities and DTOs so the database models never reach the views.

## 🗂 Data Model

`User` · `Role` · `Department` · `Doctor` · `Patient` · `Appointment` · `Prescription` · `Medicine` · `PrescriptionMedicine`

```
Department 1──* Doctor 1──* Appointment *──1 Patient
                                 │
                                 1
                                 │
                           Prescription 1──* PrescriptionMedicine *──1 Medicine
User *──1 Role
```

## 🚀 Getting Started

**Prerequisites:** [.NET 9 SDK](https://dotnet.microsoft.com/download), SQL Server or LocalDB, and Visual Studio 2022 (recommended).

```bash
git clone https://github.com/Bayzeedsakib/HospitalManagementSystem.git
cd HospitalManagementSystem
```

1. Create a SQL Server database named `HospitalDB` with the tables above. The project is **database-first**: the entities in `DAL/EF/Tables` are scaffolded from the database.
2. Check the connection string in `Hospital.Web/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "SchoolDbContext": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HospitalDB;TrustServerCertificate=True;Integrated Security=True;"
   }
   ```

3. Add the three roles (`1 Admin`, `2 Doctor`, `3 Patient`) and at least one admin user to the `Roles` and `Users` tables.
4. Run the web project:

   ```bash
   dotnet run --project Hospital.Web
   ```

5. Open the app and log in at `/Auth/Login`.

## 🛣 Roadmap

- [ ] Hash passwords (for example with ASP.NET Core Identity's `PasswordHasher`)
- [ ] Enforce role checks on every controller (for example with a custom authorization filter)
- [ ] Add EF Core migrations or a SQL seed script for one-command setup
- [ ] Add screenshots of each dashboard

## 👤 Author

**Bayzeed Sakib** · [GitHub](https://github.com/Bayzeedsakib) · [Portfolio](https://bayzeed-sakib-portfolio.vercel.app)
