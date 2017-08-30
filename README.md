# MvcLanches
1st version<br /><br />
•	IDE: Visual Studio 2015 Comunity <br />
•	Server-side:  .Net Framework 4.5<br />
•	Arquitetura: MVC (MVC 5 com Razor)<br />
•	ORM: Entity Framework 6<br />
o	Context<br />
o	Code-First<br />
•	Client-side: Bootstrap<br />
o	Html 5<br />
o	Css 3<br />
o	jQuery 1.10.2<br />
•	Persistence: MS SQL Local DB<br /><br /><br />

<b>Instruções para Executar</b><br />
•	Para abrir a Solução:<br />
&nbsp;o	Visual Studio 201x (com suporte a versão 4.5 do Framework)<br />
&nbsp;o	NuGet Package habilitado (baixará os packages adicionais automaticamente na primeira execução do projeto, porém é necessário que o NuGet esteja instalado e habilitado<br />
&nbsp;o	Fazer download do Código Fonte no GitHub pelo seguinte endereço: https://github.com/rafabaptista/MvcLanches<br />
&nbsp;o	Abrir projeto pelo arquivo VLO.sln , que está logo na pasta raiz do projeto.<br />
•	Para rodar o código Fonte é necessário:<br />
&nbsp;o	Clicar com o botão direito do mouse sobre o projeto VLO e então clicar na opção  “Set as StartUp Project”<br />
&nbsp;o	Executar o projeto apertando a tecla ‘F5’ ou clicar no menu ‘Debug’ -> ‘Start Debugging’.<br />
&nbsp;&nbsp;	Ao executar o projeto, será aberto um IIS Express pelo Visual Studio. É possível rodar a aplicação, então, a partir do Local Host (http://localhost:50303/)<br />
&nbsp;&nbsp;	Caso seja necessária a publicação do projeto (deploy), será necessário a criação da estrutura de base (sugestão: SQL Server) de acordo com a base local (baixada juntamente com o projeto pelo repositório: VLODBP.mdf).<br />
•	Necessário configurar Web.Config com com a nova ConnectionString.<br />
•	Necessário criação dos dados (podem ser feitos utilizando as telas do sistema, quando executado após ser configurado)<br />
&nbsp;o	Acompanhar Tutorial de uso do aplicativo abaixo para checar a aplicação e suas funcionalidades<br />
•	Para rodar os Testes é necessário:<br />
&nbsp;o	Clicar com o botão direito do mouse sobre o projeto VLO.Tests e então clicar na opção  “Set as StartUp Project”<br />
&nbsp;o	Dentro da pasta “Controllers” existe a classe “CardapioControllerTests.cs”, abra-a<br />
&nbsp;o	Existem 2 blocos de testes que executam o mesmo teste:<br />
&nbsp;&nbsp;	Um deles usa chamadas de objetos instanciados manualmente (com valores declarados no topo da classe)<br />
&nbsp;&nbsp;&nbsp;•	Para alterar os valores, basta checar o region “Instanciacao de objetos manuais” e alterar os valores.<br />
&nbsp;&nbsp;	O outro bloco com valores puxados do banco de Dados.<br /><br />
•	Para executar os testes utilizando o Banco de Dados, é necessário alterar o App.config do projeto de testes<br />
&nbsp;o	Na seção de “ConnectionStrings”, altere o valor da chave “ContextDB“, opção “AttachDbFilename“. Basta colocar o caminho para o arquivo “VLODBP.mdf“ que foi baixado juntamente com o restante do Projeto pelo Repositório. Ex: “C:\Projects\Web\MvcLanches-master\VLO\App_Data\ VLODBP.mdf”<br />
&nbsp;o	Dentro do método de teste, clique com o botão direito do Mouse e clique na opção “Run Tests”.<br />
&nbsp;o	Os testes foram montados para mostrar os casos de “Sucesso” para cada lanche e promoção do Cardápio.<br />
