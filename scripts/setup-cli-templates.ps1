Write-Host "🚀 Setting up Nexavor Templates..."

# ROOT
$root = "Templates"

# -----------------------------
# FRONTEND - REACT
# -----------------------------
$reactPath = "$root/Frontend/react/src"
New-Item -ItemType Directory -Path $reactPath -Force | Out-Null

# package.json
@"
{
  "name": "{{APP_NAME}}-web",
  "private": true,
  "scripts": {
    "dev": "vite",
    "build": "vite build"
  },
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0"
  },
  "devDependencies": {
    "vite": "^5.0.0",
    "@vitejs/plugin-react": "^4.0.0"
  }
}
"@ | Out-File "$root/Frontend/react/package.json"

# vite.config.js
@"
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()]
})
"@ | Out-File "$root/Frontend/react/vite.config.js"

# index.html
@"
<div id='root'></div>
<script type='module' src='/src/main.jsx'></script>
"@ | Out-File "$root/Frontend/react/index.html"

# main.jsx
@"
import React from 'react'
import ReactDOM from 'react-dom/client'

function App() {
  return <h1>{{APP_NAME}} 🚀</h1>
}

ReactDOM.createRoot(document.getElementById('root')).render(<App />)
"@ | Out-File "$root/Frontend/react/src/main.jsx"

# -----------------------------
# GATEWAY (YARP)
# -----------------------------
$gatewayPath = "$root/Gateway/Yarp"
New-Item -ItemType Directory -Path $gatewayPath -Force | Out-Null

@"
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy();

var app = builder.Build();

app.MapReverseProxy();

app.Run();
"@ | Out-File "$gatewayPath/Program.cs"

# -----------------------------
# INFRA (DOCKER)
# -----------------------------
$infraPath = "$root/Infra"
New-Item -ItemType Directory -Path $infraPath -Force | Out-Null

@"
version: '3.8'

services:
  gateway:
    build: ./backend/gateway
    ports:
      - "5000:80"
"@ | Out-File "$infraPath/docker-compose.yml"

Write-Host "✅ Templates ready!"