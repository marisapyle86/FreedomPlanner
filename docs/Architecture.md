# Architecture

## Stack
- ASP.NET Core (.NET 9)
- React + TypeScript
- Tailwind CSS + shadcn/ui
- SQLite
- xUnit

## Projects
- FreedomPlanner.Api
- FreedomPlanner.Application
- FreedomPlanner.Domain
- FreedomPlanner.Infrastructure
- FreedomPlanner.Client
- FreedomPlanner.Tests

## Architecture Overview

Version 1 will be a web application with a React frontend and an ASP.NET Core backend API.

The frontend is responsible for presentation only.

The backend is responsible for:
- receiving user input
- validating requests at the transport boundary
- executing business validation in the Application layer
- running domain calculations and projections
- generating Insights and Recommendations
- composing a UI-friendly Dashboard View Model

## Request Flow

User edits data

↓

Application validates input

↓

Domain calculations execute

↓

Projection engine runs

↓

Insight engine runs

↓

Recommendation engine runs

↓

Dashboard View Model produced

↓

API returns the result

↓

React renders the dashboard

## Dashboard Endpoint

Version 1 should expose a single dashboard endpoint that returns a complete Dashboard View Model.

The dashboard represents one cohesive projection of the user's financial state, so it should be retrieved as a single response rather than assembled through multiple API calls.

The response should be shaped specifically for presentation.

The frontend should receive a UI-friendly object that is ready to render without additional transformation or business logic.

## Validation Boundaries

Validation should occur at three levels:

1. API layer
   - performs transport validation
   - checks for malformed requests, missing fields, invalid data types, and obviously impossible values such as negative amounts

2. Application layer
   - owns all business validation rules
   - centralises reusable business decisions

3. Domain layer
   - enforces core business invariants where appropriate

This keeps invalid requests rejected early while preserving a single, reusable place for business rules.

## Dashboard Response Shape

The dashboard response should be primarily read-only.

It should include lightweight metadata describing the generated snapshot, such as:
- Generated timestamp
- Last updated timestamp
- Future projection assumption version

This metadata describes the dashboard snapshot itself rather than UI state.

## Rule
Business logic belongs in the Domain/Application layers.

The UI should never perform financial calculations.

The API should remain a thin transport layer and should not expose the underlying domain object graph directly to the frontend.
