# Freedom Planner

Freedom Planner is a personal financial decision-support application designed to help the user understand whether they are on track, what changed since the last review, and what financial decision should come next.

## Solution Structure

- `FreedomPlanner.Api` — ASP.NET Core backend API
- `FreedomPlanner.Application` — application orchestration and business workflow
- `FreedomPlanner.Domain` — core financial domain model and deterministic calculations
- `FreedomPlanner.Infrastructure` — EF Core SQLite persistence and infrastructure concerns
- `FreedomPlanner.Client` — React + TypeScript frontend
- `FreedomPlanner.Tests` — automated tests

## Current Status

This repository currently includes the application skeleton:

- ASP.NET Core API with a `/api/health` endpoint
- DI setup for infrastructure services
- EF Core with SQLite configuration support
- React + TypeScript client scaffold
- solution-level project structure

No business logic has been implemented yet.

## Running the Skeleton

### Backend

From the repository root:

```powershell
dotnet restore
cd FreedomPlanner.Api
dotnet run
```

The API should be available at:

- `http://localhost:5100/api/health`

### Frontend

From the repository root:

```powershell
cd FreedomPlanner.Client
npm install
npm run dev
```

## Notes

- Version 1 is intentionally simple and focused on a single-user, dashboard-first experience.
- The UI is presentation-only.
- Financial calculations and recommendation generation are expected to live in the backend layers.
