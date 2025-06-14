# AI-Powered Distributed Architecture with .NET Aspire

This repository is a study project based on the Udemy course:

**Develop AI-Powered Distributed Architecture w/ PostgreSQL, Redis, RabbitMQ, Keycloak, Ollama, VectorDB using .NET Aspire**  
by Mehmet Ozkaya  
[Course Link](https://www.udemy.com/course/net-aspire-and-genai-develop-distributed-architectures/)

## Project Overview

This solution demonstrates a modern distributed architecture using .NET Aspire (targeting .NET 9), integrating the following technologies:

- **PostgreSQL** for persistent data storage
- **Redis** for caching
- **RabbitMQ** for messaging
- **Keycloak** for authentication and authorization
- **Ollama** for AI model serving (including Llama3.2 and all-minilm models)
- **VectorDB** for vector-based data operations
- **OpenTelemetry** for observability and tracing
- **Service Discovery** and **Resilience** patterns

The solution is organized into multiple projects/services (Catalog, Basket, WebApp, etc.), each following best practices for distributed systems and microservices.

## What Has Been Done

- Set up a distributed application using .NET Aspire's builder and service orchestration features.
- Integrated PostgreSQL, Redis, RabbitMQ, Keycloak, and Ollama as backing services.
- Implemented service-to-service communication, health checks, and OpenTelemetry-based observability.
- Used modern .NET 9 features and Aspire packages for configuration and service defaults.
- Added AI capabilities via Ollama and vector search.

## Notes on Deprecations

Some dependencies, APIs, or configuration patterns from the original course have been updated or replaced due to deprecations in the .NET ecosystem or third-party libraries. All changes were made to ensure compatibility with the latest .NET 9 and Aspire releases.

---
This project is for educational purposes and follows the structure and concepts taught in the referenced course.
