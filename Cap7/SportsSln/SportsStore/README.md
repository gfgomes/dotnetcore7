# 🐳 SQL Server no Docker para Desenvolvimento (.NET + EF Core)

Este projeto utiliza o **Microsoft SQL Server 2022 em container Docker** como banco de dados para desenvolvimento, substituindo o LocalDB. Isso garante portabilidade, persistência e independência entre os ambientes de trabalho e casa.
https://hub.docker.com/r/microsoft/mssql-server

Featured Tags

- 2022-latest

```docker pull mcr.microsoft.com/mssql/server:2022-latest```

- 2019-latest

```docker pull mcr.microsoft.com/mssql/server:2019-latest```

- 2017-latest

```docker pull mcr.microsoft.com/mssql/server:2017-latest```

---

## ✅ Requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução
- .NET 7 ou 8 instalado (`dotnet --version`)
- EF Core CLI instalado (se necessário):

```bash
dotnet tool install --global dotnet-ef
```

---

## 🔐 Senha do usuário `sa`

A senha do usuário `sa` é definida por você **no momento da criação do container**. No exemplo abaixo, usamos:

```text
@Sql2022
```

Você pode alterar para qualquer senha forte que atenda aos requisitos da Microsoft:

- Mínimo 8 caracteres
- Pelo menos uma letra maiúscula
- Pelo menos uma letra minúscula
- Um número e um símbolo

---

## ⚙️ Opção 1 – Rodando com `docker run` (persistente)

Crie um volume nomeado:

```bash
docker volume create sqlserver2022volume
```

Execute o container:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" ^
  -p 1433:1433 --name sqlserver2022dev ^
  -v sqlserver2022volume:/var/opt/mssql ^
  -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## ⚙️ Opção 2 – Usando `docker-compose`

Crie um arquivo chamado `docker-compose.yml` com o seguinte conteúdo:

```yaml
version: '3.9'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022dev
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "@Sql2022"
    ports:
      - "1433:1433"
    volumes:
      - sql_data:/var/opt/mssql
    restart: unless-stopped

volumes:
  sql_data:
```

### Comandos:

- Subir o container:
  ```bash
  docker-compose up -d
  ```

- Parar o container:
  ```bash
  docker-compose down
  ```

- Subir novamente mantendo os dados:
  ```bash
  docker-compose up -d
  ```

---

