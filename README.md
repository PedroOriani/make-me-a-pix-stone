# Make me a Pix API
<h1>Descrição</h1>

  <p>A Make me a Pix API é uma aplicação que possibilita aos usuários a criarem suas chaves pix em suas contas bancárias para determinados bancos (PSP)</p>

  <h3>Comandos</h3>
  Para inicializar corretamente a aplicação, execute os seguintes comandos:

```bash
dotnet restore
"Adicione as informações do banco de dados no arquivo appsettings.json e rode as migrations"
dotnet ef migrations add <nome_da_migração>
dotnet ef database update
dotnet build
```

<h1>Rotas</h1>
<h2>Keys</h2>
• <strong>Criar Chave:</strong> Registra uma nova chave pix.


    POST /keys

  ```bash
Body:

{
  "key": {
		"value": string,
		"type": string // CPF, Email, Phone ou Random
	}
	"user": {
		"cpf": string
	},
	"account": {
		"number": string
		"agency": string
	} 
}
```

• <strong>Retornar Chave:</strong> Retorna todas as informações de uma determinada chave pix.

    GET /keys/:type/:value

  ```bash
Retorno:
{
	"key": {
		"value": string,
		"type": string // CPF, Email, Phone ou Random
	    },
	"user": {
		"name": string,
		"maskedCpf": string // somente os três primeiros e dois últimos dígitos
	},
	"account": {
		"number": string,
		"agency": string,
		"bankName": string,
		"bankId": string
	}
}
```

<h2>Payments</h2>
<strong>Realizar Pagamento:</strong> Registra um novo pagamento.

    POST /payments

  ```bash
Body:

{
	"origin": {
		"user": {
			"cpf": string
		},
		"account": {
			"number": string,
			"agency": string,
		}
	},
	"destiny": {
		"key": { // chave pix do destinatário
			"value": string
			"type": string // CPF, Email, Phone ou Random
		}
	}
	"amount": integer,
	"description": string // opcional
}
```

<h1>Monitoramento</h1>

<h3>Comandos</h3>
  Para inicializar corretamente o monitoramento da aplicação, siga os seguintes passos:

```bash
- Configure os arquivos de Monitoring conforme suas informações: IP, portas, etc.
- Abra dois terminais: em um rode a aplicação com "dotnet run" e no outro entre na pasta "/pix/Monitoring" e rode o comando de "docker compose up -d"
- Acesse o Grafana em localhost:3000
```


https://github.com/PedroOriani/pix-consumer/settings