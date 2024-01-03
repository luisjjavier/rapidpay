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
- [Contributing](#contributing)
- [License](#license)

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
- **Create a new card**: This will create a new card
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
  - Method: POST
  - Parameters: cardId
  - Body:
  ```
    {
     "cardId": "71192eb7-9538-46a2-b7b8-08dc0b9c9c68",
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

## Contributing

If you would like to contribute, please fork the repository and submit a pull request.
