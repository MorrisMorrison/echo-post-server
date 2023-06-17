# EchoPost

EchoPost is a powerful and flexible social media management tool that enables users to seamlessly create, schedule, and post messages across multiple social media platforms.

## Table of Contents

- [EchoPost](#echopost)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Features](#features)
  - [Technologies](#technologies)
  - [Getting Started](#getting-started)
  - [Usage](#usage)
  - [Web Frontend](#web-frontend)
  - [Change Database to MSSQL](#change-database-to-mssql)
    - [Install Package](#install-package)
    - [Change ConnectionString in appsettings.json](#change-connectionstring-in-appsettingsjson)
    - [Wire up service in ConfigureServices.cs](#wire-up-service-in-configureservicescs)
  - [API Documentation](#api-documentation)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)
  - [Thanks To](#thanks-to)

## Introduction

EchoPost is a robust social media management tool designed with Clean Architecture principles in mind. By adhering to Clean Architecture, we aim to achieve a highly maintainable, testable, and scalable codebase that is easy to understand and extend. The architecture separates concerns into distinct layers, promoting a clear separation of business logic from infrastructure and ensuring a high level of modularity. 

## Features

- Easy message creation and scheduling
- Support for multiple social media platforms including Facebook, Instagram, and Twitter
- Intuitive user interface for streamlined management
- OAuth authentication for secure access to social media accounts
- API integration for developers to incorporate EchoPost functionality into their applications

## Technologies
- .NET 7
- Entity Framework Core

## Getting Started

To get started with EchoPost, follow these steps:

1. Clone the repository: `git clone https://github.com/yourusername/echopost.git`
2. Navigate to `src/WebUI`
3. Install the necessary dependencies: `npm install`
4. Configure your settings in the `appsettings.{ENV}.json` file.
5. Run the application: `dotnet run`

## Usage

Once the application is running, you can access the EchoPost user interface through your web browser. From there, you can create and schedule messages for posting on different social media platforms. The user interface provides an intuitive and user-friendly experience, allowing you to efficiently manage your social media content.

## Web Frontend
- Angular Frontend (https://github.com/MorrisMorrison/echo-post-angular-client)

## Change Database to MSSQL
### Install Package
`cd src/Infrastructure`
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer `
### Change ConnectionString in appsettings.json
`"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EchoPostDb;Trusted_Connection=True;MultipleActiveResultSets=true"`
### Wire up service in ConfigureServices.cs
```            
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
```

## API Documentation

The EchoPost API allows developers to integrate EchoPost functionality into their own applications. For detailed API documentation, refer to the [API Documentation](api-docs.md) file.

## Contributing

We welcome contributions to EchoPost! If you would like to contribute, please follow these guidelines:

- Fork the repository and create your branch: `git checkout -b feature/your-feature`
- Commit your changes: `git commit -am 'Add your feature'`
- Push to the branch: `git push origin feature/your-feature`
- Create a pull request detailing your changes and enhancements.

## License

EchoPost is released under the [MIT License](LICENSE).

## Contact

For any questions, feedback, or support, please contact our team at echo.post@example.com.


## Thanks To
- Jason Taylor - Clean Architecture Project Template (https://github.com/jasontaylordev/CleanArchitecture)