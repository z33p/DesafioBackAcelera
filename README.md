# DesafioBackAcelera

Desafio realizado em 4 dias (29/05 a 02/06)

O único requisito pra rodar o projeto é adicionar a chave da API no arquivo appsettings.json.<br/>
Seguindo a risca o desafio a única depedência do projeto é o SQLite.<br/>

## Rotas
**Controller**<br/>
/video

**Actions**<br/>
GET: Obtem todos os videos<br/>
POST: Cria um vídeo (Obs.: A duração do vídeo deve estar no padrão ISO 8601)<br/>
PUT: Atualiza um video<br/>
DELETE: Remove um video (somente adiciona uma flag de deletado como descrito no desafio)<br/>

### Opicionais
Um dos tópicos opicionais se tratava de um servidor de implementar autenticação com JWT.<br/>
Eu já havia desenvolvido pra fins de aprendizado pessoal um servidor de autenticação com JWT segue o link

> https://github.com/z33p/AuthServer

#### Observações
Na primeira execução do projeto ele irá fazer a carga de dados Um dos requísitos:<br/>

> Os vídeos devem estar relacionados à manipulação

Talves não tenha sido alcançado por não estar claro o suficiente, e não ter ocorrido feedback a respeito do mesmo.<br/>
Desse modo, foram carregados videos a partir de q="Manipulação".<br/>

Outro ponto importante é que será criado um branch paralelo do projeto onde alguns pontos não finalizados serão concluidos<br/>
Visto que o prazo do teste é de 7 dias no RADME do repositório do desafio e no meu caso tive 4 dias para concluir.<br/>

Após uma a duas semanas será feito merge nesse único branch.

Agradeço a oportunidade!
