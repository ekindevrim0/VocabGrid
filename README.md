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

3. **Configure JWT secret (required — do NOT put secrets in appsettings.json):**

   Development (User Secrets):
   ```bash
   cd VocabGrid
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:Key" "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"
   ```

   Or set an environment variable:
   - Windows PowerShell: `$env:Jwt__Key = "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"`
   - Production / hosting: set `Jwt__Key` in the environment or secret store

   `Jwt:Issuer` and `Jwt:Audience` stay in `appsettings.json`.
   If `Jwt:Key` is missing, the API will not start.

4. **Apply migrations:**
   ```bash
   dotnet ef database update --project VocabGrid.csproj
   ```

5. **Run:**
   ```bash
   dotnet run --project VocabGrid.csproj
   ```
   Swagger: `http://localhost:5068/swagger`
