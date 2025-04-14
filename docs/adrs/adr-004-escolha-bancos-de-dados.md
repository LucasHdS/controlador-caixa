# ADR: 004: Escolha das Tecnologias dos Bancos de Dados

## Contexto

Todas os lançamentos realizados no sistema, além dos dados consolidados devem ser armazenados para consulta.

## Decisão

Foi decidido usar o PostgreSQL como banco de dados, por ser um banco muito utilizado e sem custo de licença. Além de ser um banco relacional, o que facilita a gestão dos lançamentos, que podem ser de diferentes tipos e naturezas diferentes.

## Alternativas

Uma opção considerada para o banco relacional foi o SQLServer, mas para reduzir custos com licença, a opção foi descartada. Outra opção, seria usar um banco NoSQL, como MongoDB para a base de dados do consolidador, por ser um serviço que vai exibir muitas operações de leitura. Porém, como os dados a serem retornados são limitados, e não possui uma grande quantidade de colunas, manter com o PostgreSQL fez mais sentido. Decisão que pode ser revisada se roadmap do serviço ter consultas de alta complexidade e quantidade maior de colunas.

## Pontos Positivos

- Bancos de dados de fácil manutenção e implantação
- Baixa complexidade por utilizar bancos de uma unica tecnologia

## Pontos Negativos

- Caso seja necessário consulta que traga uma alta quantidade de registros com grande quantidade de colunas, a escolha de um banco NoSQL deve ser revisitada.