# Rapid pay
Meet our company – RapidPay.
RapidPay as a payment provider needs YOU to develop its new Authorization system and is
willing to pay accordingly!
The whole project is divided into two parts: Card Management module and Payment Fees
module.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Technologies Used](#technologies-used)
- [Architecture used](#architecture-used)
- [Contributing](#contributing)

## Introduction

This is a .NET Core API project that provides a set of functionalities using .NET Core 8. It includes three endpoints to perform specific tasks and utilizes SQL Server in a Docker container for data storage.

## Features

- .NET Core 8 API
- Three endpoints for specific functionalities
  - /api/cards/create-card
  - /api/cards/{cardId}/pay
  - /api/cards/{cardId}/balance
- SQL Server in Docker for data storage

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET Core 8](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

## Getting Started

Follow the steps below to set up and run the project locally.

### Installation

1. Clone the repository to your local machine:

    ```bash
    git clone https://github.com/luisjjavier/rapidpay.git
    cd rapidpay
    ```

2. Build the project:

    ```bash
    dotnet build
    ```


### Running the Application

1. Run the application:

    ```bash
    dotnet run
    ```

2. The application will be accessible at `http://localhost:5000` by default.

### another way to test the app is using docker under solution folder
1. Run the following command 
```bash
docker-compose up
```
### Unit testing (Core.Facts)
    ```bash
    dotnet test
    ```
## API Endpoints

- **Login**: Use this endpoint for login
  - Route: `/login`
  - Method: POST
  - Body:
  ```
  {
  "email": "TestAccount@example.com",
  "password": "string12@A"
  } 
##### *Please note that the login process will provide a token, which you will then use to authorize subsequent requests.
- **Create a new card**: This will create a new card, In my understanding, the system generates a card with a unique 15-digit identifier. Notably, this identifier is not obtained from the incoming request but is dynamically created by the system. This approach ensures the uniqueness and security of each card generated within the system. By autonomously generating the card number, the system reduces the risk of potential security vulnerabilities associated with externally provided card numbers. This practice aligns with best security practices in financial and card-based systems, where the system itself generates and manages sensitive identification information to enhance security and prevent potential misuse.
  - Route: `/api/cards/create-card`
  - Method: POST
  - Body: 
  ```
    {
    "balance": 5000
    }
    ```

- **Do a payment**: Description of the third endpoint.
  - Route: `/api/cards/{cardId}/pay`
  - Method: PUT
  - Parameters: cardId
  - Body:
  ```
    {
     "amount": 12
    }
    ```

- **Check balance**:
  - Route: `/api/cards/{cardId}/balance`
  - Method: GET
  - Parameters: cardId

## Technologies Used

- .NET Core 8
- SQL Server
- Docker

## Architecture Used
Hexagonal Architecture, also known as Ports and Adapters or the Onion Architecture, is a
software architectural pattern that focuses on separating concerns and promoting a clean and modular design. The core concept is to structure the application around its business
logic while keeping external dependencies, such as databases, frameworks, and UI, at the edges.
## Key Principles

1. **Separation of Concerns:** Hexagonal Architecture emphasizes the separation of business logic from external concerns. The core business logic resides at the center, and various adapters connect to the outside world.

2. **Dependency Inversion:** The architecture encourages the use of dependency inversion, where high-level modules (business logic) are not dependent on low-level modules (external services). Instead, both depend on abstractions.

3. **Testability:** By isolating the business logic from external dependencies, it becomes easier to test the application. Unit testing, integration testing, and system testing can be performed more efficiently.

4. **Adaptability:** The hexagonal structure allows the system to be more adaptable to changes. When external components change, only the corresponding adapters need modification, leaving the core business logic unaffected.


## Benefits

### 1. **Flexibility and Maintainability:**
   - **Scenario:** When there is a need to change the database or switch to a different UI framework.
   - **Benefit:** Hexagonal Architecture enables making changes without affecting the core business logic, resulting in a more flexible and maintainable system.

### 2. **Testability and Isolation:**
   - **Scenario:** Conducting unit tests or mocking external dependencies.
   - **Benefit:** Business logic is decoupled from external concerns, facilitating easier testing and isolation of components.

### 3. **Reduced Coupling:**
   - **Scenario:** Minimizing dependencies between different parts of the system.
   - **Benefit:** Hexagonal Architecture reduces coupling by enforcing a clear separation between the inner core and external components, making the system more modular.

### 4. **Portability:**
   - **Scenario:** Transitioning the application to different environments or platforms.
   - **Benefit:** The hexagonal structure allows the application to be more portable as long as the adapters for external interactions are adjusted.

## Contributing

If you would like to contribute, please fork the repository and submit a pull request.
