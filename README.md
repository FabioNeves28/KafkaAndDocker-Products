# Ecommerce Microservices

Este projeto é uma solução de e-commerce baseada em microserviços, desenvolvida em .NET 8, utilizando Blazor para a interface, ASP.NET Core para APIs, Entity Framework Core para persistência e Kafka para mensageria.

---

## Estrutura dos Projetos

- **Ecommerce.AppHost**  
  Projeto principal para inicialização e orquestração dos serviços (pode conter configurações globais e arquivos como kafka.yml).

- **Ecommerce.Model**  
  Contém as classes de modelo compartilhadas entre os serviços, como `OrderModel` e `ProductModel`.

- **Ecommerce.OrderService**  
  Microserviço responsável pelo gerenciamento de pedidos.  
  - API REST para criação e consulta de pedidos.  
  - Integração com Kafka para publicação de eventos de pedidos.

- **Ecommerce.ProductService**  
  Microserviço responsável pelo gerenciamento de produtos.  
  - API REST para consulta de produtos.  
  - Consome eventos do Kafka (exemplo: atualização de estoque).

- **Ecommerce.Tests**  
  Projeto de testes unitários utilizando xUnit e Moq, cobrindo os principais fluxos dos serviços.

---

## Tecnologias Utilizadas

- .NET 8  
- Blazor (para interface web)  
- ASP.NET Core Web API  
- Entity Framework Core (com SQL Server e InMemory para testes)  
- Apache Kafka (mensageria)  
- xUnit e Moq (testes unitários)  
- Docker (opcional, para infraestrutura)

---

## Como Executar

### 1. Configurar o Banco de Dados

Certifique-se de que o SQL Server está disponível e as strings de conexão estão corretas nos arquivos `appsettings.json` dos serviços.

### 2. Configurar o Kafka

Utilize o arquivo `kafka.yml` para subir o Kafka via Docker Compose:

```bash
docker compose -f kafka.yml up -d
```

### 3. Executar os Serviços

Inicie os projetos `Ecommerce.OrderService` e `Ecommerce.ProductService` via Visual Studio ou CLI:

```bash
dotnet run --project Ecommerce.OrderService
dotnet run --project Ecommerce.ProductService
```

### 4. Executar a Interface Blazor

Inicie o projeto Blazor (caso exista um projeto como `Ecommerce.BlazorApp`):

```bash
dotnet run --project Ecommerce.BlazorApp
```

---

## Endpoints Principais

### OrderService

- `GET /api/order`  
  Lista todos os pedidos.

- `POST /api/order`  
  Cria um novo pedido.

### ProductService

- `GET /api/product`  
  Lista todos os produtos.

- `GET /api/product/{id}`  
  Consulta um produto pelo ID.

---

## Padrões e Boas Práticas

- **SOLID**: Serviços e controllers seguem princípios de responsabilidade única e injeção de dependências.  
- **Testes**: Cobertura de testes unitários para regras de negócio e persistência.  
- **Mensageria**: Integração com Kafka para comunicação assíncrona entre serviços.

---

## Observações

- Ajuste as strings de conexão e configurações de Kafka conforme seu ambiente.  
- O projeto pode ser expandido com autenticação, autorização, logs e monitoramento.
