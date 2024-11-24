
# Banking System Project

This repository contains the implementation of a Banking System application. It is built following key software design principles like SOLID, Dependency Injection, and Clean Code practices.

---

## Project Structure

The project is organized as follows:

- **BankingSystem.Application**
  - Contains business logic, services, and utility classes for processing transactions, interest rules, and generating statements.

- **BankingSystem.ConsoleApp**
  - Entry point of the application. Provides a console-based interface for the user to interact with the banking system.

- **BankingSystem.Domain**
  - Contains the core domain entities, constants, and interfaces. Implements the transaction and interest processors.

- **BankingSystem.Infrastructure**
  - Provides in-memory repositories for managing data persistence.

- **BankingSystem.Tests**
  - Includes integration tests to validate the functionality of the application.

---

## Key Features

- **Transaction Management**: Add and validate transactions with types (Deposit/Withdrawal).
- **Interest Rules**: Add and update interest rules with validation.
- **Statement Generation**: Generate statements summarizing transactions and interest calculations.

---

## Design Principles and Patterns Used

- **SOLID Principles**: Each class has a single responsibility, and dependencies are injected using interfaces.
- **Dependency Injection**: Configured through the `DependencyInjection` class to decouple components.
- **Layered Architecture**: Separation of concerns between domain, application, infrastructure, and user interface layers.
- **Value Objects**: `InterestRate` is encapsulated as a value object for validation and immutability.
- **Integration Tests**: Ensures end-to-end functionality through mock in-memory repositories.

---

## How to Run

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory and restore dependencies:
   ```bash
   cd BankingSystem
   dotnet restore
   ```

3. Run the console application:
   ```bash
   dotnet run --project BankingSystem.ConsoleApp
   ```

4. Run the tests:
   ```bash
   dotnet test
   ```

---

## Metrics and Code Quality

- **Code Metrics**: Follows Clean Code standards. Each method is concise, with clear separation of logic.
- **Test Coverage**: Ensures validation of all key features through comprehensive integration tests.

---

## Folder Structure

```plaintext
BankingSystem/
├── BankingSystem.Application/
│   ├── Constants/
│   ├── Interfaces/
│   ├── Services/
│   ├── Utilities/
│   └── ...
├── BankingSystem.ConsoleApp/
│   ├── Constants/
│   ├── DependencyInjection.cs
│   └── Program.cs
├── BankingSystem.Domain/
│   ├── Constants/
│   ├── Entities/
│   ├── Services/
│   ├── Interfaces/
│   ├── ValueObjects/
│   └── ...
├── BankingSystem.Infrastructure/
│   ├── Repositories/
│   └── ...
├── BankingSystem.Tests/
│   ├── BankingSystemIntegrationTests.cs
│   └── ...
└── README.md
