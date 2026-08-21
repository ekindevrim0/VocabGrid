# VocabGrid API

An ASP.NET Core Web API backend for a language learning and memorization app utilizing flashcards, user authentication, and a SQL Server database.

---

## Tech Stack

* **Framework:** ASP.NET Core Web API (targets .NET 8, runs on .NET 8 or any newer runtime)
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** JWT (JSON Web Tokens), plus Google and Apple sign-in

---

## Features

* **User Authentication:** Secure registration and login endpoints using JWT.
* **Email Verification:** 6-digit codes with a 15-minute lifetime and a 5-attempt budget.
* **Password Reset:** Emailed reset tokens with anti-enumeration responses.
* **Flashcard & Deck Management:** Create, update, and manage vocabulary flashcards for target languages.
* **Curriculum Content:** 20 lessons from A1 to B2 and 238 vocabulary entries ship as seed data.
* **Data Integrity:** every text column is length-bounded and 23 CHECK constraints enforce
  the same rules the DTOs do, so invalid data cannot reach the tables by any path.
* **Database Persistence:** SQL Server backend managed via Entity Framework migrations.

---

## Getting Started Locally

Only steps 1–3 need anything from you. Step 4 (the database) happens by itself, and
step 5 (SMTP) is optional — the API runs fine without it.

### Prerequisites

* **.NET SDK 8.0 or newer.** The project targets `net8.0` but sets
  `<RollForward>LatestMajor</RollForward>`, so a machine that only has the .NET 9 or 10
  runtime installed can still run it.
* **SQL Server** — LocalDB (ships with Visual Studio), SQL Server Express, or a full instance.
  You do not need to create a database by hand; the app does that on first run.
* Visual Studio 2022, VS Code, Rider, or Cursor.

The EF Core CLI (`dotnet tool install --global dotnet-ef`) is **optional** — useful for
adding migrations, not needed to run the project.

### 1. Clone and enter the project folder

```bash
git clone https://github.com/suleymandogan-software/LanGigaCards-Backend.git
cd LanGigaCards-Backend/VocabGrid
```

Everything below runs from the inner `VocabGrid` folder — the one containing
`VocabGrid.csproj`. The user-secrets commands only work from there.

### 2. Set the JWT signing key (required)

**The API refuses to start without `Jwt:Key`.** This is deliberate: a missing key is a
loud startup failure rather than a silently insecure default.

The project already carries a `UserSecretsId`, so there is no `init` step — just set the
value. Use a long random string, at least 32 characters:

```bash
dotnet user-secrets set "Jwt:Key" "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"
```

Need one generated? On Windows PowerShell:

```bash
[Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Max 256 }))
```

Secrets are stored per-machine, outside the repository, at
`%APPDATA%\Microsoft\UserSecrets\f07a094b-d00a-4a4b-a2bf-526e44ee23a7\secrets.json`
(Windows) or `~/.microsoft/usersecrets/...` (macOS/Linux). Nothing you set this way is
ever committed.

Environment variables work too, and are what you would use on a server — replace `:`
with `__`:

```bash
$env:Jwt__Key = "REPLACE_WITH_A_LONG_RANDOM_SECRET_AT_LEAST_32_CHARS"
```

`Jwt:Issuer` and `Jwt:Audience` already have working defaults in `appsettings.json`.

### 3. Point at your SQL Server

The default connection string in `appsettings.json` uses LocalDB and needs no change if
you have it:

```
Server=(localdb)\mssqllocaldb;Database=VocabGridDb;Trusted_Connection=True;MultipleActiveResultSets=true
```

Using SQL Server Express or a named instance instead? Override it in user secrets — this
keeps your machine-specific server name out of the repository, so everyone can use a
different instance without editing tracked files:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=VocabGridDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

SQL authentication instead of Windows authentication:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=.\SQLEXPRESS;Database=VocabGridDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

`TrustServerCertificate=True` is needed for local instances using a self-signed
certificate. Do not carry it into production.

