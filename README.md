# Employee List Application

A full-stack web application for managing employee records, built with Angular frontend and ASP.NET Core backend.

## Technologies

- **Frontend**: Angular 19 with PrimeNG UI components
- **Backend**: ASP.NET Core 9.0 Web API with Entity Framework Core
- **Database**: Microsoft SQL Server 2022
- **Authentication**: JWT Bearer tokens with ASP.NET Core Identity
- **Containerization**: Docker with Docker Compose

## Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine

## Quick Start with Docker

### 1. Clone the Repository

```bash
git clone https://github.com/PaulAllan209/employee-list-application.git
cd employee-list-application
```

### 2. Build and Run with Docker Compose

Make sure your terminal is in the root directory (`/employee-list-application`) and run:

```bash
# Build all services
docker-compose build --no-cache

# Start all services
docker-compose up
```

### 3. Access the Application

Once all containers are running, you can access:

- **Frontend**: http://localhost:4200/login
- **Backend API**: http://localhost:5001

## Database Connection

The application uses Microsoft SQL Server running in a Docker container:

- **Server**: `localhost,1435`
- **Authentication**: SQL Server Authentication
- **Login**: `sa`
- **Password**: `YourStrongPassword123!`
- **Database**: `EmployeesDB`

## Development

### Environment Configuration

The application uses different configuration files for different environments:

- **Development**: `appsettings.Development.json`
- **Docker**: `appsettings.Docker.json`
- **Production**: `appsettings.json`

### Database Seeding

The application automatically seeds the database with initial data when starting up. This includes:
- Sample employee data
