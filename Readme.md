# .NET Desafio Técnico - Backend

## Introdução

Este repositório representa minha solução do desafio técnico apresentado pelo no respositório: https://gitlab.com/gabriel.militello1/desafio-tecnico-backend.

O desafio proposto teve limitação de tempo de 3 dias, logo esta solução, apesar de implementar o desafio, ainda falta melhorias que o tempo impediu de ser implementadas:

1. Falta a implementação da validação dos dados de entrada, o planejamento era implementar o FluentValidation.
2. Falta a implementação do Result pattern
3. Falta testes de integração
4. Falta a criação do arquivo yaml do docker compose para o uso com outros tipoes de banco de dados.
5. Falta atualização do arquivo .http para testes.
6. Falta a implementação do output cache.

## Estrutura do projeto

- **`/Cards`**: Contém a implementação dos Handlers do MediatR
- **`/Configuration`**: Contém a geração dos dados de configuração do Swagger.
- **`/Data`**: Contém o contexto de persistência do banco de dados(**)
- **`/EndpointsDefinition`**: Contém as implementações de cada endpoint.
- **`/Extensions`**: Contém as configurações da applicação, extendendo WebApplication e WebapplicationBuilder.
- **`/Interfaces`**: Contém as interfaces utilizadas na lógica de negócio.
- **`/Migrations`**: Contém as migrações do Entity Framework para gerenciamento das mudanças do esquema das tabelas do banco de dados. (**)
- **`/Models`**: Contém os modelos de classes usados no projeto.
- **`/Services`**: Contém todos os processos da lógica de negócio e acesso aos dados.

(**) Apesar da aplicação ter o suporte para rodar com banco de dados: sqlite e MSSQL Server, como não houve tempo para criar o arquivo do docker, sem alterar nenhuma configuração a aplicação irá rodar com o banco de dados em memória.

## Configurações necessárias para executar

É necessário adicionar algumas configurações no appsettings.json.

É necessário adicionar uma chave para criptografia da geração do token de no minimo 128 bits, exemplo 2f598abb2b801a9ab5cc41856607512a .
```json
"jwt": {
    "key": ""
  },
```
Também é necessário adicionar o login e senha do usuário permitido para criação do token. Exemplo: { "login":"letscode", "senha":"lets@123"}

```json
"User": {
    "Login": "",
    "Senha": ""
},
```

Então só executar pelo método favorito.