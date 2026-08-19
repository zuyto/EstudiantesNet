# 🚀 EstudiantesNet — Pipeline CI/CD con GitHub Actions, Docker, GHCR y Kubernetes

[![CI - EstudiantesNet](https://github.com/zuyto/EstudiantesNet/actions/workflows/ci.yml/badge.svg)](https://github.com/zuyto/EstudiantesNet/actions/workflows/ci.yml)

Aplicación REST desarrollada con **.NET 8 / ASP.NET Core**, utilizada como proyecto base para la implementación de prácticas DevOps mediante integración continua, pruebas automatizadas, cobertura de código, contenedores Docker, GitHub Container Registry y Kubernetes.

El proyecto implementa un pipeline automatizado mediante **GitHub Actions**, encargado de restaurar dependencias, compilar la solución, ejecutar pruebas, generar reportes de cobertura, construir una imagen Docker y publicarla en un registro privado de GitHub Container Registry (GHCR).

Posteriormente, la imagen es utilizada para validar el despliegue de la aplicación en un entorno Kubernetes ejecutado mediante Docker Desktop.

---

## 📋 Tabla de contenido

- [🎯 Objetivo](#-objetivo)
- [🏗️ Arquitectura](#️-arquitectura)
- [🛠️ Tecnologías utilizadas](#️-tecnologías-utilizadas)
- [📁 Estructura del proyecto](#-estructura-del-proyecto)
- [⚙️ Prerrequisitos](#️-prerrequisitos)
- [🐳 Docker](#-docker)
- [🔄 Pipeline CI](#-pipeline-ci)
- [🧪 Pruebas automatizadas](#-pruebas-automatizadas)
- [📊 Cobertura de código](#-cobertura-de-código)
- [📦 GitHub Container Registry](#-github-container-registry)
- [☸️ Kubernetes](#️-kubernetes)
- [🚀 Despliegue](#-despliegue)
- [❤️ Health Check](#️-health-check)
- [📚 Swagger](#-swagger)
- [🔐 Seguridad y buenas prácticas](#-seguridad-y-buenas-prácticas)
- [🔁 Flujo DevOps](#-flujo-devops)
- [📈 Evolución hacia CD](#-evolución-hacia-cd)
- [📊 Resultados](#-resultados)
- [🎓 Conclusiones](#-conclusiones)

---

# 🎯 Objetivo

El objetivo de esta implementación es establecer un proceso básico de **Integración Continua (CI)** y preparar la aplicación para un flujo de **Continuous Delivery / Continuous Deployment (CD)**.

La solución busca automatizar las principales actividades posteriores a un cambio en el código:

```text
Developer
    │
    ▼
Git Push / Pull Request
    │
    ▼
GitHub
    │
    ▼
GitHub Actions
    │
    ├── Restore
    ├── Build
    ├── Test
    ├── Coverage
    ├── Docker Build
    └── Docker Push
             │
             ▼
           GHCR
             │
             ▼
        Kubernetes
```
# 🏗️ Arquitectura

La arquitectura implementada integra control de versiones, integración continua, construcción de contenedores y orquestación mediante Kubernetes.

```text

                         ┌─────────────────────┐
                         │      Developer      │
                         └──────────┬──────────┘
                                    │
                              Git Push / PR
                                    │
                                    ▼
                         ┌─────────────────────┐
                         │       GitHub        │
                         │     Repository      │
                         └──────────┬──────────┘
                                    │
                                    ▼
                     ┌───────────────────────────┐
                     │      GitHub Actions       │
                     │                           │
                     │  ✓ Checkout               │
                     │  ✓ .NET 8                 │
                     │  ✓ Restore                │
                     │  ✓ Build                  │
                     │  ✓ Unit Tests             │
                     │  ✓ Coverage               │
                     │  ✓ Docker Build            │
                     │  ✓ Docker Push             │
                     └─────────────┬─────────────┘
                                   │
                                   ▼
                     ┌───────────────────────────┐
                     │           GHCR            │
                     │ GitHub Container Registry  │
                     │       Private Image        │
                     └─────────────┬─────────────┘
                                   │
                              Pull Image
                                   │
                                   ▼
              ┌────────────────────────────────────────┐
              │           Docker Desktop               │
              │              Kubernetes                │
              │                                        │
              │   ┌────────────────────────────────┐   │
              │   │ Deployment                     │   │
              │   │                                │   │
              │   │ EstudiantesNet API             │   │
              │   │                                │   │
              │   │ Pod: 1/1 Running               │   │
              │   └───────────────┬────────────────┘   │
              │                   │                    │
              │            Kubernetes Service          │
              │                   │                    │
              └───────────────────┼────────────────────┘
                                  │
                             NodePort 31017
                                  │
                                  ▼
                           ┌──────────────┐
                           │   Browser    │
                           └──────┬───────┘
                                  │
                    ┌─────────────┴─────────────┐
                    ▼                           ▼
             /api/health                    /swagger
                    │                           │
                    ▼                           ▼
                Healthy                     Swagger UI

```

# 🛠️ Tecnologías utilizadas

| Componente           | Tecnología                 | Propósito                      |
| -------------------- | -------------------------- | ------------------------------ |
| Lenguaje             | C#                         | Desarrollo de la aplicación    |
| Framework            | .NET 8 / ASP.NET Core      | Implementación de la API       |
| Arquitectura         | Separación por capas       | Organización y mantenibilidad  |
| Control de versiones | Git / GitHub               | Gestión del código fuente      |
| CI                   | GitHub Actions             | Automatización del pipeline    |
| Testing              | xUnit                      | Pruebas automatizadas          |
| Mocking              | Moq                        | Simulación de dependencias     |
| Coverage             | Coverlet                   | Medición de cobertura          |
| Reportes             | ReportGenerator            | Generación del reporte HTML    |
| Contenedores         | Docker                     | Empaquetado de la aplicación   |
| Registry             | GitHub Container Registry  | Almacenamiento de imágenes     |
| Orquestación         | Kubernetes                 | Administración de contenedores |
| Entorno Kubernetes   | Docker Desktop             | Clúster local                  |
| API Documentation    | Swagger / OpenAPI          | Documentación interactiva      |
| Health Check         | ASP.NET Core Health Checks | Verificación del estado        |

# 📁 Estructura del proyecto

```text
EstudiantesNet/
│
├── EstudiantesNet.Api/
├── EstudiantesNet.Application/
├── EstudiantesNet.Domain/
├── EstudiantesNet.Infrastructure/
├── EstudiantesNet.Logger/
│
├── EstudiantesNet.UnitTests.Api/
├── EstudiantesNet.UnitTests.Application/
├── EstudiantesNet.UnitTests.Domain/
├── EstudiantesNet.UnitTests.Infrastructure/
├── EstudiantesNet.UnitTests.Logger/
│
├── .github/
│   └── workflows/
│       └── ci.yml
│
├── k8s/
│   ├── deployment.yaml
│   └── service.yaml
│
├── EstudiantesNet.sln
├── Dockerfile
└── README.md
```
El Dockerfile utilizado para construir la API se encuentra actualmente en EstudiantesNet.Api/Dockerfile.

# ⚙️ Prerrequisitos

Para ejecutar el proyecto se requiere:

Git
Cuenta de GitHub
.NET 8 SDK
Visual Studio 2022
Docker Desktop
Kubernetes habilitado en Docker Desktop
kubectl

Verificar .NET
```text
dotnet --version
```

Verificar Docker
```text
docker --version
```

Verificar kubectl
```text
kubectl version --client
```

Verificar Kubernetes
```text
kubectl get nodes
```

Resultado obtenido:
```text
NAME             STATUS   ROLES           VERSION
docker-desktop   Ready    control-plane   v1.32.2
```

# 🐳 Docker

La aplicación utiliza un Dockerfile basado en una estrategia multi-stage build.

Ubicación:

```text
EstudiantesNet.Api/Dockerfile
```

El proceso se divide en:

```text
.NET 8 SDK
     │
     ├── Restore
     ├── Build
     └── Publish
             │
             ▼
     ASP.NET Core Runtime
             │
             ▼
      EstudiantesNet.Api
```

Se utilizan las imágenes oficiales:

```text
mcr.microsoft.com/dotnet/sdk:8.0
mcr.microsoft.com/dotnet/aspnet:8.0
```

Esto permite mantener separadas las herramientas necesarias para compilar de los componentes necesarios para ejecutar la aplicación.

# 🔄 Pipeline CI

El pipeline está definido en:

```text
.github/workflows/ci.yml
```

Se ejecuta automáticamente cuando existe un push o pull request sobre:
```text
main
develop
```

El flujo implementado es:

```text
Checkout
   │
   ▼
Setup .NET 8
   │
   ▼
Restore
   │
   ▼
Build
   │
   ▼
Unit Tests
   │
   ▼
Coverage
   │
   ▼
HTML Report
   │
   ▼
Docker Build
   │
   ▼
Docker Push
   │
   ▼
GHCR
```


Workflow utilizado

```text
name: CI - EstudiantesNet

on:
  push:
    branches:
      - main
      - develop

  pull_request:
    branches:
      - main
      - develop
```

El workflow utiliza:

```text
permissions:
  contents: read
  packages: write
```

Esto permite que GitHub Actions pueda leer el repositorio y publicar paquetes en GHCR.

# 🧪 Pruebas automatizadas

El pipeline ejecuta las pruebas de toda la solución:

```text
dotnet test EstudiantesNet.sln
```

Resultado

| Proyecto                                | Pruebas | Resultado         |
| --------------------------------------- | ------: | ----------------- |
| EstudiantesNet.UnitTests.Api            |      18 | ✅                 |
| EstudiantesNet.UnitTests.Application    |      35 | ✅                 |
| EstudiantesNet.UnitTests.Domain         |       9 | ✅                 |
| EstudiantesNet.UnitTests.Infrastructure |      27 | ✅                 |
| EstudiantesNet.UnitTests.Logger         |      17 | ✅                 |
| **TOTAL**                               | **106** | **106 correctas** |


Resultado global

```text
106 pruebas ejecutadas
106 pruebas correctas
0 pruebas fallidas
```

Esto demuestra que los cambios pueden ser validados automáticamente antes de generar y publicar la imagen Docker.

# 📊 Cobertura de código

La cobertura se genera utilizando:
```text
Coverlet
```

mediante:
```text
--collect:"XPlat Code Coverage"
```

Los resultados se generan en:
```text
TestResults/
```

Posteriormente se utiliza:
```text
ReportGenerator
```

para convertir los resultados en un reporte HTML.

```text
coverage.cobertura.xml
          │
          ▼
    ReportGenerator
          │
          ▼
     CoverageReport
          │
          ▼
       HTML
```

El pipeline publica dos tipos de artefactos:
```text
coverage-report-xml
coverage-report-html

```

## Resultado obtenido

# 📊 Cobertura global: 53 %

El reporte HTML puede descargarse desde la sección Artifacts de la ejecución correspondiente en GitHub Actions.

# 📦 GitHub Container Registry

Las imágenes Docker son publicadas automáticamente en:
```text
GitHub Container Registry (GHCR)
```
El registro utilizado es:
```text
ghcr.io
```
La imagen se genera utilizando dos etiquetas:
```text
ghcr.io/zuyto/estudiantesnet-api:${GITHUB_SHA}


ghcr.io/zuyto/estudiantesnet-api:latest
```
La etiqueta basada en el SHA permite mantener trazabilidad entre la imagen y el commit que la generó.
```text
Git Commit
    │
    ▼
Git SHA
    │
    ▼
Docker Image
    │
    ▼
GHCR
```
La imagen se mantiene como privada.


# ☸️ Kubernetes

Para validar el despliegue se utilizó Kubernetes mediante Docker Desktop.
```text
Docker Desktop
      │
      ▼
 Kubernetes
      │
      ├── Deployment
      │
      ├── Pod
      │
      └── Service
```
Los manifiestos se encuentran en:
```text
k8s/
├── deployment.yaml
└── service.yaml
```

# 🔐 Registro privado en Kubernetes

Debido a que la imagen de GHCR es privada, Kubernetes requiere credenciales para realizar el pull.

Se creó el secreto:
```text
ghcr-secret
```
Tipo:
```text
kubernetes.io/dockerconfigjson
```
El Deployment utiliza:
```text
imagePullSecrets:
  - name: ghcr-secret
```
De esta forma, las credenciales no se almacenan directamente dentro del manifiesto.

### ⚠️ Nunca deben almacenarse tokens reales en el repositorio.

# 🚀 Despliegue

El Deployment se aplica mediante:
```text
kubectl apply -f k8s/deployment.yaml
```
Posteriormente se aplica el Service:
```text
kubectl apply -f k8s/service.yaml
Verificar Pods
kubectl get pods
```
Resultado obtenido:
```text
NAME                                 READY   STATUS    RESTARTS
estudiantesnet-api-fb8b8c45c-4jpcj   1/1     Running   0
```
El Pod se encuentra:
```text
READY     1/1
STATUS    Running
RESTARTS  0
```

# 🔌 Service Kubernetes

Verificar:
```text
kubectl get services
```
Resultado:
```text
NAME                 TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)
estudiantesnet-api   NodePort    10.98.114.238   <none>        8080:31017/TCP
```
El servicio utiliza:
```text
NodePort: 31017
```
Por lo tanto, la aplicación queda disponible localmente mediante:
```text
http://localhost:31017
```

# ❤️ Health Check

La aplicación dispone del endpoint:
```text
/api/health
```
URL:
```text
http://localhost:31017/api/health
```
Resultado:
```text
Healthy
```

El flujo de validación es:
```text
Browser
   │
   ▼
NodePort :31017
   │
   ▼
Kubernetes Service
   │
   ▼
Pod :8080
   │
   ▼
ASP.NET Core
   │
   ▼
/api/health
   │
   ▼
Healthy
```

# 📚 Swagger

La API dispone de documentación interactiva mediante Swagger/OpenAPI.

URL:
```text
http://localhost:31017/swagger/index.html
```
Swagger permite:
```text
Visualizar los endpoints.
Consultar parámetros.
Ejecutar operaciones.
Revisar respuestas.
Explorar la API desplegada.
```
La validación de Swagger confirma que la aplicación está funcionando correctamente dentro del contenedor y no únicamente que el Pod se encuentra en estado Running.

# 🔐 Seguridad y buenas prácticas

| # | Situación                        | Riesgo                          | Mejora                                |
| - | -------------------------------- | ------------------------------- | ------------------------------------- |
| 1 | GHCR privado                     | Acceso no autorizado            | Mantener el registry privado          |
| 2 | Token de GHCR                    | Exposición accidental           | Utilizar GitHub Secrets               |
| 3 | Kubernetes local                 | No representa producción        | Migrar a EKS, AKS o GKE               |
| 4 | Cobertura 53 %                   | Código sin suficiente cobertura | Incrementar cobertura progresivamente |
| 5 | Sin análisis de vulnerabilidades | Posibles CVE                    | Integrar Trivy / GitHub Security      |
| 6 | CD Kubernetes manual             | Requiere intervención           | Automatizar deployment                |
| 7 | Swagger habilitado               | Exposición de información       | Restringir en producción              |
| 8 | Sin aprobación de producción     | Riesgo de despliegues           | Utilizar ambientes protegidos         |



# 🔁 Flujo DevOps

La solución aplica principios fundamentales de DevOps:

Automatización

GitHub Actions elimina tareas repetitivas:
```text
Restore
   ↓
Build
   ↓
Test
   ↓
Coverage
   ↓
Docker Build
   ↓
Docker Push
```
Esto disminuye la intervención manual y los errores asociados.

Feedback rápido

Ante un cambio:
```text
Git Push
   ↓
GitHub Actions
   ↓
Build
   ↓
Tests
   ↓
Coverage
```
Si una prueba falla:

# ❌ Pipeline failed

El desarrollador recibe retroalimentación inmediata.

Colaboración

GitHub facilita:

* Control de versiones.
* Pull Requests.
* Revisión de código.
* Historial de cambios.
* Automatización.
* Trazabilidad.
* Consistencia

Docker permite empaquetar la aplicación junto con sus dependencias y ejecutarla de manera consistente en diferentes ambientes.

# 📈 Evolución hacia CD

Actualmente se cuenta con:
```text
GitHub
   ↓
GitHub Actions
   ↓
Build
   ↓
Tests
   ↓
Coverage
   ↓
Docker Build
   ↓
GHCR
   ↓
Kubernetes
```
El despliegue Kubernetes fue validado mediante:
```text
k8s/deployment.yaml
k8s/service.yaml
```
Para completar un proceso de Continuous Delivery / Continuous Deployment, el siguiente paso sería incorporar Kubernetes directamente al pipeline.

La arquitectura futura sería:
```text
Developer
    │
    ▼
GitHub
    │
    ▼
GitHub Actions
    │
    ├── Build
    ├── Test
    ├── Coverage
    ├── Security Scan
    └── Docker Build
            │
            ▼
           GHCR
            │
            ▼
      Kubernetes Cloud
            │
            ▼
       Rolling Update
            │
            ▼
        Health Check
```
Algunas alternativas para producción:

* Amazon EKS
* Azure Kubernetes Service (AKS)
* Google Kubernetes Engine (GKE)

# 📊 Resultados
| Validación            | Resultado          |
| --------------------- | ------------------ |
| .NET 8                | ✅                  |
| Compilación           | ✅                  |
| Pruebas automatizadas | ✅                  |
| Pruebas correctas     | **106/106**        |
| Pruebas fallidas      | **0**              |
| Cobertura             | **53 %**           |
| Reporte HTML          | ✅                  |
| Docker Build          | ✅                  |
| Docker Push           | ✅                  |
| GHCR                  | ✅                  |
| Registry privado      | ✅                  |
| Kubernetes            | ✅                  |
| Deployment            | ✅                  |
| Pod                   | **1/1 Running**    |
| Restarts              | **0**              |
| Service               | **NodePort 31017** |
| Health Check          | **Healthy**        |
| Swagger               | ✅                  |


# 🎓 Conclusiones

La implementación permitió establecer un proceso de integración continua para una aplicación ASP.NET Core desarrollada con .NET 8.

GitHub Actions automatiza la restauración de dependencias, compilación, ejecución de pruebas, generación de cobertura y construcción de imágenes Docker.

Se ejecutaron:
```text
106 pruebas automatizadas, con 106 resultados correctos y 0 fallos.
```
La cobertura obtenida fue del:
```text
53 %
```
El reporte de cobertura se genera automáticamente y queda disponible como artefacto de GitHub Actions.

Docker permitió empaquetar la aplicación de forma reproducible y GitHub Container Registry proporciona un repositorio privado para las imágenes generadas.

Posteriormente, la imagen fue validada en Kubernetes mediante Docker Desktop, obteniendo un Pod en estado:
```text
1/1 Running
```
sin reinicios.

Finalmente, la aplicación respondió correctamente mediante:
```text
/api/health → Healthy
```
y Swagger quedó disponible para la exploración de la API.

Desde la perspectiva DevOps, la solución demuestra cómo la automatización, las pruebas, la generación de artefactos, la contenerización y la orquestación pueden integrarse dentro del ciclo de vida del desarrollo de software.

Como evolución futura, el proceso puede convertirse en un CD completo incorporando un clúster Kubernetes administrado en la nube, análisis de vulnerabilidades, gestión centralizada de secretos, ambientes protegidos y despliegues automatizados mediante Rolling Updates.

# 👨‍💻 Proyecto

EstudiantesNet

Tecnología principal: .NET 8 / ASP.NET Core

CI/CD: GitHub Actions

Containerización: Docker

Registry: GitHub Container Registry

Orquestación: Kubernetes

Autor: Mauro Ferney Martinez Quiroga 
