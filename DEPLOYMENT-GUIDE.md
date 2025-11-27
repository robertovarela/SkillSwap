# 🚀 Guia de Deploy - Aplicar Migrations em Produção

## 📌 Visão Geral

Este guia explica como aplicar migrations do Entity Framework Core em ambiente de produção de forma segura.

---

## 🎯 Método 1: Script SQL (RECOMENDADO)

### ✅ Vantagens:
- ✅ Controle total sobre o que será executado
- ✅ Possibilidade de revisão antes da execução
- ✅ Idempotente (pode executar múltiplas vezes sem problemas)
- ✅ Pode ser executado por DBA
- ✅ Pode ser versionado no controle de versão

### 📝 Passo a Passo:

#### 1. Gerar o Script SQL

```bash
# Gera script de TODAS as migrations
dotnet ef migrations script --idempotent --output migration.sql --project RDS.Persistence

# Gera script de uma migration específica até a mais recente
dotnet ef migrations script <MigrationAnterior> --idempotent --output migration.sql --project RDS.Persistence

# Gera script entre duas migrations específicas
dotnet ef migrations script <MigrationInicio> <MigrationFim> --idempotent --output migration.sql --project RDS.Persistence
```

**Flags importantes:**
- `--idempotent`: Gera script que verifica se já foi executado (seguro para executar múltiplas vezes)
- `--output`: Define o arquivo de saída
- `--project`: Especifica o projeto onde estão as migrations

#### 2. Revisar o Script

Abra o arquivo `migration.sql` e revise:
- ✅ Verifique se todas as alterações estão corretas
- ✅ Confirme que não há DROP de dados importantes
- ✅ Verifique índices e constraints

#### 3. Executar no Banco de Produção

**Opção A - Via SQL Server Management Studio (SSMS):**
1. Conecte ao servidor de produção
2. Abra o arquivo `migration.sql`
3. Execute em uma transação (recomendado):
   ```sql
   BEGIN TRANSACTION;
   
   -- Cole o conteúdo do migration.sql aqui
   
   -- Revise os resultados
   COMMIT; -- ou ROLLBACK se houver problemas
   ```

**Opção B - Via sqlcmd (linha de comando):**
```bash
sqlcmd -S <servidor> -d <banco> -U <usuario> -P <senha> -i migration.sql
```

**Opção C - Via Azure Data Studio:**
1. Conecte ao servidor
2. Abra o arquivo `migration.sql`
3. Execute o script

---

## 🎯 Método 2: Aplicar via Comando (Para ambientes controlados)

### ⚠️ Use APENAS se:
- Você tem acesso direto ao servidor de produção
- O ambiente permite conexão da sua máquina
- É um ambiente de menor criticidade

### 📝 Passo a Passo:

#### 1. Configurar Connection String de Produção

No `appsettings.Production.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<SERVIDOR_PROD>;Database=<DB_PROD>;User Id=<USER>;Password=<SENHA>;TrustServerCertificate=true;"
  }
}
```

#### 2. Aplicar a Migration

```bash
# Aplicar todas as migrations pendentes
dotnet ef database update --project RDS.Persistence --startup-project RDS.Server --configuration Production

# Aplicar até uma migration específica
dotnet ef database update <NomeDaMigration> --project RDS.Persistence --startup-project RDS.Server --configuration Production
```

---

## 🎯 Método 3: Aplicar Automaticamente na Inicialização (NÃO RECOMENDADO)

### ⚠️ NÃO use em produção crítica!

Este método aplica migrations automaticamente quando a aplicação inicia.

### Implementação:

No `Program.cs`:
```csharp
var app = builder.Build();

// Aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // CUIDADO: Isso aplica migrations automaticamente!
}
```

### ❌ Desvantagens:
- Sem controle sobre quando executa
- Pode causar downtime inesperado
- Problemas em ambiente com múltiplas instâncias
- Difícil de reverter em caso de erro

---

## 📋 Checklist de Deploy

### Antes de Aplicar:

- [ ] Fazer backup completo do banco de dados
- [ ] Revisar o script SQL gerado
- [ ] Testar o script em ambiente de staging/homologação
- [ ] Verificar se há dados que serão perdidos
- [ ] Planejar janela de manutenção (se necessário)
- [ ] Notificar equipe e usuários (se necessário)
- [ ] Ter plano de rollback pronto

### Durante a Aplicação:

