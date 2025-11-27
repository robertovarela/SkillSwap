# 🔌 Connection Strings - Exemplos

## 📝 Formato Geral

### SQL Server (Autenticação Windows)
```json
"Server=localhost;Database=SkillSwapDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

### SQL Server (Autenticação SQL Server)
```json
"Server=localhost;Database=SkillSwapDB;User Id=sa;Password=SuaSenha123;TrustServerCertificate=True;"
```

### SQL Server (Porta Customizada)
```json
"Server=localhost,1433;Database=SkillSwapDB;User Id=sa;Password=SuaSenha123;TrustServerCertificate=True;"
```

---

## 🌐 Ambientes

### Desenvolvimento (appsettings.Development.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SkillSwapDB_Dev;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Staging (appsettings.Staging.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=staging-server.empresa.com;Database=SkillSwapDB_Staging;User Id=app_user;Password=${DB_PASSWORD};TrustServerCertificate=True;"
  }
}
```

### Produção (appsettings.Production.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server.empresa.com;Database=SkillSwapDB;User Id=app_user;Password=${DB_PASSWORD};Encrypt=True;TrustServerCertificate=False;"
  }
}
```

---

## ☁️ Azure SQL Database

### Connection String Básica
```json
"Server=tcp:seu-servidor.database.windows.net,1433;Database=SkillSwapDB;User Id=seu-usuario@seu-servidor;Password=SuaSenha;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

### Com Managed Identity
```json
"Server=tcp:seu-servidor.database.windows.net,1433;Database=SkillSwapDB;Authentication=Active Directory Managed Identity;Encrypt=True;"
```

---

## 🐳 Docker / Container

### SQL Server em Docker
```json
"Server=localhost,1433;Database=SkillSwapDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
```

### Docker Compose (usando nome do service)
```json
"Server=sqlserver,1433;Database=SkillSwapDB;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
```

---

## 🔒 Segurança - Variáveis de Ambiente

### Windows
```powershell
# Definir variável de ambiente
$env:DB_CONNECTION_STRING = "Server=...;Database=...;User Id=...;Password=..."

# Usar no código
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
```

### Linux/Mac
```bash
# Definir variável de ambiente
export DB_CONNECTION_STRING="Server=...;Database=...;User Id=...;Password=..."

# Ou em .bashrc/.zshrc
echo 'export DB_CONNECTION_STRING="..."' >> ~/.bashrc
```

### .NET User Secrets (Desenvolvimento)
```bash
# Inicializar secrets
dotnet user-secrets init --project RDS.Server

# Adicionar connection string
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=SkillSwapDB;Trusted_Connection=True;" --project RDS.Server
```

### Azure App Service (Application Settings)
```json
{
  "name": "ConnectionStrings__DefaultConnection",
  "value": "Server=tcp:...;Database=...;User Id=...;Password=...;",
  "slotSetting": false
}
```

---

## 🔑 Parâmetros Importantes

### Essenciais
- `Server` ou `Data Source`: Endereço do servidor
- `Database` ou `Initial Catalog`: Nome do banco de dados
- `User Id` ou `UID`: Usuário
- `Password` ou `PWD`: Senha
- `Trusted_Connection`: Usar autenticação Windows (True/False)

### Segurança
- `Encrypt`: Criptografar conexão (True/False)
- `TrustServerCertificate`: Confiar no certificado do servidor
- `MultipleActiveResultSets` (MARS): Permitir múltiplos resultados ativos

### Performance
- `Connection Timeout`: Tempo limite de conexão (segundos)
- `Max Pool Size`: Tamanho máximo do pool de conexões
- `Min Pool Size`: Tamanho mínimo do pool de conexões
- `Pooling`: Habilitar pooling (True/False)

### Exemplo Completo
```json
"Server=prod-server;Database=SkillSwapDB;User Id=app_user;Password=Senha123;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=True;Connection Timeout=30;Max Pool Size=100;Min Pool Size=5;Pooling=True;"
```

---

## 🧪 Testar Connection String

### Via PowerShell
```powershell
# Script para testar conexão
$connectionString = "Server=localhost;Database=SkillSwapDB;Trusted_Connection=True;"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)

try {
    $connection.Open()
    Write-Host "✅ Conexão estabelecida com sucesso!" -ForegroundColor Green
    $connection.Close()
}
catch {
    Write-Host "❌ Erro ao conectar: $_" -ForegroundColor Red
}
```

### Via C# (Program.cs)
```csharp
// Testar conexão ao iniciar
using var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
try
{
    await connection.OpenAsync();
    Console.WriteLine("✅ Conexão estabelecida com sucesso!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Erro ao conectar: {ex.Message}");
    throw;
}
```

---

## ⚠️ IMPORTANTE - Segurança

### ❌ NUNCA faça:
- Commitar senhas no Git
- Usar senhas fracas em produção
- Usar `sa` em produção
- Desabilitar encriptação em produção sem necessidade
- Compartilhar connection strings por email/chat

### ✅ SEMPRE faça:
- Use variáveis de ambiente para senhas
- Use User Secrets para desenvolvimento
- Use Azure Key Vault ou similar para produção
- Crie usuários específicos com permissões limitadas
- Habilite encriptação em produção
- Use senhas fortes e únicas
- Rotacione senhas periodicamente

---

## 📚 Referências

- [Connection Strings - Microsoft Docs](https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-connection-strings)
- [Azure SQL Connection Strings](https://docs.microsoft.com/azure/azure-sql/database/connect-query-dotnet-core)
- [Safe Storage of App Secrets](https://docs.microsoft.com/aspnet/core/security/app-secrets)

