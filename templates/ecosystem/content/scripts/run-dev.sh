#!/usr/bin/env bash
set -euo pipefail

echo "Starting EcosystemName local environment..."
docker compose -f infra/docker-compose.yml up --build
