# EstudiantesNet

## 🚀 Descripción

### EstudiantesNet 
es una API REST desarrollada con .NET 8, implementada bajo principios de arquitectura limpia y buenas prácticas de desarrollo de software.

El proyecto fue utilizado como base para implementar un ecosistema DevOps que integra automatización, pruebas, seguridad, contenerización, despliegue continuo y monitoreo.

La solución incorpora:

* GitHub Actions para la automatización del proceso de integración y despliegue.
* Pruebas unitarias y cobertura de código para validar la calidad de la solución.
* Snyk para el análisis de vulnerabilidades en dependencias y en la imagen Docker.
* Docker para la contenerización de la aplicación.
* Google Artifact Registry para el almacenamiento de imágenes Docker.
* Google Cloud Run para el despliegue de la API.
* Prometheus para la recolección de métricas.
* Grafana para la visualización de métricas y configuración de alertas.
* cAdvisor para la obtención de métricas de los contenedores.

El objetivo principal es demostrar la implementación práctica de un flujo DevOps que permita integrar desarrollo, pruebas, seguridad, despliegue y monitoreo dentro de un ciclo automatizado y reproducible.

<img src="Docs/images/Descripcion.png" alt="Arquitectura del proyecto" width="900"/>


## 🏗️ Arquitectura

La solución EstudiantesNet implementa un flujo DevOps que integra desarrollo, integración continua, seguridad, contenerización, despliegue y monitoreo.

### 🔄 Flujo principal


👨‍💻 Desarrollo
      │
      ▼
🐙 GitHub
      │
      ▼
⚙️ GitHub Actions
      │
      ├── 🧪 Build & Tests
      ├── 📊 Cobertura
      ├── 🔐 Snyk - Dependencias
      │
      ▼
🐳 Docker
      │
      ├── 🔐 Snyk - Imagen
      │
      ▼
☁️ Google Artifact Registry
      │
      ▼
🚀 Google Cloud Run
      │
      ▼
🌐 EstudiantesNet API


### 📊 Componentes de la solución

Componente	Responsabilidad

🐙 GitHub	Almacenamiento del código fuente y control de versiones

⚙️ GitHub Actions	Automatización del proceso CI/CD

🧪 .NET 8 + Tests	Compilación, validación y pruebas automatizadas

🔐 Snyk	Análisis de vulnerabilidades en dependencias e imagen Docker

🐳 Docker	Contenerización de la aplicación

📦 Artifact Registry	Almacenamiento de imágenes Docker

☁️ Cloud Run	Despliegue y ejecución de la API

📈 Prometheus	Recolección de métricas

📊 Grafana	Visualización de métricas y alertas

🖥️ cAdvisor	Recolección de métricas de contenedores

🔁 Integración CI/CD

El proceso automatizado se ejecuta a partir de cambios realizados sobre el repositorio:
````
Código → Build → Tests → Snyk → Docker → Snyk → Artifact Registry → Cloud Run
````
De esta manera, una modificación realizada en el código puede recorrer automáticamente las etapas de validación, análisis de seguridad, construcción de la imagen y despliegue.

### 📊 Observabilidad

La solución incorpora un entorno de monitoreo basado en Prometheus + Grafana + cAdvisor.

````
EstudiantesNet API ──────┐
                         │
                         ▼
                    📈 Prometheus
                         │
                         ▼
                    📊 Grafana
                         │
                  ┌──────┴──────┐
                  ▼             ▼
              Dashboard      Alertas

cAdvisor ────────────────► Prometheus
````
Durante el laboratorio, este entorno permitió visualizar métricas de la API y de los contenedores, incluyendo uso de CPU, memoria, solicitudes HTTP, estado de los servicios y reinicios de contenedores.

### ☁️ Entorno de despliegue

El despliegue de la aplicación se realiza actualmente en Google Cloud, utilizando:

Artifact Registry → Cloud Run

Adicionalmente, Kubernetes sobre Docker Desktop fue utilizado como entorno local para validar el despliegue y la ejecución de la aplicación en contenedores.

### 🖼️ Diagrama de arquitectura

<img src="Docs/images/Diagrama_Arquitectura.png"  width="1000"/>



## 🔄 Pipeline CI/CD

El proyecto utiliza GitHub Actions para automatizar el ciclo de integración y despliegue de la aplicación.

Cada cambio integrado al repositorio activa un flujo que valida el código, ejecuta las pruebas, realiza análisis de seguridad, construye y analiza la imagen Docker, la publica en Google Artifact Registry y finalmente despliega la aplicación en Google Cloud Run.

