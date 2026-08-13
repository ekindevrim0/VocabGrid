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
   git clone https://github.com/ekindevrim0/VocabGrid.git
   cd VocabGrid
   ```

   > GitHub repo name is **VocabGrid**. Local folder may still be named `GigaMind` if you cloned earlier; the project inside is `VocabGrid/` (`VocabGrid.csproj`).

2. **Open the project** (folder that contains `VocabGrid.csproj`):
   ```bash
   cd VocabGrid
   ```
   Open `VocabGrid.slnx` or `VocabGrid.csproj`.

3. **Configure JWT secret (required — do NOT put secrets in appsettings.json):**

   > **Important for every developer machine:** the API will not start without `Jwt:Key`.
   > Run these once inside the project folder from step 2:

   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "Jwt:Key" "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"
   dotnet user-secrets set "Jwt:Issuer" "VocabGridAPI"
   dotnet user-secrets set "Jwt:Audience" "VocabGridApp"
   ```

   Or set an environment variable:
   - Windows PowerShell: `$env:Jwt__Key = "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"`
   - Production / hosting: set `Jwt__Key` in the environment or secret store

   `Jwt:Issuer` and `Jwt:Audience` also stay in `appsettings.json` as defaults.
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

### Auth register body (required fields)

`POST /api/Auth/register` requires:
```json
{
  "firstName": "Ekin",
  "lastName": "Adsay",
  "email": "ekin@example.com",
  "password": "Test1234!",
  "confirmPassword": "Test1234!"
}
```
`confirmPassword` is mandatory and must match `password` (min 8 chars).

### Flutter / client notes

* Android emulator base URL: `http://10.0.2.2:5068`
* Chrome / Windows: `http://localhost:5068`
* Achievements: `GET /api/Achievements` (there is no `/api/Badges`)
* Decks/cards: `/api/Deck`, `/api/Flashcard`
* Profile language codes: `nativeLanguageCode` / `targetLanguageCode` (e.g. `en`, `tr`)
* Categories include `iconName` and `colorHex`
* Development CORS is open; production must set `Cors:AllowedOrigins` in config
