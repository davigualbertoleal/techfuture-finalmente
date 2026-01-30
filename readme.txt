Documentação para rodar o projeto do zero

Este documento descreve os passos necessários para configurar e executar o projeto em um ambiente local.

==================================================

O QUE PRECISA ESTAR INSTALADO NO PC

Antes de abrir o terminal, certifique-se de que os seguintes softwares estejam instalados:

* .NET SDK 8.0
  Necessário para executar aplicações em C#.
  Link: [https://dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
  Versões mais recentes (como 10.0) também funcionam, porém não são recomendadas.

* Visual Studio Code (VS Code)
  Utilizado para editar o código-fonte.
  Link: [https://code.visualstudio.com/](https://code.visualstudio.com/)

* XAMPP
  Utilizado para o banco de dados MySQL.
  Link: [https://www.apachefriends.org/pt_br/index.html](https://www.apachefriends.org/pt_br/index.html)

==================================================

COMANDOS DO TERMINAL (OBRIGATÓRIOS)

Se você acabou de baixar o projeto e ele não roda, é porque as bibliotecas ainda não estão instaladas.

Abra o terminal na pasta do Backend, onde está o arquivo .csproj.
Caso necessário, navegue pelas pastas com os comandos abaixo, nesta ordem:

cd Backend
cd TechFutureAPI

==================================================

1. Baixar todas as dependências

Este comando lê o arquivo do projeto e baixa automaticamente tudo o que for necessário:

dotnet restore

==================================================

2. Instalar bibliotecas manualmente (opcional)

Utilize este passo apenas se ocorrerem erros durante o restore.

Instalação do driver MySQL para C#:

dotnet add package MySql.Data

Se o erro continuar, instale também:

dotnet add package Pomelo.EntityFrameworkCore.MySql

==================================================

COMO RODAR O PROJETO

1. Ligar o Backend (C#)

No terminal, dentro da pasta do Backend, execute exatamente o comando abaixo para garantir a porta correta:

dotnet run --urls=[http://localhost:5062](http://localhost:5062)

Se aparecer a mensagem:

Now listening on: [http://localhost:5062](http://localhost:5062)

o backend está funcionando corretamente.

---

2. Abrir o Frontend (Site)

Vá até a pasta onde está o arquivo index.html.

Recomendado:
Clique com o botão direito no index.html e selecione “Open with Live Server”.

---

O QUE É LIVE SERVER?

Live Server é uma extensão do Visual Studio Code que cria um servidor local para arquivos HTML.

Para instalar:

* Abra a aba Extensões no VS Code
* Pesquise por “Live Server”
* Instale a extensão com ícone roxo

==================================================

DEU PROBLEMA?

* Erro “Connection Refused”
  Verifique se o MySQL está ativo no XAMPP e se a senha no arquivo appsettings.json está correta.

* Erro de CORS
  Verifique se o backend está rodando e se a porta utilizada é a 5062.

* Nada funciona no 3D
  Confirme se o backend está ativo na porta 5062.

==================================================

RESUMO RÁPIDO

Comando principal para baixar dependências:

dotnet restore

Se não funcionar, instale manualmente:

dotnet add package MySql.Data

Se ainda houver erro:

dotnet add package Pomelo.EntityFrameworkCore.MySql

OBSERVAÇÃO IMPORTANTE:
Não copie os comandos com aspas.