### 🚦 Flujo automatizado
````
🐙 GitHub
   │
   │ Push / Pull Request
   ▼
⚙️ GitHub Actions
   │
   ├── 🔧 Configuración de .NET 8
   │
   ├── 📦 Restaurar dependencias
   │
   ├── 🏗️ Compilar solución
   │
   ├── 🧪 Ejecutar pruebas
   │
   ├── 📊 Generar cobertura
   │
   ├── 🔐 Snyk - Análisis de dependencias
   │
   ├── 🐳 Construir imagen Docker
   │
   ├── 🔎 Snyk - Análisis de imagen
   │
   ├── 📦 Publicar en Artifact Registry
   │
   └── 🚀 Desplegar en Cloud Run
````

### 🔵 Continuous Integration — CI

La etapa de integración continua permite validar automáticamente cada cambio antes de considerarlo apto para despliegue.

| Etapa           | Propósito                                        |
| --------------- | ------------------------------------------------ |
| 🔧 **Restore**  | Restaurar las dependencias del proyecto          |
| 🏗️ **Build**   | Compilar la solución .NET 8                      |
| 🧪 **Tests**    | Ejecutar las pruebas automatizadas               |
| 📊 **Coverage** | Generar y publicar el reporte de cobertura       |
| 🔐 **Snyk**     | Identificar vulnerabilidades en las dependencias |



### 🟢 Continuous Delivery — CD

Una vez superadas las validaciones, el pipeline continúa automáticamente con el proceso de entrega:

E| Etapa                    | Propósito                                        |
| ------------------------ | ------------------------------------------------ |
| 🐳 **Docker Build**      | Construir la imagen de la API                    |
| 🔎 **Snyk Image**        | Analizar vulnerabilidades presentes en la imagen |
| 📦 **Artifact Registry** | Publicar la imagen Docker en Google Cloud        |
| 🚀 **Cloud Run**         | Desplegar la nueva versión de la aplicación      |

### ☁️ Resultado

El flujo completo queda automatizado de extremo a extremo:
````
Código
  ↓
Validación
  ↓
Pruebas
  ↓
Seguridad
  ↓
Docker
  ↓
Artifact Registry
  ↓
Cloud Run
````

Esto permite reducir la intervención manual, detectar problemas antes del despliegue y mantener un proceso de entrega repetible, trazable y automatizado.

### ✅ Última ejecución

La última ejecución validada del pipeline completó correctamente todas las etapas:

* ✅ Compilación
* ✅ Pruebas y cobertura
* ✅ Snyk — dependencias
* ✅ Construcción de imagen Docker
* ✅ Snyk — imagen Docker
* ✅ Publicación en Artifact Registry
* ✅ Despliegue en Cloud Run

Resultado: succeeded en aproximadamente 3 minutos y 16 segundos.

<img src="Docs/images/CI_CD.png"  width="1500"/>



## 🔐 Seguridad

La seguridad se integra directamente dentro del pipeline CI/CD mediante **Snyk**, permitiendo identificar vulnerabilidades antes de que una nueva versión de la aplicación sea desplegada.

El análisis se realiza en **dos niveles**:

```text
📦 Dependencias del proyecto
          │
          ▼
     🔐 Snyk Open Source
          │
          ▼
   Vulnerabilidades en
   paquetes y dependencias
```
y posteriormente:

```text
🐳 Imagen Docker
       │
       ▼
   🔐 Snyk Container
       │
       ▼
Vulnerabilidades de la
imagen y componentes base
```

### 🛡️ Análisis de dependencias

Durante la etapa de integración continua, Snyk analiza las dependencias utilizadas por la solución .NET.

Esta validación permite detectar componentes con vulnerabilidades conocidas y proporciona información para evaluar acciones de actualización o corrección.

### 🐳 Análisis de la imagen Docker

Después de construir la imagen de la aplicación, el pipeline ejecuta un segundo análisis utilizando Snyk.

Esta etapa permite identificar vulnerabilidades asociadas tanto a los componentes de la aplicación como a las librerías presentes en la imagen base utilizada por Docker.

El análisis se ejecuta antes de publicar la imagen en Google Artifact Registry, incorporando un control adicional de seguridad al proceso de entrega.

### 🔄 Seguridad integrada al CI/CD

El proceso queda integrado de la siguiente manera:

