# SkillSwap

SkillSwap é uma plataforma de troca de habilidades desenvolvida com .NET 9 e Blazor. A plataforma permite que os usuários ofereçam suas habilidades e encontrem outras pessoas com quem possam trocar serviços.

## Tecnologias Utilizadas

*   **.NET 9**: A versão mais recente do framework de desenvolvimento da Microsoft.
*   **Blazor Server**: Para criar uma interface de usuário web rica e interativa com C#.
*   **Entity Framework Core**: Para mapeamento objeto-relacional (ORM) e acesso a dados.
*   **ASP.NET Core Identity**: Para gerenciamento de autenticação e identidade de usuários.
*   **MudBlazor**: Uma biblioteca de componentes de interface de usuário para Blazor.
*   **FluentValidation**: Para validação de modelos de forma declarativa.
*   **Swagger (Swashbuckle)**: Para documentação e teste de APIs.

## Estrutura do Projeto

O projeto está organizado em várias camadas, seguindo uma arquitetura limpa:

*   `RDS.Core`: Contém as entidades de domínio e a lógica de negócios principal.
*   `RDS.Infrastructure`: Contém as implementações de serviços externos, como envio de e-mail e acesso a sistemas de arquivos.
*   `RDS.Persistence`: Contém o `DbContext` do Entity Framework Core e as configurações de banco de dados.
*   `RDS.Server`: O projeto principal do Blazor Server, que hospeda a interface do usuário e a API.

## Como Começar

### Pré-requisitos

*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   Um editor de código, como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

### Configuração

1.  Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/SkillSwap.git
    ```
2.  Navegue até o diretório do projeto:
    ```bash
    cd SkillSwap
    ```
3.  Restaure as dependências do .NET:
    ```bash
    dotnet restore
    ```
4.  Configure a string de conexão do banco de dados no arquivo `appsettings.Development.json` no projeto `RDS.Server`.

### Executando o Projeto

1.  Navegue até o diretório `RDS.Server`:
    ```bash
    cd RDS.Server
    ```
2.  Execute o aplicativo:
    ```bash
    dotnet run
    ```
3.  Abra seu navegador e acesse `https://localhost:5001` (ou o endereço especificado no console).

O banco de dados será preenchido com dados de exemplo na primeira vez que o aplicativo for executado no ambiente de desenvolvimento.