## 🔌 Connection string para `appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "SportsStoreConnection": "Server=localhost,1433;Database=SportsStore;User Id=sa;Password=@Sql2022;TrustServerCertificate=True"
  }
}
```

> `TrustServerCertificate=True` é necessário porque usamos SSL self-signed no ambiente local.

---

## 🧱 Usando Entity Framework Core

### Criar migration (caso ainda não exista):

```bash
dotnet ef migrations add Initial
```

### Aplicar migration no banco de dados (container Docker):

```bash
dotnet ef database update
```

### Listar migrations existentes:

```bash
dotnet ef migrations list
```

---

## 🧹 Manutenção e limpeza

### Parar e remover o container (sem apagar os dados):

```bash
docker stop sqlserver2022dev
docker rm sqlserver2022dev
```

### Remover volume (isso apaga o banco de dados):

```bash
docker volume rm sqlserver2022volume
```

---

## 📁 Estrutura esperada do projeto

```
/Migrations
/appsettings.json
/appsettings.Development.json
/docker-compose.yml
/README.md
```

---

## 🌍 Ambiente de desenvolvimento

Use a variável de ambiente `ASPNETCORE_ENVIRONMENT` para garantir que a aplicação use `appsettings.Development.json`.

### No PowerShell (Windows):

```bash
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
```

---

## 🚀 Vantagens

- Elimina dependência do LocalDB (Windows-only)
- Compatível com Linux, macOS, WSL e CI/CD
- Fácil replicação entre ambientes de trabalho e casa
- Persistência de dados via volumes Docker
- Ideal para testes, homologação e desenvolvimento local

---

## 📌 Observações

- O nome `SportsStoreConnection` deve ser utilizado no código C# com:
  ```csharp
  builder.Configuration.GetConnectionString("SportsStoreConnection")
  ```
- A senha `@Sql2022` pode ser alterada conforme sua preferência. Apenas lembre de atualizar também na string de conexão.

# 🐳 SQL Server no Docker com Volume Persistente (GUI, CLI, Scripts)

Este guia mostra como executar o **SQL Server 2022 no Docker com volume persistente**, de três formas:

- Pela interface gráfica do **Docker Desktop (GUI)**
- Pela **linha de comando (CLI)**
- Via **scripts `.ps1` (PowerShell) e `.cmd` (Windows)**

---

## ✅ 1. Usando Docker Desktop (GUI)

### Criar volume:

1. Abra o Docker Desktop
2. Vá na aba **Volumes**
3. Clique em **Create**
4. Nomeie como:  
   ```
   sqlserver2022volume
   ```
5. Clique em **Create**

### Criar container:

1. Vá em **Containers** → **Create**
2. Digite a imagem:  
   ```
   mcr.microsoft.com/mssql/server:2022-latest
   ```
3. Clique em **Advanced Settings**

#### Aba **Volumes**:
- Clique em **Add Volume**
- Use o volume existente: `sqlserver2022volume`
- Container mount path: `/var/opt/mssql`

#### Aba **Environment**:
Adicione:

| Nome               | Valor        |
|--------------------|--------------|
| ACCEPT_EULA        | Y            |
| MSSQL_SA_PASSWORD  | @Sql2022     |

#### Aba **Ports**:
- Container: `1433`  
- Host: `1433`

#### Nome do container:
```
sqlserver2022dev
```

Clique em **Run** para iniciar.

---

## ✅ 2. Usando linha de comando (CLI)

### Criar volume:

```bash
docker volume create sqlserver2022volume
```

### Rodar container com `docker run`:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" ^
  -p 1433:1433 --name sqlserver2022dev ^
  -v sqlserver2022volume:/var/opt/mssql ^
  -d mcr.microsoft.com/mssql/server:2022-latest
```

> Substitua `@Sql2022` por uma senha forte, se desejar.

---

### Usar com `docker-compose.yml`:

Crie um arquivo chamado `docker-compose.yml` com:

```yaml
version: '3.9'

services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022dev
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "@Sql2022"
    ports:
      - "1433:1433"
    volumes:
      - sql_data:/var/opt/mssql
    restart: unless-stopped

volumes:
  sql_data:
```

Rode:

```bash
docker-compose up -d
```

---

## 🧪 Testar conexão no .NET

Use a seguinte **connection string**:

```json
{
  "ConnectionStrings": {
    "SportsStoreConnection": "Server=localhost,1433;Database=SportsStore;User Id=sa;Password=@Sql2022;TrustServerCertificate=True"
  }
}
```

---

## ⚙️ 3. Scripts prontos

### 📜 `start-sqlserver.ps1` (PowerShell)

```powershell
docker volume create sqlserver2022volume

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" `
  -p 1433:1433 --name sqlserver2022dev `
  -v sqlserver2022volume:/var/opt/mssql `
  -d mcr.microsoft.com/mssql/server:2022-latest
```

### 📜 `start-sqlserver.cmd` (Windows CMD)

```cmd
docker volume create sqlserver2022volume

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2022" ^
  -p 1433:1433 --name sqlserver2022dev ^
  -v sqlserver2022volume:/var/opt/mssql ^
  -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## 🔁 Comandos úteis

| Ação                        | Comando                                            |
|-----------------------------|----------------------------------------------------|
| Listar volumes              | `docker volume ls`                                 |
| Remover volume              | `docker volume rm sqlserver2022volume`            |
| Parar container             | `docker stop sqlserver2022dev`                    |
| Remover container           | `docker rm sqlserver2022dev`                      |
| Subir com compose           | `docker-compose up -d`                            |
| Parar compose               | `docker-compose down`                             |

---

## 📌 Observações finais

- O volume garante que os dados não sejam perdidos entre reinicializações.
- O container escutará em `localhost:1433`, pronto para uso com Entity Framework ou SQL Server Management Studio.
- Recomenda-se manter a senha segura e evitar `latest` em produção.