```text
Código
  │
  ▼
Build + Tests
  │
  ▼
🔐 Snyk
Dependencias
  │
  ▼
Docker Build
  │
  ▼
🔐 Snyk
Imagen Docker
  │
  ▼
Artifact Registry
  │
  ▼
Cloud Run
```
De esta forma, la seguridad no se considera una actividad independiente al final del desarrollo, sino un control integrado dentro del ciclo de entrega continua.

### ✅ Evidencia

La ejecución del pipeline muestra correctamente las etapas:

* ✅ Analizar dependencias con Snyk
* ✅ Analizar imagen Docker con Snyk
* ✅ Publicar imagen en Artifact Registry
* ✅ Desplegar en Cloud Run

<img src="Docs/images/dependencias_synk.png"  width="800"/>

<img src="Docs/images/docker_synk.png"  width="800"/>


## ☁️ Despliegue en Google Cloud

La aplicación **EstudiantesNet API** se encuentra desplegada en **Google Cloud Platform (GCP)** utilizando **Google Artifact Registry** para almacenar la imagen Docker y **Google Cloud Run** como plataforma de ejecución.

### 📦 Artifact Registry

La imagen Docker generada durante el pipeline CI/CD se publica automáticamente en un repositorio de **Artifact Registry**.

```text
🐳 Docker Image
      │
      │ Push
      ▼
📦 Google Artifact Registry
      │
      │ Pull
      ▼
☁️ Google Cloud Run
```

El repositorio utilizado es:

| Configuración  | Valor            |
| -------------- | ---------------- |
| 📦 Repositorio | `estudiantesnet` |
| 🐳 Formato     | Docker           |
| 🌎 Región      | `us-central1`    |
| ☁️ Proyecto    | `estudiantesnet` |


### 🚀 Cloud Run

Google Cloud Run se utiliza para ejecutar la aplicación como un servicio administrado de contenedores.

El pipeline obtiene la imagen publicada en Artifact Registry y realiza automáticamente el despliegue de la nueva versión.

| Configuración | Valor                                |
| ------------- | ------------------------------------ |
| 🚀 Servicio   | `estudiantesnet-api`                 |
| 🌎 Región     | `us-central1`                        |
| 📦 Imagen     | Google Artifact Registry             |
| 🐳 Runtime    | Contenedor Docker                    |
| 🔄 Despliegue | Automatizado mediante GitHub Actions |


### 🔄 Flujo de despliegue

```text
🐙 GitHub
   │
   ▼
⚙️ GitHub Actions
   │
   ├── 🧪 Tests
   ├── 🔐 Snyk
   ├── 🐳 Docker Build
   │
   ▼
📦 Artifact Registry
   │
   ▼
🚀 Cloud Run
   │
   ▼
🌐 EstudiantesNet API
```

De esta manera, el proceso de despliegue no requiere copiar manualmente imágenes ni ejecutar comandos de actualización en Cloud Run. La publicación de la imagen y el despliegue de la aplicación forman parte del pipeline automatizado.

### ✅ Estado del despliegue

La última ejecución del pipeline completó correctamente la etapa:

```text
Desplegar en Cloud Run
```
El servicio `estudiantesnet-api` se encuentra desplegado en la región `us-central1`.


<img src="Docs/images/ArtifactRegistry.png"  width="2000"/>


<img src="Docs/images/cloud_run.png"  width="1500"/>


## 📊 Monitoreo

La solución incorpora un entorno de observabilidad basado en **Prometheus**, **Grafana** y **cAdvisor**, permitiendo recopilar, consultar y visualizar métricas de la aplicación y de los contenedores.

### 🔎 Arquitectura de monitoreo

```text
🌐 EstudiantesNet API
        │
        │ /metrics
        ▼
📈 Prometheus
        │
        │ PromQL
        ▼
📊 Grafana
        │
        ├── 📈 Dashboards
        └── 🚨 Alertas
```
```text
🐳 Contenedores
        │
        ▼
🖥️ cAdvisor
        │
        ▼
📈 Prometheus
```

### Prometheus

Prometheus es utilizado como sistema de recopilación y almacenamiento de métricas.

La API expone un endpoint:
```text
/metrics
```
que proporciona métricas relacionadas con el comportamiento de la aplicación.

que proporciona métricas relacionadas con el comportamiento de la aplicación.

Prometheus consulta periódicamente este endpoint y almacena las series temporales para posteriormente ser consultadas mediante PromQL.

La configuración utiliza un intervalo de scraping de:
```text
5 segundos
```

### Grafana