### 4. The database — nothing to do

**Skip this step.** In Development the app creates the database itself on first run and
applies any migration added since the last one. There is no script to execute, no backup
to restore, and nothing to attach in SSMS.

You will see it happen in the startup log:

```
info: VocabGrid[0] Applying 10 pending migration(s) to VocabGridDb.
info: Microsoft.EntityFrameworkCore.Migrations[20402] Applying migration '20260808183423_InitialCreate'.
...
info: VocabGrid[0] Database ready: VocabGridDb.
```

On later runs, with nothing pending, only the last line appears.

What you get is a fully populated database — 26 tables plus the seed content: 10
languages, 15 categories, 14 word tags, 6 learning purposes, 5 achievements, 20 lessons,
10 quizzes, 238 vocabulary entries and 312 word-to-tag links. No user accounts; you create
yours by registering in the app.

This is Development-only on purpose. In production a schema change should be a deliberate,
reviewed step, and two instances starting at once would race each other applying it —
so deploy with an explicit `dotnet ef database update` instead.

Want to apply migrations by hand anyway (needs the optional EF CLI):

```bash
dotnet ef database update
```

### 5. Email delivery (optional)

**Skip this and the API still works.** Without SMTP credentials the app registers a
logging stub instead of a real sender: verification codes and password-reset tokens are
written to the console rather than emailed, and in Development the
`send-verification-code` endpoint returns the code in its response as
`DevVerificationCode` so you can finish the flow from Swagger.

To send real mail, set all four values. `Smtp:Password` is absent from
`appsettings.json` on purpose — it belongs in user secrets only:

```bash
dotnet user-secrets set "Smtp:Host" "smtp.gmail.com"
dotnet user-secrets set "Smtp:User" "you@gmail.com"
dotnet user-secrets set "Smtp:FromAddress" "you@gmail.com"
dotnet user-secrets set "Smtp:Password" "your-16-char-app-password"
```

For Gmail, that password is **not** your account password. Turn on 2-Step Verification in
your Google account, then create an App Password under *Security → App passwords*; Google
gives you a 16-character string. Accounts without 2-Step Verification cannot create one.

`Smtp:Port` (587), `Smtp:EnableSsl` (true), and `Smtp:FromName` already have defaults in
`appsettings.json` that suit Gmail and most providers.

The transport is chosen from configuration, not from the environment, and the app says
which one it picked on the first line of its startup log:

```
Email transport: SMTP via smtp.gmail.com:587 as you@gmail.com.
Email transport: logging stub — no Smtp:Host/User/Password configured, so no mail will be sent.
```

If you configured SMTP and still see the second line, one of Host, User, or Password is
empty — all three are required to activate the real sender.

### 6. Run

```bash
dotnet run
```

Swagger UI: `http://localhost:5068/swagger`

---

## Configuration reference

| Key | Required | Where it belongs | Default |
| --- | --- | --- | --- |
| `Jwt:Key` | **Yes** | User secrets / environment | none — startup fails without it |
| `Jwt:Issuer` | No | `appsettings.json` | `VocabGridAPI` |
| `Jwt:Audience` | No | `appsettings.json` | `VocabGridApp` |
| `ConnectionStrings:DefaultConnection` | No | `appsettings.json`, override in user secrets | LocalDB |
| `Smtp:Host` | No | User secrets | empty → logging stub |
| `Smtp:User` | No | User secrets | empty → logging stub |
| `Smtp:Password` | No | **User secrets only** | empty → logging stub |
| `Smtp:FromAddress` | No | User secrets | empty |
| `Smtp:Port` | No | `appsettings.json` | `587` |
| `Smtp:EnableSsl` | No | `appsettings.json` | `true` |
| `Cors:AllowedOrigins` | Production | Environment / secret store | empty — Development allows any origin |
| `Authentication:Google:ClientId` | For Google sign-in | User secrets | empty |
| `Authentication:Apple:ClientId` | For Apple sign-in | User secrets | empty |

