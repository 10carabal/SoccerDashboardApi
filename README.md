# Football Stats Management System

**Solution for managing and visualizing professional football statistics.**
This project is composed of two parts:
- **Backend API** built with ASP.NET Core (.NET)
- **Frontend Dashboard** built with Angular (included as a submodule)

---

## 🚀 Overview
This repository serves as the main project, providing a professional practice environment to strengthen full-stack development skills.

- The **.NET API** delivers endpoints for managing football data, ensuring performance and scalability.
- The **Angular Dashboard** (still under development) provides a modern, responsive web interface for visualization.

---

## 🛠 Tech Stack
- **Backend**: ASP.NET Core (.NET 7/8)
- **Frontend**: Angular (submodule: [`SoccerDashboardApp`](./SoccerDashboardApp))
- **Database**: (to be defined / can be SQL Server, PostgreSQL, etc.)

---

## 📦 Project Structure
```
FootballStats-API/         → Main repository (API in .NET)
│
├── Controllers/           → API endpoints
├── Models/                → Domain models
├── ...
└── SoccerDashboardApp/    → Angular dashboard (submodule)
```

---

## ⚡️ Getting Started

### 1. Clone with submodules
```bash
git clone --recurse-submodules https://github.com/YOUR-USER/FootballStats-API.git
cd FootballStats-API
```

### 2. Run the API
```bash
dotnet restore
dotnet build
dotnet run
```

### 3. Run the Angular Dashboard
```bash
cd SoccerDashboardApp
npm install
ng serve -o
```

---

## 📌 Status
- **API**: Functional, serving football stats data.
- **Dashboard**: Work in progress (UI and features under development).

---

## 🎯 Purpose
This project was created as a **professional practice exercise** to demonstrate skills in full-stack web development, combining .NET for backend services and Angular for frontend applications.
It is also intended as part of a **portfolio for future job opportunities**.

## Licencia

Este proyecto se distribuye bajo la licencia MIT.

---

Desarrollado por Daniel Alejandro Otero y colaboradores.
