# Sistema de Reservas (Booking System)

Este proyecto es una API RESTful desarrollada con .NET para gestionar un sistema de reservas. Permite administrar diversas entidades como clientes, habitaciones, ubicaciones y usuarios, junto con un sistema de roles y permisos.

## Características Principales

El sistema expone varios endpoints para gestionar los siguientes módulos:

*   **Bookings (Reservas):** Crear, leer, actualizar y eliminar reservas.
*   **Clients (Clientes):** Administrar la información de los clientes.
*   **Rooms (Habitaciones):** Gestionar las habitaciones disponibles para reservar.
*   **Locations (Ubicaciones):** Administrar las ubicaciones donde se encuentran las habitaciones.
*   **Users (Usuarios):** Gestionar los usuarios del sistema.
*   **Roles y Permisos:** Asignar roles a los usuarios y definir permisos específicos para cada rol, controlando el acceso a las diferentes funcionalidades de la API.

## Arquitectura del Proyecto

El proyecto sigue una arquitectura limpia (Clean Architecture) para separar las responsabilidades y mejorar la mantenibilidad y escalabilidad.

*   **`Domain`**: Contiene las entidades del negocio (modelos), las interfaces de los repositorios y la unidad de trabajo (Unit of Work). Es el núcleo del proyecto y no depende de ninguna otra capa.
*   **`Application`**: Contiene la lógica de la aplicación, los servicios, los DTOs (Data Transfer Objects) y las interfaces de los servicios. Orquesta el flujo de datos entre la capa de presentación y el dominio.
*   **`Infrastructure`**: Contiene las implementaciones concretas de las interfaces definidas en las otras capas. Aquí se encuentra la configuración de la base de datos (Entity Framework Core), los repositorios y otros servicios externos.
*   **`Api`**: Es la capa de presentación. En este caso, una API RESTful que expone los endpoints para interactuar con la aplicación.

## Tecnologías Utilizadas

*   **.NET 8**
*   **ASP.NET Core Web API**
*   **Entity Framework Core**
*   **MySQL**
*   **Arquitectura Limpia (Clean Architecture)**
*   **Patrón Repositorio y Unidad de Trabajo (Repository and Unit of Work Pattern)**

## Cómo Empezar

1.  **Clonar el repositorio:**
    ```bash
    git clone <URL_DEL_REPOSITORIO>
    ```
2.  **Configurar la conexión a la base de datos:**
    Modifica el archivo `appsettings.Development.json` en el proyecto `Api` con tu cadena de conexión.
3.  **Aplicar las migraciones:**
    Abre una terminal en el directorio del proyecto `Infrastructure` y ejecuta:
    ```bash
    dotnet ef database update
    ```
4.  **Ejecutar la aplicación:**
    Puedes ejecutar el proyecto desde Visual Studio o usando el siguiente comando en la raíz del proyecto:
    ```bash
    dotnet run --project Api/Api.csproj
    ```