# Sistema de Gestão de Contas Bancárias

## Descrição do Projeto

Este projeto tem como objetivo gerenciar contas bancárias para clientes, oferecendo funcionalidades essenciais como cadastro, consulta, inativação e transferência entre contas. O sistema foi desenvolvido para garantir segurança, integridade dos dados e facilidade de uso, atendendo às necessidades do gestor do sistema.

---

## Funcionalidades

### 1. Cadastro de Contas Bancárias

**Descrição:**  
Permite ao gestor cadastrar novas contas bancárias para os clientes, assegurando a integridade e as regras de negócio definidas.

**Critérios de Aceitação:**  
- Nome e documento do cliente são obrigatórios.  
- Não é permitido cadastrar múltiplas contas para o mesmo documento.  
- Contas iniciam com saldo de R$1000 como bonificação.  
- Data de abertura é registrada automaticamente no momento do cadastro.

### 2. Consulta de Contas Cadastradas

**Descrição:**  
Permite consultar contas cadastradas, com possibilidade de filtro por nome ou documento.

**Critérios de Aceitação:**  
- Listagem deve retornar: nome do cliente, documento, saldo atual, data de abertura e status da conta (ativa/inativa).  
- Filtro por nome (parcial ou completo) ou documento.

### 3. Inativação de Conta

**Descrição:**  
Permite desativar uma conta bancária a partir do número do documento do titular.

**Critérios de Aceitação:**  
- Recebe como parâmetro o documento do titular.  
- Se conta ativa, altera status para inativa.  
- Registra ação com documento, data/hora e usuário responsável.  
- Dados históricos da conta não são excluídos.

### 4. Transferência entre Contas

**Descrição:**  
Realiza transferências entre contas, garantindo que as contas estejam ativas e saldo suficiente na conta origem.

**Critérios de Aceitação:**  
- Contas de origem e destino devem estar ativas e válidas.  
- Conta de origem deve possuir saldo suficiente.  
- Débito na conta origem e crédito na conta destino com o valor da transferência.

---

## Instruções para Execução do Projeto

Para rodar este projeto, são necessários os seguintes pré-requisitos:

- Visual Studio (recomendado)  
- .NET 9 SDK instalado  
- Sistema operacional Windows (não testado em Linux)  
- Docker instalado e configurado  

### Banco de Dados com Docker Compose

O projeto utiliza PostgreSQL. Para subir o banco com Docker, use o arquivo `docker-compose.yml` abaixo:

docker-compose up -d

## Rodando a cobertura de teste unitário e gerando relatório de cobertura:  

1. Instale o componente ReportGenerator version **5.4.7**: 
2. Acesse via prompt de comando a pasta do **teste-conta-corrente**, a pasta raíz do projeto.
3. Rode o arquivo **Coverage.bat**, dentro desse arquivo encontram-se os comandos para realizar a cobertura de teste.
    -   ***OBS:*** A versão do ReportGenerator pode alterar a pasta do executável, verifique antes de rodar.
4. Uma vez gerado a cobertura, é possível ver a página com resultado acessando o arquivo localizado: ***BankSystem.Tests\Coverage\index.htm***
 ![Imagem ilustrativa da cobertura de teste](https://github.com/rodrigoSilvestreMoraes/teste-conta-corrente/blob/main/img_cobertura_teste_ilustrativa.png)

## Documentação da API

Toda a documentação necessária para uso da API está disponível via Swagger.

Ao executar o projeto, acesse o Swagger através da URL:

http://localhost:5008/swagger/index.html


> O Swagger fornece detalhes completos sobre os endpoints, payloads, métodos HTTP, códigos de resposta e exemplos de requisição/resposta.

---

## Estrutura do Projeto

O projeto segue uma arquitetura em camadas, dividida nos seguintes diretórios:

-   **BankSystem.API:** Contém a camada de apresentação (API RESTful) com os controladores e configurações do ASP.NET Core.
-   **BankSystem.Application:** Contém a lógica de negócio, DTOs, interfaces de serviço e validadores (FluentValidation).
-   **BankSystem.Infrastructure:** Contém a implementação dos repositórios, configuração de acesso a dados (PostgreSQL com ADO.NET) e outros serviços de infraestrutura.
-   **BankSystem.Tests:** Contém os projetos de testes unitários (xUnit e Moq) para as camadas Application e API.

---

## Tecnologias Utilizadas

-   **.NET 9.0:** Framework para desenvolvimento da aplicação.
-   **ASP.NET Core Web API:** Para construção da API RESTful.
-   **PostgreSQL:** Banco de dados relacional para armazenamento das informações das contas.
-   **ADO.NET:** Para acesso a dados ao PostgreSQL.
-   **FluentValidation:** Para validação de modelos e regras de negócio.
-   **xUnit:** Framework de testes unitários.
-   **Moq:** Biblioteca para criação de mocks em testes unitários.
-   **Docker Compose:** Para orquestração do ambiente de desenvolvimento com PostgreSQL.
-   **Swagger/OpenAPI:** Para documentação e teste interativo da API.

