# Global API

Simple .NET API for Countries and Cities with JWT auth, EF Core (SQL Server), and Docker.

## Run with Docker

1. Set env vars in `.env` (already provided).
2. Start services:

```bash
docker compose up --build
```

API runs on `http://localhost:8080`.

## Get a Token (Swagger)

1. Open `http://localhost:8080/swagger`.
2. Call `POST /api/v1/auth/token`.
3. Click **Authorize** and paste:

```
Bearer <accessToken>
```

## Run Tests

```bash
dotnet test
```
