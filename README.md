# Hiring Process

This repository contains a complete solution for managing hiring process, consisting of a .Net Core API (HiringProcessService), and an Angular frontend application. This system allows you to perform various operations on candidate data, such as creating, updating, deleting, and viewing candidate details.

## Backend

### Hiring process Service - Candidate Management API

Hiring process Service is a .Net Core API that handles candidate management. It follows best practices and design principles to ensure code quality and maintainability.

#### Features

- **Candidate Data**:
  - Candidates are identified by an `id` and have attributes including `name`, `stage`, `phone`, and `email`.
  
- **Endpoints**:
  - The API provides the following endpoints:
    - `GET /candidate`: Retrieve a list of all candidates.
    - `GET /candidate/{id}`: Retrieve a candidate by their ID.
    - `POST /candidate`: Create a new candidate.
    - `PUT /candidate/{id}`: Update an existing candidate by their ID.
    - `DELETE /candidate/{id}`: Delete an candidate by their ID.
    
- **Design Principles**:
  - Use Clean Architecure
  - Apply CQRS pattern using MediatR
  - Follow Domain-Driven Design (DDD)
  - Apply SOLID, KISS, YAGNI principles for clean and maintainable code.
  
- **Data Storage**:
  - candidate data is stored in a JSON file.

- **Caching**:
  - Use .NET InMemmory Cache, apply DI to be able to replace by other Cache like Redis, ...

- **Logging**:
  - User Serilog to log console and file, be able to extend to write to other platforms like DataDog, ...
  
- **Dependency Injection**:
  - Apply for Services, Repositories, ... be able to replace, change the implementation or technical easily
  
- **Configuration**:
  - Configuration options, such as the file path of the cadidate repository, are abstracted using configuration settings.
  
- **Unit Tests**:
  - Use xUnit to write Unit Test (for Business (Application and Domain layers)) and Integration Test (for API, ...)

## Frontend - Angular hiring process management Application

The front end is an Angular application that allows users to manage the hiring process using the candidate Management API.

#### Features

- **API Integration**:
  - The application integrates with the candidate Management API to perform various operations.
  - Use HttpClient with Observable

## How to Run
To run easily, we can use Visual Studio and Angular CLI to serve/build
(or we can run/deploy each service/app by dotnet CLI and Angular CLI)
1. Clone Repo
2. Use Visual Studio to open the solution file **Mercu.sln**
3. Run the project using Visual Studio
4. Run the Angular app by open **FrontEnd** and run / build by angular cli
