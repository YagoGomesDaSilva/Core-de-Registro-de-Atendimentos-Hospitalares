# 🏥 Core de Registro de Atendimentos Hospitalares

Sistema web para gerenciamento de pacientes e registro de atendimentos hospitalares, desenvolvido em **ASP.NET MVC 5 / Web API 2** com **.NET Framework 4.8**, seguindo os princípios da **Clean Architecture**.

---

## 📋 Visão Geral

O sistema permite o cadastro de pacientes e o registro de atendimentos clínicos, controlando sinais vitais (pressão arterial, temperatura e frequência cardíaca) e o ciclo de vida de cada atendimento por meio de um status (`Ativo` / `Finalizado`).

### Principais Funcionalidades

| Módulo | Funcionalidades |
|---|---|
| **Pacientes** | Cadastro, listagem, edição e exclusão. Validação de CPF (formato e unicidade). |
| **Atendimentos** | Registro, listagem de histórico, edição de sinais vitais/status e exclusão. Regra de negócio: apenas **um atendimento ativo por paciente**. |
| **API REST** | Endpoints completos (CRUD) em `/api/paciente` e `/api/atendimento`, documentados via **Swagger**. |
| **Interface Web** | Views MVC com **Bootstrap 5** para operação do sistema via navegador. |

---

## 🏗️ Arquitetura

O projeto segue **Clean Architecture**, organizado em cinco camadas:

```
┌──────────────────────────────────────────────────┐
│                  WebApplication                  │  ← Apresentação (MVC + Web API)
│           Depende de: Application, CrossCutting  │
├──────────────────────────────────────────────────┤
│                  CrossCutting                    │  ← IoC / Injeção de Dependência
│           Conecta todas as camadas               │
├──────────────────────────────────────────────────┤
│                  Application                     │  ← Serviços, DTOs, Mapeamentos
│           Depende de: Domain                     │
├──────────────────────────────────────────────────┤
│                  Infraestructure                 │  ← Repositórios, DbContext, Mappings EF
│           Depende de: Domain                     │
├──────────────────────────────────────────────────┤
│                    Domain                        │  ← Entidades, Interfaces, Exceções
│           Nenhuma dependência externa             │
└──────────────────────────────────────────────────┘
```

| Projeto | Responsabilidade |
|---|---|
| **Domain** | Entidades (`Paciente`, `Atendimento`), interfaces de repositório e exceções de domínio. |
| **Application** | Serviços de aplicação, DTOs, perfil AutoMapper e exceções de serviço (`ServiceException`, `NotFoundException`). |
| **Infraestructure** | Implementação dos repositórios, `HospitalContext` (Entity Framework 6) e mapeamentos Fluent API. |
| **CrossCutting** | Registro de dependências via **Simple Injector** (`NativeInjectorBootStrapper`). |
| **WebApplication** | Controllers MVC, Controllers Web API, filtro global de exceções, configuração Swagger e Views Razor. |

---

## 🛠️ Tecnologias e Dependências

| Tecnologia | Versão | Finalidade |
|---|---|---|
| .NET Framework | 4.8 | Runtime da aplicação |
| ASP.NET MVC | 5.2.9 | Interface web (Views Razor) |
| ASP.NET Web API | 5.2.9 | API REST |
| Entity Framework | 6.5.1 | ORM — acesso a dados |
| SQL Server | 2022 | Banco de dados relacional |
| AutoMapper | 10.1.1 | Mapeamento Entidade ↔ DTO |
| Simple Injector | 5.5.0 | Container de injeção de dependência |
| Swashbuckle | 5.6.0 | Documentação Swagger / Swagger UI |
| Newtonsoft.Json | 13.0.3 | Serialização JSON |
| Bootstrap | 5.x | Framework CSS para as Views |
| Docker / Docker Compose | — | Containerização do banco de dados |

---

## ✅ Pré-requisitos

Antes de executar o projeto, certifique-se de ter instalado:

