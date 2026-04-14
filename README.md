# 🚀 Nexavor

Nexavor is a starter template collection for generating a .NET micro-services ecosystem.

## Included Templates

- `ecosystem` — scaffolds the full folder structure (backend, frontend, infra, scripts).
- `microservice` — creates a Clean Architecture microservice skeleton.
- `gateway` — creates an API gateway starter (YARP/Ocelot-ready config).
- `frontend` — creates a frontend placeholder scaffold (React/Angular/Next.js option).

## Repository Layout

```text
templates/
  ecosystem/
  microservice/
  gateway/
  frontend/
```

Each template includes a `.template.config/template.json` plus scaffold content files.

## Local Usage

Install templates from this repo root:

```bash
dotnet new install ./templates/ecosystem
dotnet new install ./templates/microservice
dotnet new install ./templates/gateway
dotnet new install ./templates/frontend
```

Generate an ecosystem:

```bash
dotnet new ecosystem -n MyApp
```

Generate a service:

```bash
dotnet new microservice -n user-service -o backend/services/user-service
```

## Ecosystem Options

```bash
dotnet new ecosystem -n ShopApp \
  --services user,order,payment,notification \
  --gateway yarp \
  --frontend react \
  --db postgres \
  --infra docker,k8s,monitoring,logging \
  --auth jwt
```

## Status

This is the initial implementation pass with minimal but functional scaffolding for all four template types.
