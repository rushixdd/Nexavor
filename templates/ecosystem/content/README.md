# EcosystemName Distributed Ecosystem

Generated with `dotnet new ecosystem`.

## Selected Options

- Services: `__SERVICES__`
- Architecture: `__ARCHITECTURE__`
- Database: `__DB__`
- Frontend: `__FRONTEND__`
- Infrastructure: `__INFRA__`
- Gateway: `__GATEWAY__`
- Auth: `__AUTH__`

## Generation Flow

1. Generate or update each microservice under `backend/services/`.
2. Generate the API gateway under `backend/gateway/`.
3. Keep cross-service messages in `backend/shared/contracts`.
4. Put reusable primitives in `backend/shared/building-blocks`.
5. Generate and configure the frontend under `frontend/web-app`.
6. Keep runtime infra manifests under `infra/`.

## Recommended Next Commands

```bash
# from EcosystemName/
dotnet new microservice -n user-service -o backend/services/user-service
dotnet new gateway -n EcosystemName.Gateway -o backend/gateway
```
