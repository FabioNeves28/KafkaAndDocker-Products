# KafkaAndDocker‑Products

Aplicação em .NET (C#) para demonstrar a integração de produtos com **Apache Kafka**, com configuração via **Docker Compose**. Permite enviar e consumir eventos de produtos usando o Kafka, Docker, e tópicos específicos.

---

## Índice

- [Descrição](#descrição)  
- [Arquitetura](#arquitetura)  
- [Pré‑requisitos](#pré-requisitos)  
- [Instalação e Execução](#instalação-e-execução)  
- [Uso da API](#uso-da-api)  
- [Componentes principais](#componentes-principais)  
- [Deploy com Docker](#deploy-com-docker)  
- [Testes](#testes)  
- [Boas práticas SOLID/Arquitetura](#boas-práticas-solidarquitetura)  
- [Licença](#licença)

---

## Descrição

Este projeto demonstra como construir um sistema de produtos usando:

- Backend em **.NET 8**
- Envio e consumo de mensagens para Kafka
- Uso de **Docker Compose** para orquestração de containers: Zookeeper, Kafka, e app .NET

Vamos enviar eventos de criação de produto para o Kafka e consumir para processamento/armazenamento.

---

## Arquitetura

1. **Produtos.Api** – Web API para criação/listagem de produtos. Publica evento Kafka ao criar.
2. **Produtos.Kafka.Consumer** – Microserviço que consome eventos Kafka e processa lógica (ex: logging, armazenamento).
3. **Docker Compose (docker-compose.yml)** – Define containers:
   - Zookeeper
   - Kafka
   - API .NET
   - Consumer .NET

---

## Pré‑requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://docker.com)
- [Docker Compose] (v2)

---

## Instalação e Execução

1. Clone o projeto:
   ```bash
   git clone https://github.com/FabioNeves28/KafkaAndDocker-Products.git
   cd KafkaAndDocker-Products
   ```

2. Assegure-se de ter o .NET 8 instalado:
   ```bash
   dotnet --version
   ```

3. Faça build das aplicações:
   ```bash
   dotnet build
   ```

---

## Docker Compose

Inicie todos os serviços com:

```bash
docker-compose up --build
```

Isso vai subir:

- `zookeeper` e `kafka`
- `produtos.api` acessível em `http://localhost:5005`
- `produtos.consumer` rodando em container separado, consumindo o tópico Kafka

Para rodar em background:
```bash
docker-compose up -d
```

---

## Uso da API

### Endpoints disponíveis (porta 5005):

- `GET /api/Products` – lista produtos consumidos pelo Kafka
- `POST /api/Products` – cria produto e publica evento Kafka

#### Exemplo de request:

```http
POST http://localhost:5005/api/Products
Content-Type: application/json

{
  "name": "Camiseta Oficial",
  "price": 79.90
}
```

#### Exemplo de response consumido:
```json
{
  "id": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
  "name": "Camiseta Oficial",
  "price": 79.9,
  "createdAt": "2025-06-18T12:34:56Z"
}
```

---

## Componentes principais

- **Domain**: classes `Product`, `ProductCreatedEvent`
- **API**: controller `ProductsController`, serviço `ProductService` que publica evento em Kafka
- **Consumer**: `ProdutoConsumerService` que consome eventos do tópico `produto-created`, exibe no console e armazena em banco (em memória ou persistente)

---

## Testes / Exemplos

1. Subir containers com `docker-compose`
2. Enviar POST conforme exemplo acima
3. No console do consumer será exibido algo como:

```
Recebido evento: { "id":"...","name":"Camiseta Oficial","price":79.9,"createdAt":"..."}
```

4. Consultar `GET /api/Products` retorna o produto consumido.

---

## Boas práticas SOLID / Arquitetura

- **Domain** define entidades e eventos
- **Services** implementam lógica (publicação/consumo Kafka)
- **Infra** contém implementações concretas (Produzir/consumir Kafka)
- **API** implementa endpoints e injeta dependências via interfaces
- **Consumer** segue separação de responsabilidades

---

## Futuras melhorias

- Persistência em banco de dados (PostgreSQL, etc.)
- Balanceamento/escalonamento de consumidores
- Suporte a envio via UI ou Blazor
- Testes unitários/integration com Kafka embedded

---

## Licença

Projeto disponibilizado com licença MIT.  
© 2025 Fábio Neves
