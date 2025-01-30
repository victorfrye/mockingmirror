# Mocking Mirror

🤡 Mirror mirror on the wall, who knows nothing about AI at all?

## Overview

This repository contains the public code for the EDJEovation Days 2024 hackathon project to create an AI solution that can comment on video of a user and provide them with feedback on their appearance, with a sarcastic twist. The project is called the Mocking Mirror.

This version of the project has been modified for public use and maintenance by myself. The original project was created by a team of Leading EDJE developers.

The solution for this hackathon includes:

🌐 A React web client application for user interactivity and presentation.

💜 An ASP.NET Core Web API that processes an image and returns text and audio responses.

✨ Azure AI services for generative image-to-text and text-to-speech synthesis.

🛠️ .NET Aspire for orchestration of the local development environment.

## Table of Contents

- [Overview](#overview)
- [Table of Contents](#table-of-contents)
- [Get Started](#get-started)
  - [Prerequisites](#prerequisites)
  - [.NET Aspire](#net-aspire)
  - [HTTPS](#https)
  - [Clone the repo](#clone-the-repo)
  - [Run the app](#run-the-app)
- [Acknowledgements](#acknowledgements)

## Get Started

### Prerequisites

To run this project, you will need to have the following software installed on your machine:

- [Git](https://git-scm.com/downloads)
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js](https://nodejs.org/en/download/)
- An OCI compliant container runtime
  - [e.g., Docker Desktop](https://www.docker.com/get-started/)
- An IDE or text editor of your choice
  - [e.g., Visual Studio](https://visualstudio.microsoft.com/downloads/)
  - [e.g., Visual Studio Code](https://code.visualstudio.com/download)

*WARNING: Additional changes are required if using an alternative to Docker Desktop.*

### .NET Aspire

This project uses .NET Aspire to orchestrate the local development environment. This simplifies the process of running the frontend and backend services together.

For more information on or troubleshooting .NET Aspire, see the [Aspire documentation](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview).

### HTTPS

This project uses HTTPS for local development. This requires the use of a trusted certificate. Before running the application, the following command will check for an existing certificate or generate a new one.

```pwsh
dotnet dev-certs https --trust
```

### Clone the repo

To clone the repository, run the following command in your terminal:

```pwsh
git clone https://github.com/victorfrye/mockingmirror.git
```

### Run the app

To run the application, simply run the following command in the root of the project:

```pwsh
dotnet run --project .\src\AppHost\AppHost.csproj
```

## Acknowledgements

This project was created by a team of Leading EDJE developers for the EDJEovation Days 2024 hackathon. It won second place. The original project was created by:

- [Victor Frye](https://linkedin.com/in/victorfrye)
- [Matt Eland](https://linkedin.com/in/matteland/)
- [Terry Welsh](https://linkedin.com/in/terry-welsh/)
- [Jon Trotter](https://linkedin.com/in/jon-trotter/)
- [Bob Fornal](https://linkedin.com/in/rfornal/)
- [Ben Schwemlein](https://linkedin.com/in/benschwemlein/)
- [Julie Yakunich](https://linkedin.com/in/julieyakunich/)
- [Dave Michels](https://linkedin.com/in/davidmichels/)

The project was modified for public use and maintenance by myself, Victor Frye. However, it would not have been a reality if not for each of these individuals and the support of [Leading EDJE](https://leadingedje.com/).
