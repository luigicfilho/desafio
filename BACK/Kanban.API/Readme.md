# .NET Desafio T�cnico - Backend

## Introdu��o

Este reposit�rio representa minha solu��o do desafio t�cnico apresentado pelo no resposit�rio: https://gitlab.com/gabriel.militello1/desafio-tecnico-backend.

O desafio proposto teve limita��o de tempo de 3 dias, logo esta solu��o, apesar de implementar o desafio, ainda falta melhorias que o tempo impediu de ser implementadas:

1. Falta a implementa��o da valida��o dos dados de entrada, o planejamento era implementar o FluentValidation.
2. Falta a implementa��o do Result pattern
3. Falta testes de integra��o
4. Falta a cria��o do arquivo yaml do docker compose para o uso com outros tipoes de banco de dados.
5. Falta atualiza��o do arquivo .http para testes.
6. Falta a implementa��o do output cache.

## Estrutura do projeto

- **`/Cards`**: Cont�m a implementa��o dos Handlers do MediatR
- **`/Configuration`**: Cont�m a gera��o dos dados de configura��o do Swagger.
- **`/Data`**: Cont�m o contexto de persist�ncia do banco de dados(**)
- **`/EndpointsDefinition`**: Cont�m as implementa��es de cada endpoint.
- **`/Extensions`**: Cont�m as configura��es da applica��o, extendendo WebApplication e WebapplicationBuilder.
- **`/Interfaces`**: Cont�m as interfaces utilizadas na l�gica de neg�cio.
- **`/Migrations`**: Cont�m as migra��es do Entity Framework para gerenciamento das mudan�as do esquema das tabelas do banco de dados. (**)
- **`/Models`**: Cont�m os modelos de classes usados no projeto.
- **`/Services`**: Cont�m todos os processos da l�gica de neg�cio e acesso aos dados.

(**) Apesar da aplica��o ter o suporte para rodar com banco de dados: sqlite e MSSQL Server, como n�o houve tempo para criar o arquivo do docker, sem alterar nenhuma configura��o a aplica��o ir� rodar com o banco de dados em mem�ria.

## Configura��es necess�rias para executar

� necess�rio adicionar algumas configura��es no appsettings.json.

� necess�rio adicionar uma chave para criptografia da gera��o do token de no minimo 128 bits, exemplo 2f598abb2b801a9ab5cc41856607512a .
```json
"jwt": {
    "key": ""
  },
```
Tamb�m � necess�rio adicionar o login e senha do usu�rio permitido para cria��o do token. Exemplo: { "login":"letscode", "senha":"lets@123"}

```json
"User": {
    "Login": "",
    "Senha": ""
},
```

Ent�o s� executar pelo m�todo favorito.