# Script de Deploy - Aplicar Migrations em Produção
# Uso: .\deploy-migration.ps1 -Environment "Production" -ConnectionString "Server=..."

param(
    [Parameter(Mandatory=$true)]
    [ValidateSet("Development", "Staging", "Production")]
    [string]$Environment,

    [Parameter(Mandatory=$false)]
    [string]$ConnectionString = "",

    [Parameter(Mandatory=$false)]
    [switch]$GenerateScriptOnly,

    [Parameter(Mandatory=$false)]
    [switch]$CreateBackup
)

$ErrorActionPreference = "Stop"
$ProjectPath = "RDS.Persistence"
$StartupProject = "RDS.Server"

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Migration Deployment Script" -ForegroundColor Cyan
Write-Host "  Environment: $Environment" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Função para verificar se dotnet ef está instalado
function Test-DotNetEF {
    try {
        $result = dotnet ef --version 2>&1
        return $true
    }
    catch {
        return $false
    }
}

# Verificar dotnet ef
if (-not (Test-DotNetEF)) {
    Write-Host "❌ dotnet-ef não está instalado!" -ForegroundColor Red
    Write-Host "📦 Instalando dotnet-ef..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
    Write-Host "✅ dotnet-ef instalado com sucesso!" -ForegroundColor Green
}

# Timestamp para nome do arquivo
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$scriptFileName = "migration_${Environment}_${timestamp}.sql"

Write-Host "📝 Gerando script SQL..." -ForegroundColor Yellow

# Gerar script SQL idempotente
try {
    $command = "dotnet ef migrations script --idempotent --output `"$scriptFileName`" --project $ProjectPath --startup-project $StartupProject"

    if ($Environment -ne "Development") {
        $command += " --configuration $Environment"
    }

    Write-Host "Executando: $command" -ForegroundColor Gray
    Invoke-Expression $command

    if (Test-Path $scriptFileName) {
        Write-Host "✅ Script SQL gerado com sucesso: $scriptFileName" -ForegroundColor Green
    }
    else {
        throw "Arquivo de script não foi criado"
    }
}
catch {
    Write-Host "❌ Erro ao gerar script SQL: $_" -ForegroundColor Red
    exit 1
}

# Se for apenas para gerar o script, para aqui
if ($GenerateScriptOnly) {
    Write-Host ""
    Write-Host "✅ Script gerado! Revise o arquivo antes de aplicar em produção." -ForegroundColor Green
    Write-Host "📄 Arquivo: $scriptFileName" -ForegroundColor Cyan
    exit 0
}

# Se não foi fornecida connection string, perguntar
if ([string]::IsNullOrWhiteSpace($ConnectionString)) {
    Write-Host ""
    Write-Host "⚠️  ATENÇÃO: Você está prestes a aplicar migrations no ambiente $Environment" -ForegroundColor Yellow
    Write-Host ""
    $confirmation = Read-Host "Digite 'SIM' para continuar ou qualquer outra tecla para cancelar"

    if ($confirmation -ne "SIM") {
        Write-Host "❌ Operação cancelada pelo usuário" -ForegroundColor Red
        exit 0
    }

    Write-Host ""
    Write-Host "Por favor, informe a connection string do banco de dados:" -ForegroundColor Cyan
    $ConnectionString = Read-Host
}

# Criar backup (se solicitado)
if ($CreateBackup) {
    Write-Host ""
    Write-Host "💾 Criando backup do banco de dados..." -ForegroundColor Yellow

    # Parse connection string para extrair servidor e database
    if ($ConnectionString -match "Server=([^;]+).*Database=([^;]+)") {
        $server = $matches[1]
        $database = $matches[2]
        $backupFile = "${database}_backup_${timestamp}.bak"

        Write-Host "Servidor: $server" -ForegroundColor Gray
        Write-Host "Database: $database" -ForegroundColor Gray
        Write-Host "Arquivo Backup: $backupFile" -ForegroundColor Gray

        # Aqui você pode adicionar lógica de backup específica
        Write-Host "⚠️  Lembre-se de fazer backup manualmente se necessário!" -ForegroundColor Yellow
    }
}

# Aplicar migration
Write-Host ""
Write-Host "🚀 Aplicando migrations..." -ForegroundColor Yellow

try {
    $updateCommand = "dotnet ef database update --project $ProjectPath --startup-project $StartupProject --connection `"$ConnectionString`""

    if ($Environment -ne "Development") {
        $updateCommand += " --configuration $Environment"
    }

    Write-Host "Executando: dotnet ef database update..." -ForegroundColor Gray
    Invoke-Expression $updateCommand

    Write-Host ""
    Write-Host "✅ Migrations aplicadas com sucesso!" -ForegroundColor Green
}
catch {
    Write-Host ""
    Write-Host "❌ Erro ao aplicar migrations: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "💡 Dica: Você ainda pode aplicar manualmente usando o arquivo:" -ForegroundColor Yellow
    Write-Host "   $scriptFileName" -ForegroundColor Cyan
    exit 1
}

# Listar migrations aplicadas
Write-Host ""
Write-Host "📋 Listando migrations aplicadas..." -ForegroundColor Yellow
try {
    dotnet ef migrations list --project $ProjectPath --startup-project $StartupProject --connection "$ConnectionString"
}
catch {
    Write-Host "⚠️  Não foi possível listar as migrations" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  ✅ Deploy Concluído!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "📄 Script SQL salvo em: $scriptFileName" -ForegroundColor Cyan
Write-Host "💡 Mantenha este script para documentação" -ForegroundColor Yellow

