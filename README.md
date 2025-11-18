# 📧 EasyMail

**EasyMail** é uma API .NET para gerenciamento de clientes e envio de e-mails, construída seguindo os princípios de **Clean Architecture** e **SOLID**. O projeto oferece funcionalidades completas para cadastro de clientes e envio de e-mails através de SMTP para todos os clientes da base de dados.

## 🚀 Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core** - Web API
- **MongoDB** - Banco de dados NoSQL
- **MongoDB.Driver** - Driver oficial do MongoDB para .NET
- **SMTP** - Protocolo para envio de e-mails
- **Docker** - Containerização do MongoDB
- **Swagger/OpenAPI** - Documentação da API
- **Microsoft.Extensions.Logging** - Sistema de logs

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, organizando o código em camadas bem definidas:

```
├── Domain/                 # Camada de Domínio
│   ├── Entities/          # Entidades do domínio
│   ├── Interfaces/        # Contratos de repositórios
│   └── Common/            # Classes base e comuns
│
├── Application/           # Camada de Aplicação
│   ├── DTOs/             # Data Transfer Objects
│   ├── Services/         # Serviços de aplicação
│   ├── Interfaces/       # Contratos de serviços
│   └── Mappers/          # Mapeamento entre objetos
│
├── Infrastructure/        # Camada de Infraestrutura
│   ├── Data/             # Contexto do banco de dados
│   ├── Repositories/     # Implementação dos repositórios
│   ├── Services/         # Serviços externos (SMTP)
│   └── Configuration/    # Configurações
│
└── EasyMail/
    ├── Controllers/      # Controllers da API
    └── Program.cs        # Configuração da aplicação
```

## 📋 Funcionalidades

### 👥 Gerenciamento de Clientes
- ✅ Criar novos clientes
- ✅ Listar todos os clientes
- ✅ Validação de dados
- ✅ Logs detalhados

### 📧 Serviço de E-mail
- ✅ Envio de e-mails via SMTP
- ✅ Suporte a HTML e texto simples
- ✅ Configuração flexível de provedores
- ✅ Logs e monitoramento
- ✅ Tratamento robusto de erros

## 🛠️ Configuração e Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

### 1. Clone o repositório

```bash
git clone https://github.com/arthur-fenili/EasyMail.git
cd EasyMail
```

### 2. Configure o MongoDB via Docker

```bash
# Criar e executar container MongoDB
docker run -d \
  --name mongodb-easymail \
  -p 27017:27017 \
  mongo:latest

# Verificar se está rodando
docker ps
```

### 3. Configurar o appsettings.json

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "EasyMail"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "Username": "seu-email@gmail.com",
    "Password": "sua-senha-de-app",
    "EnableSsl": true,
    "FromEmail": "seu-email@gmail.com",
    "FromName": "EasyMail"
  }
}
```

### 4. Restaurar dependências e executar

```bash
# Restaurar pacotes NuGet
dotnet restore

# Executar a aplicação
dotnet run --project EasyMail/EasyMail.csproj
```

A API estará disponível em: `https://localhost:7014` ou `http://localhost:5023`

## 📚 Documentação da API

### Swagger UI
Acesse `https://localhost:7014/swagger` para a documentação interativa.

### Endpoints Principais

#### 👥 Clientes

**GET** `/api/client` - Listar todos os clientes
```http
GET /api/client
```

**POST** `/api/client` - Criar novo cliente
```http
POST /api/client
Content-Type: application/json

{
  "name": "João Silva",
  "email": "joao@email.com",
  "isActive": true
}
```

#### 📧 E-mails

**POST** `/api/email/send` - Enviar e-mail
```http
POST /api/email/send
Content-Type: application/json

{
  "to": "destinatario@email.com",
  "subject": "Assunto do E-mail",
  "body": "Corpo do e-mail",
  "isHtml": false
}
```

**Resposta de sucesso:**
```json
{
  "success": true,
  "message": "Email sent successfully.",
  "sentAt": "2024-11-18T01:30:00.000Z"
}
```

## ⚙️ Configuração de E-mail

### Gmail
1. Ative a verificação em duas etapas
2. Gere uma senha de app em [myaccount.google.com](https://myaccount.google.com/)
3. Use a senha de 16 caracteres (com espaços) em "Password" no `appsettings.json`


## 🐳 Docker

### MongoDB via Docker
```bash
# Executar MongoDB
docker run -d \
  --name mongodb-easymail \
  -p 27017:27017 \
  mongo:latest

# Parar
docker stop mongodb-easymail

# Iniciar novamente
docker start mongodb-easymail
```

## 🧪 Exemplos de Uso

### Criar um cliente
```bash
curl -X POST "https://localhost:7014/api/client" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Maria Santos",
    "email": "maria@empresa.com",
    "isActive": true
  }'
```

### Enviar um e-mail
```bash
curl -X POST "https://localhost:7014/api/email/send" \
  -H "Content-Type: application/json" \
  -d '{
    "to": "maria@empresa.com",
    "subject": "Bem-vinda!",
    "body": "<h1>Olá Maria!</h1><p>Bem-vinda ao nosso sistema!</p>",
    "isHtml": true
  }'
```

## 📊 Logs e Monitoramento

O sistema inclui logging detalhado nos níveis:

- **Information**: Operações bem-sucedidas
- **Error**: Falhas e exceções

Logs aparecem no console durante desenvolvimento e podem ser configurados para diferentes destinos em produção.

## 🚨 Tratamento de Erros

- **Validação de modelos**: Retorna `400 Bad Request` para dados inválidos
- **Erros de SMTP**: Tratamento específico para falhas de e-mail
- **Logs estruturados**: Facilita debug e monitoramento
- **Respostas consistentes**: Formato padronizado de resposta

## 🔧 Desenvolvimento

### Estrutura do Projeto
- **Injeção de Dependências**: Baixo acoplamento entre componentes
- **Clean Architecture**: Separação clara entre camadas
- **Testabilidade**: Interfaces permitem fácil criação de mocks

### Build do projeto
```bash
dotnet build
```

---
