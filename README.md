
# Gerenciamento de clientes

Uma aplicação de backend, desenvolvida inteiramente em .NET Core 8, composta por: 

- Uma API de gerenciamente de clientes e pedidos (seguindo padrão CQRS), que produz mensagens (com as informações de pedido e cliente) para um tópico do Kafka;

- Um Worker Service, que consome as mensagens do tópico do Kafka, e as envia, via Refit, para uma API de envio de emails;

- Uma API de envio de emails, que recebe as informações do Worker e envia informações de cadastro e de pedido para o cliente, via SMTP. 



## Backend
O backend é composto de duas APIs, sem autenticação e usando Swagger como documentação.

Backend desenvolvido em .NET Core versão 8. Para comunicação com as Apis externas, utilizou-se a biblioteca REFIT;

Na WebApi, procurou-se seguir os princípios do clean code e do solid e seguiu-se o padrão CQRS, com uma arquitetura em camadas;
## Instalação da aplicação na máquina

Para instalar o frontend, basta clonar o projeto na máquina. Depois disso abrir o arquivo solução (Order.Delivery.App.sln) na pasta (Order.Delivery.App)

O docker-compose.yml está no mesmo diretório do arquivo .sln e é só subir usando o docker-compose up -d

Para rodar o projeto localmente na máquina, basta definir os três projetos de inicialização:

Order.Delivery.App.Api e o Order.Delivery.Email.Service e Order.Delivery.Notification.Service
