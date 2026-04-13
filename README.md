# 🚀 Nexavor

**Nexavor is a .NET ecosystem generator for building production-ready microservices architectures in seconds.**

It scaffolds complete distributed systems including:
- Microservices (Clean Architecture)
- API Gateway
- Shared contracts & building blocks
- Frontend applications
- Infrastructure (Docker, Kubernetes, observability)
- Dev scripts for local development & deployment

---

## ⚡ Why Nexavor?

Building microservices manually is repetitive, slow, and error-prone.

Nexavor solves this by generating a complete, opinionated but customizable system architecture from a single command.

---

## 🏗️ What it generates

MyApp/
│
├── backend/
│   ├── services/
│   │   ├── user-service/
│   │   ├── order-service/
│   │   ├── payment-service/
│   │   └── notification-service/
│   │
│   ├── gateway/
│   ├── shared/
│   ├── building-blocks/
│   └── MyApp.Backend.sln
│
├── frontend/
│   └── web-app/
│
├── infra/
│   ├── docker-compose.yml
│   ├── kubernetes/
│   ├── monitoring/
│   └── logging/
│
├── scripts/
│   ├── run-dev.sh
│   ├── build-all.sh
│   └── deploy.sh
│
├── MyApp.sln
└── README.md

---

## 🚀 Quick Start

### Install template
dotnet new install Nexavor

---

### Create a new ecosystem
dotnet new ecosystem -n MyApp

---

### Example with custom configuration
dotnet new ecosystem -n ShopApp \
--services user,order,payment \
--gateway yarp \
--frontend react \
--db postgres \
--infra docker,k8s,monitoring \
--auth jwt

---

## 🧩 Features

### Backend
- Microservices with Clean Architecture
- Independent service solutions
- Docker-ready services
- CQRS-friendly structure (optional)

### Gateway
- API routing (YARP / Ocelot support)
- Central authentication handling
- Request forwarding

### Shared Layer
- Contracts (DTOs, events)
- Reusable building blocks
- Cross-service messaging models

### Frontend
- React / Angular / Next.js support
- Pre-configured API integration
- Environment-based config setup

### Infra
- Docker Compose orchestration
- Kubernetes manifests (optional)
- Observability stack (Prometheus/Grafana)
- Logging setup (Serilog-ready)

---

## 🧠 Design Principles

- Each microservice is independently deployable
- Clean Architecture inside every service
- No shared databases between services
- Communication via APIs or events
- Infrastructure is separated from application code
- Everything is reproducible via templates

---

## ⚙️ Architecture Philosophy

Nexavor follows a platform-first approach:

Instead of building one service at a time, generate the entire ecosystem and evolve it.

---

## 📦 Core Templates

- ecosystem → full system generator
- microservice → Clean Architecture service
- gateway → API gateway setup
- frontend → web application scaffold

---

## 🛣️ Roadmap

- Plugin system for custom templates
- Kafka / RabbitMQ event bus integration
- Helm chart generation
- IdentityServer / Auth server template
- Multi-cloud deployment support
- VS Code extension

---

## 🤝 Contributing

Contributions are welcome!

- Fork the repo
- Create a feature branch
- Submit a pull request

---

## 📄 License

This project is licensed under the MIT License.

---

## 🔥 Nexavor

Build systems, not services.
