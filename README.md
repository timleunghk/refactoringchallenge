# ASP.NET Core Refactoring

Docker image in App and SQL Server!

## Before Start
- Please make sure .NET Core 5.0 framework and Docker are installed successfully

## Getting started

- Open Folder "DatabaseSetup" 
- On Command Prompt, type "docker-compose up -d" to set up docker SQL Server 2017 and install Database "NorthWind"
- Open Folder "Program"  
- Open Solution File RefactoringChallenge.sln in "Program" folder in Visual Studio
- Select "Docker" option in Debug Configuration Setting
- Build and hit F5
- API Swagger is being displayed once this solution is built successfully

## Unit Test
- Open Folder "Program"  
- Open Solution File RefactoringChallenge.sln in "Program" folder in Visual Studio
- In Solution Explorer, right click on "RefactoringChallenge.Business.Test" project
- Select "Run Tests"

## Project structure

### Database Setup
- Docker Setup for SQL Server 2017 Docker version
- SQL script for creating Northwind Database

### Program

- RefactoringChallenge.API
  - Controllers
  - DockerFile for Docker Image Setup

- RefactoringChallenge.Business
  - DTO: data transfer objects
  - Mapping: autoMapper profile
  - Services: business logic of the application

- RefactoringChallenge.Business.Test
  - Test business logic in RefactoringChallenge.Business layer
  - Services depends on DBContext so using ```EF Core InMemory``` provider to mock data for testing purples

- RefactoringChallenge.Data
  - Entities - Models
  - Context - Context to work with database