**Grafana** se utiliza como herramienta de visualización y análisis de las métricas recolectadas por Prometheus.

Se configuró Prometheus como fuente de datos y se construyeron visualizaciones para observar el comportamiento de la aplicación y los contenedores.

Entre las métricas utilizadas se encuentran:

| Métrica                                  | Información                           |
| ---------------------------------------- | ------------------------------------- |
| 🟢 `up`                                  | Estado de disponibilidad del servicio |
| 🌐 `http_request_duration_seconds_count` | Cantidad de solicitudes HTTP          |
| 💾 `dotnet_total_memory_bytes`           | Memoria utilizada por .NET            |
| ♻️ `dotnet_collection_count_total`       | Colecciones del Garbage Collector     |
| 🧠 `container_cpu_usage_seconds_total`   | Uso de CPU del contenedor             |
| 💾 `container_memory_working_set_bytes`  | Memoria utilizada por el contenedor   |
| 🚀 `container_start_time_seconds`        | Momento de inicio del contenedor      |


### cAdvisor

**cAdvisor** permite recopilar métricas relacionadas con los contenedores que se encuentran ejecutándose en el entorno local.

Estas métricas son enviadas a Prometheus y posteriormente utilizadas por Grafana para construir visualizaciones sobre el comportamiento de los contenedores.

Se utilizaron métricas relacionadas con:

* 🧠 Uso de CPU
* 💾 Uso de memoria
* 🚀 Tiempo de inicio de contenedores
* ♻️ Reinicios
* 📦 Información de los contenedores
* ☸️ Información de los Pods de Kubernetes

### ☸️ Monitoreo del despliegue local

Durante las pruebas locales, la API se ejecutó sobre Kubernetes en Docker Desktop.

cAdvisor permitió obtener información del contenedor correspondiente a:
```text
estudiantesnet-api
```
y Prometheus permitió consultar estas métricas desde Grafana.

### 🚨 Alertas

Se configuró una regla de alerta en Grafana:
```text
EstudiantesNet - High CPU Usage
```

Esta regla permite identificar situaciones en las que el consumo de CPU de la aplicación supera el umbral definido.

La alerta se encuentra integrada dentro del sistema de observabilidad y puede ser consultada desde:

**Grafana** → **Alerting** → **Alert rules**

Nota: el contact point de correo fue configurado, pero el entorno local de Grafana no tiene un servidor SMTP configurado. Por esta razón, la prueba de envío de correo no se ejecutó. La regla de alerta permanece configurada y puede ser evaluada por Grafana.

### 🔄 Flujo de observabilidad
```text
📦 Aplicación / Contenedores
          │
          ▼
     📈 Métricas
          │
          ▼
      Prometheus
          │
          ▼
       Grafana
       /      \
      ▼        ▼
 📊 Dashboard 🚨 Alertas
```


## 📈 Métricas monitoreadas

#### **Prometheus_up**

<img src="Docs/images/prometheus_up.png"  width="1500"/>

#### **Prometheus_query**

<img src="Docs/images/promeyheus_query.png"  width="1500"/>

#### **Grafana_monitoring**

<img src="Docs/images/grafana_monitoring.png"  width="1200"/>

#### **RunTime_Memory**

<img src="Docs/images/RunTime_Memory.png"  width="1200"/>

#### **Errors_Status_Duration**

<img src="Docs/images/Errors_Status_Duration.png"  width="1200"/>



#### **cAdvisor**

<img src="Docs/images/cAdvisor.png"  width="900"/>




## 🚨 Alertas

El sistema de monitoreo incorpora una regla de alerta configurada en Grafana, permitiendo identificar de forma automática comportamientos que puedan afectar el rendimiento de la aplicación.

### ⚠️ Alerta configurada
EstudiantesNet - High CPU Usage

La alerta evalúa el consumo de CPU del contenedor asociado a la aplicación y permite detectar situaciones en las que el uso de recursos supera el umbral definido.

```text
🖥️ EstudiantesNet API │ ▼ 📈 Métricas de CPU │ ▼ Prometheus │ ▼ Grafana │ ▼ 🚨 High CPU Usage
```
La alerta permite detectar un consumo elevado de CPU en el contenedor asociado a la aplicación.

### 🔄 Flujo de evaluación


```text
🖥️ EstudiantesNet API
          │
          ▼
    📈 Métricas de CPU
          │
          ▼
      Prometheus
          │
          ▼
       Grafana
          │
          ▼
🚨 High CPU Usage
```

