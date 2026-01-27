readme.

Documentação para rodar o projeto do zero (pra apresentar saporra).

O QUE PRECISA ESTAR INSTALADO NO PC
Antes de abrir o terminal, você precisa ter esses softwares instalados:

.NET SDK 8.0(https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (Sem isso o C# não roda). pode ser o 10.0 tbm, mas é mais buxa.
VSCode(https://code.visualstudio.com/) (Para editar o código).
XAMPP(https://www.apachefriends.org/pt_br/index.html) (Para o banco de dados MySQL).

COMANDOS DO TERMINAL (sem isso fode tudo)

Se você acabou de baixar o projeto e ele não roda, é porque faltam as bibliotecas (burrão), mas rlx q eu te ajudo. abre o terminal na pasta do Backend (onde tem o arquivo ".csproj") e siga a ordem:

1. Baixar todas as dependências
Esse comando lê o arquivo de projeto e baixa tudo que falta automaticamente:

"dotnet restore"

poe essa porra no terminal q fica tão delicioso q você até chora.

2. (Opcional) Instalar bibliotecas manualmente
se der MUITA BOSTA, bota essa porra ai tbm no terminal q dá bom.

dotnet add package MySql.Data
(Esse é o cara que faz o C# conversar com o XAMPP).

COMO RODAR O PROJETO
1. Ligar o Backend (C#)
No terminal, dentro da pasta do C#, rode exatamente este comando para garantir a porta certa:

dotnet run --urls="http://localhost:5062"
Se aparecer "Now listening on: http://localhost:5062" tá mec.

2. Abrir o Frontend (Site)
Vá na pasta onde está o index.html.

Recomendado: Clique com botão direito no index.html e escolha "Open with Live Server".

QQ É LIVE SERVER?
É uma expansão do VS Code. pra baixar vai em expansões, coloca "Live Server" e baixa o primeiro q tiver um ícone roxinho

DEU RUIM?
Erro "Connection Refused": O MySQL (XAMPP) tá desligado ou a senha no appsettings.json tá errada.

Erro "CORS": O backend não tá rodando ou tá na porta errada. Confere se usou o --urls acima.

Nada clica no 3D: Verifique se o backend está rodando na porta 5062.


Resumo pra você não esquecer:
O comando delicia que baixar as paradas do terminal é o:
"dotnet restore"

Se ele não funcionar, força a instalação do MySQL:
"dotnet add package MySql.Data"

se continuar dando erro, baixa esse aq tbm:
"dotnet add package Pomelo.EntityFrameworkCore.MySql"

POR FAVOR N COPIA ESSAS MERDAS COM ASPAS.