- [ ] Colocar aplicação em modo manutenção (se necessário)
- [ ] Executar o script em uma transação
- [ ] Monitorar logs de erro
- [ ] Verificar tempo de execução

### Após Aplicação:

- [ ] Verificar se todas as tabelas foram criadas/alteradas
- [ ] Testar funcionalidades críticas da aplicação
- [ ] Verificar integridade dos dados
- [ ] Monitorar performance
- [ ] Remover modo manutenção
- [ ] Documentar o que foi feito

---

## 🔄 Como Reverter uma Migration

### Opção 1: Via Comando
```bash
# Reverter para migration anterior
dotnet ef database update <NomeMigrationAnterior> --project RDS.Persistence

# Reverter TODAS as migrations (CUIDADO!)
dotnet ef database update 0 --project RDS.Persistence
```

### Opção 2: Via Script SQL
```bash
# Gerar script de downgrade
dotnet ef migrations script <MigrationAtual> <MigrationAnterior> --output rollback.sql --project RDS.Persistence
```

---

## 📊 Boas Práticas

### ✅ DO (Faça):

1. **Sempre gere scripts SQL para produção**
2. **Sempre faça backup antes de aplicar**
3. **Teste em ambiente de staging primeiro**
4. **Use migrations idempotentes**
5. **Versione os scripts SQL no Git**
6. **Documente cada migration**
7. **Use nomes descritivos para migrations**
8. **Revise o código SQL gerado**

### ❌ DON'T (Não Faça):

1. **Nunca aplique migrations diretamente sem revisar**
2. **Nunca execute em produção sem testar antes**
3. **Nunca faça ALTER/DROP sem backup**
4. **Nunca use auto-migration em produção crítica**
5. **Nunca modifique migrations já aplicadas**
6. **Nunca ignore warnings do EF Core**

---

## 🛠️ Comandos Úteis

```bash
# Listar todas as migrations
dotnet ef migrations list --project RDS.Persistence

# Ver qual migration está aplicada no banco
dotnet ef migrations list --project RDS.Persistence --connection "<connection_string>"

# Remover última migration (se ainda não foi aplicada)
dotnet ef migrations remove --project RDS.Persistence

# Adicionar nova migration
dotnet ef migrations add <NomeDaMigration> --project RDS.Persistence

# Gerar script SQL de uma migration específica
dotnet ef migrations script <MigrationAnterior> <MigrationAtual> --project RDS.Persistence
```

---

## 📝 Exemplo de Workflow Completo

### Desenvolvimento:
```bash
# 1. Criar migration
dotnet ef migrations add AddUserProfile --project RDS.Persistence

# 2. Aplicar localmente
dotnet ef database update --project RDS.Persistence

# 3. Testar a aplicação
dotnet run --project RDS.Server
```

### Deploy para Produção:
```bash
# 1. Gerar script SQL
dotnet ef migrations script --idempotent --output migration-v1.0.sql --project RDS.Persistence

# 2. Commitar script no Git
git add migration-v1.0.sql
git commit -m "Migration v1.0 - Add UserProfile"
git push

# 3. Em produção: Fazer backup
# Via SSMS ou comando:
# BACKUP DATABASE [SuaDB] TO DISK = 'C:\Backup\SuaDB.bak'

# 4. Executar script SQL no banco de produção
# Via SSMS, Azure Data Studio ou sqlcmd

# 5. Verificar resultado
# SELECT * FROM __EFMigrationsHistory

# 6. Testar aplicação
```

---

## 🆘 Troubleshooting

### Problema: "A network-related or instance-specific error"
**Solução**: Verificar connection string e firewall

### Problema: "The migration has already been applied"
**Solução**: Use `--idempotent` ao gerar o script

### Problema: "Foreign key constraint"
**Solução**: Verificar ordem das operações no script, pode precisar desabilitar constraints temporariamente

### Problema: "Column names in each table must be unique"
**Solução**: Verificar se não há colunas duplicadas no modelo

---

## 📚 Referências

- [EF Core Migrations Overview](https://docs.microsoft.com/ef/core/managing-schemas/migrations/)
- [Applying Migrations](https://docs.microsoft.com/ef/core/managing-schemas/migrations/applying)
- [SQL Server Backup and Restore](https://docs.microsoft.com/sql/relational-databases/backup-restore/)

---

## 📞 Suporte

Para dúvidas ou problemas, consulte:
- Documentação oficial do EF Core
- Log de erros da aplicação
- DBA responsável pelo banco de dados

