## **Índice**

1. [Introdução]
2. [Requisitos]
3. [Funcionalidades]
4. [Arquitetura do Sistema]
5. [API]
6. [Telas]
7. [Conclusão]

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

![image](https://user-images.githubusercontent.com/80261904/230902573-a47e68f8-c9e6-4c33-a644-1e813b6541ea.png)

### **Login**

Essa página é para a autenticação do usuário já cadastrado. Os campos disponíveis são:

- Email
- Senha

![image](https://user-images.githubusercontent.com/80261904/230902596-19e2219b-8ab7-4ccc-9fa4-3e6d56ca1f68.png)

### **Galeria**

Essa área mostra todos os usuários cadastrados e seus pets. Os usuários podem visualizar os pets dos outros usuários e acessar seus perfis.

![image](https://user-images.githubusercontent.com/80261904/230902715-98f6bdd7-75d9-43d9-8524-bbaba201cb40.png)


### **Perfil**

Página do perfil do usuário, onde mostra seu nome, a imagem e seus dados cadastrados. Também é possível alterar e atualizar os dados que foram cadastrados.

![image](https://user-images.githubusercontent.com/80261904/230902859-7f86ed56-2f98-4a22-b3fc-65e215f8cd40.png)


## **7. Conclusão**

A rede social de pets e seus donos oferece uma plataforma para os amantes de animais compartilharem informações e imagens de seus bichinhos para entrarem em contato entre si e fazerem novas amizades
