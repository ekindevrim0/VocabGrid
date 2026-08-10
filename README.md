# VocabGrid API

An ASP.NET Core Web API backend for a language learning and memorization app utilizing flashcards, user authentication, and a SQL Server database.

---

## Tech Stack

* **Framework:** ASP.NET Core Web API (.NET 8)
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** JWT (JSON Web Tokens)

---

## Features

* **User Authentication:** Secure registration and login endpoints using JWT.
* **Flashcard & Deck Management:** Create, update, and manage vocabulary flashcards for target languages.
* **Database Persistence:** SQL Server backend managed via Entity Framework migrations.

---

## Getting Started Locally

### Prerequisites

* Visual Studio 2022 or VS Code / Cursor
* .NET 8 SDK
* SQL Server Express or LocalDB

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ekindevrim0/GigaMind.git
   cd GigaMind
   ```

   > GitHub repository URL may still use the old name `GigaMind`. The application name is **VocabGrid** (`VocabGrid.csproj`, namespaces, assembly).

2. **Open the project** (inside the project folder that contains `VocabGrid.csproj`):
   ```bash
   cd GigaMind
   ```
   Open `VocabGrid.slnx` or `VocabGrid.csproj`.

3. **Configure** `appsettings.json` (`DefaultConnection`, `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`). Prefer User Secrets for `Jwt:Key` in development.

4. **Apply migrations:**
   ```bash
   dotnet ef database update --project VocabGrid.csproj
   ```

5. **Run:**
   ```bash
   dotnet run --project VocabGrid.csproj
   ```
   Swagger: `http://localhost:5068/swagger`
