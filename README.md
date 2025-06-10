## Sobre o Projeto

Este projeto, chamado **Meu Livro de Receitas**, é uma aplicação voltada para pessoas que gostam de cozinhar e compartilhar suas receitas favoritas. A ideia central é criar uma plataforma que facilite o gerenciamento das receitas, tornando o processo de cozinhar mais organizado e prazeroso.

A **API** foi construída utilizando **.NET Core**, com base no curso [**.NET Core: Um curso orientado para o mercado de trabalho**](https://www.udemy.com/course/net-core-curso-orientado-para-mercado-de-trabalho), do professor **Wellison Arley**. O curso ensina as melhores práticas e técnicas utilizadas no mercado de trabalho para o desenvolvimento de aplicações web modernas. O repositório oficial do curso pode ser acessado [aqui](https://github.com/welissonArley/MyRecipeBook/tree/develop).

A **API** tem como objetivo fornecer funcionalidades para que os usuários possam cadastrar suas receitas, editar, excluir e filtrar receitas de maneira intuitiva. Cada receita pode conter um título, lista de ingredientes, instruções, tempo de preparo, nível de dificuldade e até uma imagem ilustrativa.

Além disso, a **API** é flexível e oferece suporte a dois bancos de dados populares: **MySQL** e **SQLServer**, permitindo que você escolha o melhor banco para o seu ambiente. A integração de pipelines **CI/CD** e o uso de **Sonarcloud** para análise contínua de código garantem um processo de desenvolvimento eficiente e seguro.

O projeto foi estruturado seguindo boas práticas de desenvolvimento, como **Domain-Driven Design (DDD)** e os princípios de **SOLID**, promovendo uma arquitetura limpa e modular. A validação de dados é feita utilizando **FluentValidation**, que ajuda a garantir que as entradas estejam sempre corretas.

A qualidade do código foi uma prioridade, e por isso, incluir **testes unitários e de integração** para garantir que as funcionalidades estejam funcionando conforme esperado. A injeção de dependências também foi aplicada para melhorar a modularidade e facilitar a manutenção do código.

Tecnologias como **Entity Framework** foram usadas para mapear dados entre a aplicação e o banco de dados. O gerenciamento do projeto segue a metodologia ágil **SCRUM**, e a autenticação é feita de forma segura com **Tokens JWT e Refresh Token**. O uso de **Git** e a estratégia de ramificação **GitFlow** ajudam a organizar e versionar o código de maneira eficaz.

### Funcionalidades

- **Gerenciamento de Receitas**: Os usuários podem criar, editar, excluir e buscar receitas de forma simples. 🍲✏️🗑️🔍
- **Login com Google**: Autenticação via conta Google para facilitar o acesso. 🔑🔗🟦
- **Integração com ChatGPT**: A utilização de IA para gerar receitas a partir de ingredientes fornecidos, tornando a experiência mais interativa. 🤖🍳
- **Mensageria**: Usada para controlar a exclusão de contas de maneira eficiente com o **Service Bus**. 📩🗂️🚫
- **Upload de Imagem**: Os usuários podem enviar imagens para ilustrar suas receitas. 📸⬆️🖼️

### Tecnologias Utilizadas

![badge-dot-net]  
![badge-windows]  
![badge-visual-studio]  
![badge-mysql]  
![badge-sqlserver]  
![badge-swagger]  
![badge-docker]  
![badge-azure-devops]  
![badge-azure]  
![badge-azure-pipelines]  
![badge-google]  
![badge-openai]  
![badge-sonarcloud]

## Começando

Para rodar o projeto localmente, basta seguir os passos abaixo.

### Requisitos

* Visual Studio 2022+ ou Visual Studio Code
* Windows 10+ ou Linux/MacOS com [.NET SDK][dot-net-sdk] instalado
* MySQL Server ou SQL Server

### Instalação

1. Clone o repositório:
    ```sh
    git clone https://github.com/MateusNascimento2002/MyRecipeBook.git
    ```

2. Configure o arquivo `appsettings.Development.json` com suas informações.
3. Execute a API e comece a testar as funcionalidades! :)

## Licença

Este projeto pode ser usado para fins de estudo e aprendizado. A distribuição ou comercialização não é permitida.

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Sonarcloud -->
[sonarcloud-dashboard]: https://sonarcloud.io/summary/overall?id=welissonArley_MyRecipeBook
[sonarcloud-qualityGate]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=alert_status
[sonarcloud-bugs]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=bugs
[sonarcloud-vulnerabilities]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=vulnerabilities
[sonarcloud-code-smells]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=code_smells
[sonarcloud-coverage]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=coverage
[sonarcloud-duplicated-lines]: https://sonarcloud.io/api/project_badges/measure?project=welissonArley_MyRecipeBook&metric=duplicated_lines_density

<!-- Badges -->
[badge-sqlserver]: https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]: https://img.shields.io/badge/Windows-0078D4?logo=windows&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
[badge-docker]: https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff&style=for-the-badge
[badge-azure-devops]: https://img.shields.io/badge/Azure%20DevOps-0078D7?logo=azuredevops&logoColor=fff&style=for-the-badge
[badge-azure]: https://img.shields.io/badge/Microsoft%20Azure-0078D4?logo=microsoftazure&logoColor=fff&style=for-the-badge
[badge-azure-pipelines]: https://img.shields.io/badge/Azure%20Pipelines-2560E0?logo=azurepipelines&logoColor=fff&style=for-the-badge
[badge-google]: https://img.shields.io/badge/Google-4285F4?logo=google&logoColor=fff&style=for-the-badge
[badge-openai]: https://img.shields.io/badge/OpenAI-412991?logo=openai&logoColor=fff&style=for-the-badge
[badge-sonarcloud]: https://img.shields.io/badge/SonarCloud-F3702A?logo=sonarcloud&logoColor=fff&style=for-the-badge
