# Make me a Pix API
<h1>Descrição</h1>

  <p>A Make me a Pix API é uma aplicação que possibilita aos usuários a criarem suas chaves pix em suas contas bancárias para determinados bancos (PSP)</p>

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
