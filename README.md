# Make me a Pix API
<h1>Descrição</h1>

  <p>A Make me a Pix API é uma aplicação que possibilita aos usuários a criarem suas chaves pix em suas contas bancárias para determinados bancos (PSP)</p>

<h1>Rotas</h1>
<h2>Keys</h2>
• <strong>Criar Chave:</strong> Registra uma nova chave pix.


    POST /keys

• <strong>Retornar Chave:</strong> Retorna todas as informações de uma determinada chave pix.

    GET /keys/:type/:value
