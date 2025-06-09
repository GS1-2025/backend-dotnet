# ☀ Sensolux

## 👨‍💻 Integrantes
- RM558763 • Eric Issamu de Lima Yoshida  
- RM555010 • Gustavo Matias Teixeira  
- RM557515 • Gustavo Monção  

## 💬 Vídeo Pitch  
[Youtube](https://youtu.be/WJmfimRwF8w)  

## 💬 Vídeo demonstrando o funcionamento  
[Youtube](https://www.youtube.com/playlist?list=PLsjNwOw0FQHs3V-1y2sqQyshLFREi9tOE)  

API RESTful para gerenciamento de pedidos da plataforma Sensolux.  
Construída em .NET 8, cuida da parte de vendas e pedidos, integrada com um sistema Java que gerencia a parte operacional e lógica do app.

---

## Visão Geral
O Sensolux é um sistema dividido em duas camadas principais:

- **Backend .NET (este projeto)**: responsável pelo cadastro, listagem, atualização e exclusão de pedidos, além da gestão dos itens e produtos relacionados a cada pedido.

- **Sistema Java**: gerencia a lógica de negócio, usuários, autenticação e outras funcionalidades do aplicativo principal.

O backend .NET funciona como um microsserviço focado em vendas e pedidos, expondo endpoints para consumo do app em Java, que se comunica via chamadas HTTP.

---

## Tecnologias
- .NET 8
- Entity Framework Core (com SQL Server)  
- FluentValidation para validação de requests  
- Swagger para documentação (opcional, não incluso aqui)  
- API RESTful  

---

## Endpoints

| Método | Endpoint          | Descrição                    | Código HTTP esperado           |
|--------|-------------------|------------------------------|-------------------------------|
| GET    | /api/pedido       | Lista todos os pedidos        | 200 OK                        |
| GET    | /api/pedido/{id}  | Busca pedido por ID           | 200 OK / 404 Not Found        |
| POST   | /api/pedido       | Cria um novo pedido           | 201 Created / 400 Bad Request |
| PUT    | /api/pedido/{id}  | Atualiza um pedido existente  | 204 No Content / 400 / 404    |
| DELETE | /api/pedido/{id}  | Remove um pedido pelo ID      | 204 No Content / 404 Not Found|

---

## 🧪 JSONs de Teste

Para realizar testes completos com os dados reais do banco, configure sua `appsettings.Development.json` com a seguinte string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "User ID=RM558763;Password=Fiap#2025;"
}
``` 
⚠️ Essa conexão permite acesso à tabela usuario, que é gerenciada pelo sistema Java integrado.

🔗 Projeto Java (lógica do app, autenticação e usuários):
https://github.com/GS1-2025/backend-java.git

📸 Exemplo de estrutura da tabela usuario:

![image](https://github.com/user-attachments/assets/6f40120b-e28e-4c0c-9366-d8d6dc6e88be)



### Produto

```json
{
  "nome": "Sensor de Temperatura",
  "descricao": "Sensor de alta precisão para temperatura ambiente",
  "precoUnitario": 99.90
}
```

---

### Endereço

```json
{
  "cep": "12345678",
  "estado": "SP",
  "cidade": "São Paulo",
  "bairro": "Centro",
  "rua": "Rua das Flores",
  "usuarioId": 1
}
```

---

### Pedido

```json
{
  "dataPedido": "2025-06-09T14:30:00",
  "itens": {
    "pedidoId": 0,
    "usuarioId": 1,
    "quantidade": 2,
    "produtoIds": [1]
  }
}
```

---

### Sensor

```json
{
  "tipo": "Temperatura",
  "modelo": "TMP-1000",
  "descricao": "Sensor digital de temperatura com alta precisão"
}
```

---

## Campos para Testes Personalizados

> **Preencha os campos abaixo com dados do seu banco para realizar testes:**

| Campo              | Valor Exemplo       | Descrição                      |
|--------------------|--------------------|-------------------------------|
| `usuarioId`        | 1                  | ID do usuário existente no banco |
| `produtoId(s)`     | 1, 2, 3            | IDs dos produtos cadastrados  |
| `pedidoId`         | 5                  | ID do pedido para atualização |
| `dataPedido`       | 2025-06-09T14:30:00| Data e hora do pedido          |

---

## Visualizar Usuários no Banco

Você pode listar os usuários cadastrados no banco acessando o endpoint:  

```
GET /api/usuario
```

Exemplo de resposta:

```json
[
  {
    "id": 1,
    "nome": "Eric Issamu",
    "email": "eric@example.com"
  },
  {
    "id": 2,
    "nome": "Gustavo Matias",
    "email": "gustavo@example.com"
  }
]
```

---

## Instruções para Execução

Clone este repositório:

```bash
git clone https://github.com/seu-usuario/sensolux.git
cd sensolux
```

Configure a string de conexão no `appsettings.json` para o seu banco de dados SQL Server.

Restaure pacotes e rode a migração para criar o banco:

```bash
dotnet restore
dotnet ef database update
```

Execute a API:

```bash
dotnet run
```

A API estará disponível por padrão em `https://localhost:5001` ou `http://localhost:5000`.

---

## Integração com Sistema Java

O sistema Java do app faz chamadas HTTP para esta API para:

- Listar pedidos existentes do usuário  
- Criar novos pedidos após a finalização da compra  
- Atualizar pedidos (exemplo: alteração de status)  
- Excluir pedidos cancelados  

A comunicação entre os sistemas é via JSON usando os endpoints acima. O backend .NET não gerencia usuários diretamente; isso fica a cargo do sistema Java, que passa o `usuarioId` nas requisições para associar os itens do pedido.

Caso queira utilizar o backend Java para funcionalidades operacionais e de lógica do app, acesse o repositório do projeto Java:  

🔗 [Projeto Backend Java Sensolux](https://github.com/GS1-2025/backend-java.git)

---

## Validação e Erros

A API utiliza FluentValidation para validar os dados de entrada. Em caso de erro, retorna um JSON com as mensagens detalhadas, por exemplo:

```json
{
  "mensagem": "Erro de validação no servidor.",
  "erros": [
    {
      "Campo": "dataPedido",
      "Mensagem": "Data do pedido deve ser maior ou igual a hoje."
    },
    {
      "Campo": "itens.quantidade",
      "Mensagem": "Quantidade deve ser maior que zero."
    }
  ]
}
```
