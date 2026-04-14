# EcosystemName Distributed Ecosystem

Generated with `dotnet new ecosystem`.

## Selected Options

- Services: `services`
- Architecture: `architecture`
- Database: `db`
- Frontend: `frontend`
- Infrastructure: `infra`
- Gateway: `gateway`
- Auth: `auth`

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
