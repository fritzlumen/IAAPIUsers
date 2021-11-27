Exercício IA

Modelagem

Usuario
	Nome
	Idade
	Senha

Project IAAPIUsers

Version .NET5.0

Package 

Microsoft.EntityFramworkCore.SqlLite -v 5.0.12 //Para Sql Lite
ou
Microsoft.EntityFramworkCore.SqlServer -v 5.0.12 //Para Sql Server

Microsoft.EntityFramworkCore.Design -v 5.0.12

Banco de Dados SQL Lite

1 - configurar a conexão no AppDbContext.cs colocando no optionsBuilder.UseSqlLite("DataSource=app.db;Cache=Shared");
2 - executar "dotnet ef migrations add InitialCreation"
3 - executar "dotnet ef database update"


Banco de Dados SQL SERVER

1 - Criar um banco local no sql server e configurar a conexão no AppDbContext.cs
2 - executar "dotnet ef migrations add InitialCreation"
3 - executar "dotnet ef database update"