- [**Visual Studio 2019+**](https://visualstudio.microsoft.com/) com a carga de trabalho **ASP.NET e desenvolvimento Web**
- [**.NET Framework 4.8 Developer Pack**](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [**SQL Server 2019+**](https://www.microsoft.com/sql-server) (local) **ou** [**Docker Desktop**](https://www.docker.com/products/docker-desktop/) para subir o banco via container
- [**Git**](https://git-scm.com/)

---

## 🗄️ Configuração do Banco de Dados

O projeto utiliza **SQL Server** com o banco `HospitalDB`. Existem duas formas de configurá-lo:

### Opção 1 — Docker Compose (recomendado)

O repositório já inclui um `dockerfile` e `Docker-Compose.yml` que sobem uma instância do SQL Server 2022 e executam automaticamente o script `init.sql` (criação do banco, tabelas e dados iniciais).

```bash
# Na raiz do repositório
docker-compose up -d --build
```

Aguarde cerca de **20 segundos** para que o SQL Server inicie e o script seja aplicado. O container ficará disponível em:

| Parâmetro | Valor |
|---|---|
| Host | `localhost` |
| Porta | `1433` |
| Usuário | `sa` |
| Senha | `Password@123!` |
| Banco | `HospitalDB` |

Para verificar se o container está rodando:

```bash
docker ps
```

Para parar o container:

```bash
docker-compose down
```

> **Nota:** Os dados são persistidos no volume `sqlvolume`. Para reiniciar do zero, remova o volume: `docker-compose down -v`.

### Opção 2 — SQL Server local

1. Conecte-se à sua instância do SQL Server (SSMS, Azure Data Studio, etc.).
2. Execute o script `init.sql` localizado na raiz do repositório. Ele irá:
   - Criar o banco `HospitalDB` (se não existir)
   - Criar as tabelas `Paciente` e `Atendimento`
   - Inserir dados de exemplo (2 pacientes e 2 atendimentos)
3. Ajuste a connection string no arquivo `WebApplication\Web.config` se necessário:

```xml
<connectionStrings>
  <add name="HospitalDbConnection"
       connectionString="Server=localhost,1433;Database=HospitalDB;User Id=sa;Password=Password@123!;TrustServerCertificate=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

> **Importante:** Caso use uma instância nomeada (ex.: `.\SQLEXPRESS`), autenticação Windows ou porta diferente, altere os valores de `Server`, `User Id` e `Password` de acordo com seu ambiente.

---

## 🚀 Executando a Aplicação

### 1. Clonar o repositório

```bash
git clone https://github.com/YagoGomesDaSilva/Core-de-Registro-de-Atendimentos-Hospitalares.git
cd Core-de-Registro-de-Atendimentos-Hospitalares
```

### 2. Subir o banco de dados

```bash
docker-compose up -d --build
```

Ou execute o `init.sql` manualmente conforme descrito na seção anterior.

### 3. Restaurar pacotes NuGet

Abra a solução `.sln` no **Visual Studio**. Os pacotes serão restaurados automaticamente no build. Caso contrário, execute:

```
Menu → Ferramentas → Gerenciador de Pacotes NuGet → Console do Gerenciador de Pacotes
PM> Update-Package -reinstall
```

### 4. Compilar e executar

1. Defina o projeto **WebApplication** como projeto de inicialização (clique com botão direito → *Definir como Projeto de Inicialização*).
2. Pressione **F5** (ou **Ctrl+F5** para executar sem depuração).
3. O IIS Express iniciará a aplicação.

### 5. Acessar o sistema

| Recurso | URL |
|---|---|
| **Página Inicial** | `http://localhost:{porta}/` |
| **Swagger (Documentação da API)** | `http://localhost:{porta}/swagger` |
| **API de Pacientes** | `http://localhost:{porta}/api/paciente` |
| **API de Atendimentos** | `http://localhost:{porta}/api/atendimento` |

> A porta é atribuída automaticamente pelo IIS Express. Verifique a URL na barra de endereços do navegador ou nas propriedades do projeto.

---

## 📡 Endpoints da API

### Pacientes — `/api/paciente`

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/paciente` | Lista todos os pacientes |
| `GET` | `/api/paciente/{id}` | Obtém paciente por ID |
| `POST` | `/api/paciente` | Cadastra novo paciente |
| `PUT` | `/api/paciente/{id}` | Atualiza paciente existente |
| `DELETE` | `/api/paciente/{id}` | Remove paciente |

### Atendimentos — `/api/atendimento`

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/atendimento` | Lista histórico de atendimentos |
| `GET` | `/api/atendimento/{id}` | Obtém atendimento por ID |
| `GET` | `/api/atendimento/pacientesDisponiveis` | Lista pacientes sem atendimento ativo |
| `POST` | `/api/atendimento` | Registra novo atendimento |
| `PUT` | `/api/atendimento/{id}` | Atualiza atendimento (sinais vitais/status) |
| `DELETE` | `/api/atendimento/{id}` | Remove atendimento |

---

## 📂 Estrutura de Pastas

```
├── Application/
│   ├── DTO/                  # Data Transfer Objects
│   ├── Exceptions/           # Exceções de serviço
│   ├── Mappings/             # Perfis AutoMapper
│   └── Services/             # Serviços de aplicação + interfaces
├── CrossCutting/
│   └── DependenciesApp/      # Registro de injeção de dependência
├── Domain/
│   ├── Entities/             # Entidades de domínio
│   ├── Exceptions/           # Exceções de domínio
│   └── Interfaces/           # Contratos de repositório
├── Infraestructure/
│   ├── Context/              # DbContext (Entity Framework)
│   ├── Mappings/             # Mapeamentos Fluent API
│   └── Repositories/         # Implementação dos repositórios
├── WebApplication/
│   ├── Api/                  # Controllers Web API
│   ├── App_Start/            # Configurações (rotas, Swagger, Web API)
│   ├── Controllers/          # Controllers MVC
│   ├── ExceptionFilters/     # Filtro global de exceções da API
│   ├── Views/                # Views Razor + Layout
│   └── Web.config            # Configuração e connection string
├── dockerfile                # Imagem Docker do SQL Server
├── Docker-Compose.yml        # Orquestração do container do banco
├── init.sql                  # Script de criação do banco e dados iniciais
└── README.md
```

---

## 📌 Regras de Negócio Importantes

- **CPF único:** Não é permitido cadastrar dois pacientes com o mesmo CPF. O CPF também é validado por dígitos verificadores.
- **Atendimento ativo único:** Um paciente só pode ter **um atendimento com status "Ativo"** por vez. Para registrar um novo, o anterior deve ser finalizado.
- **Status do atendimento:** Todo atendimento é criado com status `Ativo` e pode ser alterado para `Finalizado` via edição.

---
