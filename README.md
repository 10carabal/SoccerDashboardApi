# FootballStats Solution

Solución para la gestión y visualización de estadísticas de fútbol profesional. Incluye una API desarrollada en .NET y un dashboard web moderno en Angular.

## Estructura de la solución

- [`FootballStats.Api`](FootballStats.Api)
  API RESTful en ASP.NET Core para exponer datos de fútbol y estadísticas.
- [`FootballStats.Application`](FootballStats.Application)
  Lógica de negocio y casos de uso.
- [`FootballStats.Domain`](FootballStats.Domain)
  Entidades y contratos del dominio.
- [`FootballStats.Infrastructure`](FootballStats.Infrastructure)
  Integraciones externas y servicios de infraestructura.
- [`SoccerDashboardApp`](SoccerDashboardApp)
  Aplicación web Angular para visualización y consulta de datos.

## Características principales

- Consulta de goleadores, equipos y jugadores por temporada y liga.
- Integración con API externa de fútbol ([`FootballApiService`](FootballStats.Infrastructure/Services/FootballApiService.cs)).
- Arquitectura limpia: separación de dominio, aplicación, infraestructura y presentación.
- Dashboard interactivo con Angular y PrimeNG.

## Requisitos

- .NET 9.0 SDK
- Node.js 18+
- Angular CLI 19+

## Instalación y ejecución

### Backend (.NET API)

```sh
cd FootballStats.Api
dotnet restore
dotnet run
```

La API estará disponible en [http://localhost:5200](http://localhost:5200) (configurable en [`launchSettings.json`](FootballStats.Api/Properties/launchSettings.json)).

### Frontend (Angular)

```sh
cd SoccerDashboardApp
npm install
ng serve
```

Accede a [http://localhost:4200](http://localhost:4200) para ver el dashboard.

## Configuración

Edita [`appsettings.json`](FootballStats.Api/appsettings.json) para configurar claves y host de la API externa:

```json
"ApiFootball": {
  "Key": "TU_API_KEY",
  "Host": "v3.football.api-sports.io"
}
```

## Pruebas

- **Backend:**
  Ejecuta pruebas unitarias con:
  ```sh
  dotnet test
  ```
- **Frontend:**
  Ejecuta pruebas con:
  ```sh
  ng test
  ```

## Licencia

Este proyecto se distribuye bajo la licencia MIT.

---

Desarrollado por Daniel Alejandro Otero y colaboradores.
