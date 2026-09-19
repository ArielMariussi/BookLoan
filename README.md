# EmprestimoLivros(BookLoan)

Web system for managing book loans, built with ASP.NET Core 9 MVC.

🔗 **Live demo:** https://emprestimolivros-9csn.onrender.com

## About the project

EmprestimoLivros is a web application that allows full control over book loans. Each user can create, edit, delete, and view their own loans, with authentication and data isolation.

The project includes a **demo mode** accessible via a button on the login screen, letting you try the app without creating an account. Demo account data is automatically reset every 24 hours.

## How to test

Go to https://emprestimolivros-9csn.onrender.com and you can:

1. **Sign In as Demo** — click the "Sign In as Demo" button on the login screen (no need to register)
2. **Create an account** — register to get your own isolated area

> The application runs on Render's free tier. The first visit may take up to 30 seconds to load (cold start).

## Features

- User registration and login (ASP.NET Identity)
- Demo mode with quick-access button
- Full loan CRUD (create, list, edit, delete)
- Data isolation: each user sees only their own loans
- Automatic reset of demo data every 24 hours
- User feedback messages (success/error)
- Responsive interface with Bootstrap 5

## Technologies

- **ASP.NET Core 9 MVC** — web framework
- **Entity Framework Core 9** — ORM
- **ASP.NET Identity** — authentication and authorization
- **PostgreSQL (Neon)** — cloud database
- **Bootstrap 5** — UI
- **Docker** — containerization
- **Render** — hosting with automatic CI/CD
- **GitHub** — version control



## Project structure

```
EmprestimoLivros/
├── Controllers/      Controller logic (Account, Loan, Home)
├── Data/             DbContext and initial seed
├── Models/           Entities
├── ViewModels/       ViewModels for Login and Register
├── Services/         Daily reset BackgroundService
├── Views/            Razor views
├── Migrations/       EF Core migrations
├── wwwroot/          Static files (css, js, images)
├── Dockerfile        Multi-stage build
└── Program.cs        Application bootstrap
```

## Deploy

Deployment happens automatically on Render on every push to the `main` branch. The Dockerfile does a multi-stage build and migrations are applied automatically on application startup.

## Author

**Ariel Mariussi**

- Portfolio : https://portfolio-ariel-cyan.vercel.app/
- GitHub: [@ArielMariussi](https://github.com/ArielMariussi)
- Email: arielmariussi@gmail.com
