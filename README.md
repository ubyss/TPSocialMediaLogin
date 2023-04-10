## **Índice**

1. **[Introdução](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#introdu%C3%A7%C3%A3o)**
2. **[Requisitos](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#requisitos)**
3. **[Funcionalidades](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#funcionalidades)**
4. **[Arquitetura do Sistema](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#arquitetura)**
5. **[API](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#api)**
6. **[Telas](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#telas)**
7. **[Conclusão](https://chat.openai.com/chat/47d2b265-2a33-4545-99a8-d20a040a8b3a#conclus%C3%A3o)**

## **1. Introdução**

O projeto consiste em uma rede social para pets e seus donos, criada com ASP.NET e C#. O objetivo é proporcionar um espaço onde os donos possam compartilhar informações e imagens de seus animais de estimação. O sistema utiliza uma arquitetura MVC (Model-View-Controller) e uma API para gerenciar as requisições.

## **2. Requisitos**

- .NET Core SDK
- Visual Studio ou Visual Studio Code

## **3. Funcionalidades**

- Cadastro de novos usuários
- Autenticação de usuários já cadastrados
- Visualização de galeria com pets cadastrados
- Gerenciamento do perfil do usuário

## **4. Arquitetura do Sistema**

O projeto é composto por duas partes principais:

- Web MVC: Responsável pela interface do usuário e interações do cliente.
- Web API: Gerencia as requisições e a comunicação entre o cliente e o servidor.

## **5. API**

A Web API possui as seguintes requisições:

- **`GET /api/Register`**: Verifica os dados e registra o usuário no banco de dados.
- **`GET /api/Users/Login`**: Verifica os dados de autenticação do usuário.
- **`POST /api/Users/logout`**: Lança uma requisição para apagar a session do usuário.
- **`PUT /api/Users/{id}`**: Atualiza os dados de um usuário específico.
- **`DELETE /api/Users/getUserById/{id}`**: Retorna os dados de um usuário.

## **6. Telas**

### **Registro**

Essa página é para o cadastro de um novo usuário. Os campos disponíveis são:

- Nome do usuário
- Email
- Senha

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/e9baed35-de2d-4e6f-a264-7127dcc9cfde/Untitled.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/e9baed35-de2d-4e6f-a264-7127dcc9cfde/Untitled.png)

### **Login**

Essa página é para a autenticação do usuário já cadastrado. Os campos disponíveis são:

- Email
- Senha

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/c9ef24b8-867c-4438-b633-aad9d856db07/Untitled.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/c9ef24b8-867c-4438-b633-aad9d856db07/Untitled.png)

### **Galeria**

Essa área mostra todos os usuários cadastrados e seus pets. Os usuários podem visualizar os pets dos outros usuários e acessar seus perfis.

![https://s3-us-west-2.amazonaws.com/secure.notion-static.com/7aeeb3d3-f0c8-4159-9807-f839f691f0f0/Untitled.png](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/7aeeb3d3-f0c8-4159-9807-f839f691f0f0/Untitled.png)

### **Perfil**

Página do perfil do usuário, onde mostra seu nome, a imagem e seus dados cadastrados. Também é possível alterar e atualizar os dados que foram cadastrados.

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/9c46369f-fe47-42fb-89a4-50cb5802ae57/Untitled.png)

## **7. Conclusão**

A rede social de pets e seus donos oferece uma plataforma para os amantes de animais compartilharem informações e imagens de seus bichinhos para entrarem em contato entre si e fazerem novas amizades
