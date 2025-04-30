OfficeControl is a modular CRUD (Create, Read, Update, Delete) system designed to manage various data entities within an office environment. The system is divided into two independent parts:

CRUDify-API (ASP.NET Core backend)

CRUDify-UI (React frontend)

         Tech Stack

Backend -	ASP.NET Core (.NET 6+), EF Core

Frontend-	React.js, Vite, Axios

Database-	SQL Server (via EF migrations)

          Backend Setup (CRUDify-API)
cd CRUDify-API

dotnet restore

dotnet ef database update

dotnet run

          Frontend Setup (CRUDify-UI)
cd CRUDify-UI

npm install

npm run dev

               Features


Full CRUD functionality through RESTful endpoints

Responsive React UI with dynamic data rendering

Modular codebase, easy to extend

Clean separation of concerns between backend and frontend

Built with scalability and maintainability in mind


Important: Make sure to configure your Azure AD and database credentials securely in your local environment. Do not expose production credentials in source control.
