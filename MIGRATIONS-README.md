# 🚀 Deploy de Migrations - Guia Rápido

## ⚡ Comandos Essenciais

### 1️⃣ Gerar Script SQL (RECOMENDADO para Produção)
```bash
dotnet ef migrations script --idempotent --output migration.sql --project RDS.Persistence
```

### 2️⃣ Aplicar via Comando (Desenvolvimento/Staging)
```bash
dotnet ef database update --project RDS.Persistence --startup-project RDS.Server
```

### 3️⃣ Usar Script PowerShell Automatizado
```powershell
# Apenas gerar script
.\deploy-migration.ps1 -Environment "Production" -GenerateScriptOnly

# Gerar script e criar backup
.\deploy-migration.ps1 -Environment "Production" -GenerateScriptOnly -CreateBackup

# Aplicar diretamente (cuidado!)
.\deploy-migration.ps1 -Environment "Staging" -ConnectionString "Server=...;Database=..."
```

---

## 📋 Checklist Rápido

### Antes de Aplicar:
- [ ] ✅ Fazer backup do banco
- [ ] ✅ Testar em ambiente de staging
- [ ] ✅ Revisar script SQL gerado
- [ ] ✅ Planejar janela de manutenção (se necessário)

### Aplicar em Produção:
1. Gerar script SQL
2. Revisar o script
3. Fazer backup do banco
4. Executar script no SSMS/Azure Data Studio
5. Verificar se funcionou
6. Testar aplicação

---

## 🆘 Comandos de Emergência

### Reverter para Migration Anterior
```bash
dotnet ef database update <NomeMigrationAnterior> --project RDS.Persistence
```

### Gerar Script de Rollback
```bash
dotnet ef migrations script <MigrationAtual> <MigrationAnterior> --output rollback.sql --project RDS.Persistence
```

### Listar Migrations
```bash
dotnet ef migrations list --project RDS.Persistence
```

### Ver Migrations Aplicadas no Banco
```sql
SELECT * FROM __EFMigrationsHistory ORDER BY MigrationId DESC
```

---

## 📚 Documentação Completa

Para guia completo, veja: [DEPLOYMENT-GUIDE.md](./DEPLOYMENT-GUIDE.md)

---

## ⚠️ IMPORTANTE

**NUNCA** aplique migrations diretamente em produção sem:
1. Revisar o script SQL
2. Fazer backup
3. Testar em staging

**Use sempre** scripts SQL idempotentes em produção!

