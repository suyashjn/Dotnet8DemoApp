# DemoApp

DemoApp is a .NET 8 web application that provides various services including bookings, inventory management, and member management. This project is structured into multiple layers including Business, Data, and Models to ensure a clean architecture.

## Table of Contents

- [DemoApp](#demoapp)
  - [Table of Contents](#table-of-contents)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Running the Application](#running-the-application)
  - [Running Tests](#running-tests)
  - [Project Structure](#project-structure)
  - [Technologies Used](#technologies-used)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

### Installation

1. Clone the repository.
2. Restore the dependencies.

## Project Structure

- **DemoApp**: The main web application project.
- **DemoApp.Business**: Contains the business logic and services.
- **DemoApp.Data**: Contains data access logic and repositories.
- **DemoApp.Models**: Contains data models and DTOs.
- **DemoApp.UnitTests**: Contains unit tests for the application.

## Running the Application

1. Navigate to the `DemoApp` project directory.
2. Run the application.
3. Open your browser and navigate to `https://localhost:5030` to see the application running.

## Running Tests

1. Navigate to the `DemoApp.UnitTests` project directory.
2. Run the tests.

## Technologies Used

- .NET 8
- ASP.NET Core
- Entity Framework Core
- AutoMapper
- CsvHelper
- Swashbuckle (Swagger)
- xUnit
- Moq