Grafana consulta periódicamente las métricas almacenadas en Prometheus y evalúa la condición definida en la regla de alerta.

Cuando la métrica supera el umbral configurado, la regla cambia de estado y permite identificar oportunamente un posible problema de rendimiento.

| Componente | Función |
|---|---|
| 📈 **Prometheus** | Recolecta y almacena las métricas |
| 📊 **Grafana** | Evalúa la condición configurada |
| 🚨 **Alert Rule** | Detecta el consumo elevado de CPU |
| 📬 **Contact Point** | Define el canal para recibir la notificación |


Grafana consulta periódicamente las métricas almacenadas en Prometheus y evalúa la condición definida en la regla de alerta.

Cuando el consumo de CPU supera el umbral configurado, la regla puede cambiar de estado y generar una notificación.

### 📬 Notificaciones

Se configuró un Contact Point para el envío de notificaciones mediante correo electrónico.

El entorno local de Grafana no tiene configurado un servidor SMTP, por lo que la prueba de envío de correo no pudo ejecutarse correctamente.

La regla de alerta permanece configurada y disponible dentro de Grafana.
```text
💡 En un entorno productivo, esta configuración puede integrarse con un proveedor SMTP u otros canales de notificación para informar automáticamente a los responsables de la operación.
```

### 🎯 Beneficio

La incorporación de alertas permite evolucionar desde un monitoreo reactivo hacia un enfoque más proactivo, facilitando la identificación temprana de problemas relacionados con el consumo de recursos.

#### **grafana_alerting**

<img src="Docs/images/grafana_alerting.png"  width="2000"/>
<img src="Docs/images/alertascorreo.png"  width="1200"/>


## 🐳 Ejecución local


Durante el laboratorio se utilizó un entorno local para validar la aplicación, el despliegue en contenedores y las herramientas de monitoreo.

### 🧩 Componentes utilizados

- 🟦 **.NET 8** — ejecución de la API.
- 🐳 **Docker Desktop** — ejecución de contenedores.
- ☸️ **Kubernetes** — despliegue local de la API.
- 📈 **Prometheus** — recolección de métricas.
- 📊 **Grafana** — visualización y alertas.
- 🖥️ **cAdvisor** — métricas de los contenedores.

### 🚀 Ejecución de la API

La API puede ejecutarse directamente desde **Visual Studio** utilizando el perfil de ejecución configurado para el proyecto.

Durante las pruebas locales, la API quedó disponible mediante HTTPS en:

```text
https://localhost:5025
```

El endpoint utilizado por Prometheus para obtener las métricas es:

```text
https://localhost:5025/metrics
```

La disponibilidad del endpoint fue validada obteniendo una respuesta:

```text
HTTP/1.1 200 OK
```

### 📈 Ejecución del entorno de monitoreo

Las herramientas de monitoreo se ejecutan mediante Docker Compose.

El archivo utilizado se encuentra en:

```text
monitoring/
├── docker-compose.yml
└── prometheus.yml
```

Para iniciar el entorno:

```text
cd monitoring

docker compose up -d
```

Para verificar los contenedores:
```text
docker ps
```

| Servicio                  | URL                      | Propósito                             |
| ------------------------- | ------------------------ | ------------------------------------- |
| 🚀 **EstudiantesNet API** | `https://localhost:5025` | API REST                              |
| 📈 **Prometheus**         | `http://localhost:9090`  | Consulta y almacenamiento de métricas |
| 📊 **Grafana**            | `http://localhost:3000`  | Dashboards y alertas                  |
| 🖥️ **cAdvisor**          | `http://localhost:8081`  | Métricas de contenedores              |


### 🔎 Validación de métricas

El endpoint de métricas de la API fue validado localmente mediante:
```text
Invoke-WebRequest https://localhost:5025/metrics
```
La respuesta contiene métricas de ASP.NET Core y .NET, entre ellas:
```text
http_request_duration_seconds
dotnet_collection_count_total
dotnet_total_memory_bytes
```
Estas métricas son posteriormente consultadas por Prometheus y utilizadas por Grafana para las visualizaciones del dashboard.

<img src="Docs/images/API_ok.png"  width="500"/>
<img src="Docs/images/API_ok2.png"  width="500"/>
<img src="Docs/images/docker_ps.png"  width="1000"/>





## ☸️ Kubernetes

## 🧪 Pruebas y cobertura

## 📁 Estructura del proyecto

## 📸 Evidencias

## 🎓 Reflexión DevOps