In environment variables, `:` becomes `__` — `Jwt:Key` is `Jwt__Key`,
`Smtp:Password` is `Smtp__Password`.

---

## Auth endpoints

`POST /api/Auth/register` — all fields are required, `confirmPassword` must match, and
the password must be at least 8 characters. Registration also issues a verification code.

```json
{
  "firstName": "Ekin",
  "lastName": "Adsay",
  "email": "ekin@example.com",
  "password": "Test1234!",
  "confirmPassword": "Test1234!"
}
```

`POST /api/Auth/send-verification-code` — `{ "email": "..." }`. Issues a fresh 6-digit
code and retires any outstanding one. In Development the response includes
`DevVerificationCode`.

`POST /api/Auth/verify-email` — `{ "email": "...", "code": "123456" }`. Codes expire
after 15 minutes and allow 5 attempts before requiring a new one.

Other endpoints: `login`, `refresh`, `forgot-password`, `reset-password`, `google`, `apple`.

Email verification is **not** a login gate — an unverified user can still sign in, and
the auth response carries `isEmailVerified` so the client can decide what to do about it.

---

## Catalog and statistics endpoints

`GET /api/Language` — the supported languages, ordered for a picker. **Anonymous**: the
sign-up and onboarding screens need this list before the user has a session. Add
`?includeInactive=true` to see languages that have been switched off.

`GET /api/Tag` — word tags, optionally filtered with `?kind=Grammar|Register|Difficulty`.
Tags describe a word's grammar or usage ("irregular verb", "formal", "false friend") and
are separate from categories, which are topics the learner picks as interests.

`GET /api/Tag/{slug}/words` — words carrying a tag. Returns curriculum words plus the
caller's own cards; another user's cards never appear, even when they share the tag.

`GET /api/Progress/daily-summary?from=&to=` — one row per day of study, defaulting to the
last year. Backed by a rollup table rather than a scan over raw activity, so the heatmap
reads at most 365 rows instead of every review the user has ever done. Days with no study
have no row — the client draws the gap as "no activity" rather than a zero.

---

## Flutter / client notes

* Android emulator base URL: `http://10.0.2.2:5068` (`localhost` inside the emulator is the emulator itself)
* Chrome / Windows: `http://localhost:5068`
* Achievements: `GET /api/Achievements` (there is no `/api/Badges`)
* Decks/cards: `/api/Deck`, `/api/Flashcard`
* Profile language codes: `nativeLanguageCode` / `targetLanguageCode` (e.g. `en`, `tr`)
* Learning purposes: `PUT /api/User/learning-purposes` expects `{ "learningPurposeIds": [...] }`.
  A different key name binds to nothing and silently clears the user's saved purposes.
* Categories include `iconName` and `colorHex`
* Language list: `GET /api/Language` — prefer this over a hardcoded client list. `flagCode`
  is separate from `code` because they differ (`en`→`gb`, `ja`→`jp`, `ko`→`kr`, `zh`→`cn`)
* Seed word IDs live in reserved ranges (1001–1118 and 5001–5120); cards created through
  the API start at 10001, so the two never collide
* Development CORS is open; production must set `Cors:AllowedOrigins`

---

## Troubleshooting

**`Jwt:Key yapılandırması eksik`** — step 2 was skipped, or the command was run from the
wrong folder. `dotnet user-secrets list` must be run from the folder containing
`VocabGrid.csproj`; anywhere else it reads a different (or missing) secret store.

**`You must install or update .NET`** — no compatible runtime found. Install the .NET 8
SDK or newer.

**`A network-related or instance-specific error occurred`** — SQL Server is not running,
or the instance name in the connection string is wrong. Check which instances exist:
```bash
Get-Service | Where-Object { $_.Name -like 'MSSQL*' }
```

**Verification email never arrives** — check the startup log line described in step 5.
The logging stub is silent by design.
