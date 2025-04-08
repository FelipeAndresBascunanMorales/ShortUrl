# Proyecto de Acortador de URLs
Este proyecto es una aplicación para acortar URLs, gestionar su ciclo de vida y redirigir a los usuarios a las URLs originales.
Esto es una prueba de conceptos para demostrar una implementación de arquitectura hexagonal.

## Diagrama 

```mermaid
%%{init: {'theme': 'base', 'themeVariables': { 'primaryColor': '#ffdfd3', 'edgeLabelBackground':'#fff'}}}%%
graph TD
    subgraph External Interfaces
        A[API Clients]
        C[SII SOAP API]
        D[InMemory DB]
    end

    subgraph Infrastructure
        E[API Controllers]
        F[Repositories]
        G[External Services]
    end

    subgraph Application
        H[Use Cases]
        I[DTOs]
    end

    subgraph Domain
        J[Entities]
        K[Business Rules]
        L[Interfaces]
    end

    %% Connections
    A -->|HTTP Requests| E
    C -->|SOAP Calls| G
    D -->|Persistence| F
    
    E -->|Calls| H
    H -->|Implements| L
    H -->|Uses| J
    H -->|Uses| K
    
    F -->|Implements| L
    G -->|Implements| L
    
    J --> K
    L -->|Abstractions| J
    
    style Infrastructure fill:#fafad2,stroke:#333
    style Application fill:#e0ffff,stroke:#333
    style Domain fill:#98fb98,stroke:#333
```

## Características
- Crear URLs cortas a partir de URLs originales.
- Redirigir a la URL original utilizando el código corto.
- Manejo de expiración de URLs.
- Límite de accesos configurables para cada URL corta.

## Requisitos Previos
DOTNET 9 instalado en su sistema.
Un editor de código como Visual Studio o Visual Studio Code.
Instalación:
- Clonar este repositorio
- abrir el archivo sln en la carpeta principal
- dirigirse a WebApi
- en la carpeta WebApi ejecutar el siguiente comando
  ```
  dotnet build
  dotnet run
  ```
