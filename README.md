# Sistema de Controle de Financeiro

## Overview

Esse documento tem como objetivo explicar o processo de construção do sistema de controle de financeiro. Abrangendo tópicos como: objetivo do sistema, arquitetura, decisões tomadas, especifições funcionais e não funcionais.

Para cumprimento do desafio, foi imaginado um comércio que realiza venda de produtos e compra de suprimentos.

Para comprimento do prazo, o código desenvolvido se limitou ao fluxo do backend e sem envolver autenticação (mas está contemplado na arquitetura).

## Contexto

Esse é um sistema com o objetivo de realizar o controle dos lançamentos de débito e credito de um comércio. E a partir desses lançamentos, gerar um relatório do consolidado diário. A fim de trazer ao comerciante, dados sobre o fechamento financeiro do dia.

## Capacidades de Negócio

- **Venda de Produtos:** Setor responsável pela realização das vendas no comércio.
- **Compra de Produtos:** Setor responsável pela realização das compras necessárias para abastecimento do comércio.
- **Gestão Financeiro:** Setor responsável pelo acompanhamento da saúde financeira do comércio.

## Domínios

- **Vendas:** Setor responsável pela realização das vendas no comércio.
- **Compras:** Setor responsável pela realização das compras necessárias para abastecimento do comércio.
- **Financeiro:** Setor responsável pelo acompanhamento da saúde financeira do comércio.

## Requisitos Funcionais

- **Cadastro de Lançamento (crédito ou débito):** O lançamentos podem ser realizados pelos times de compra ou venda. Cada lançamento tem um tipo diferente. ex: compra de um suprimento; venda de um produto. Para cada tipo de lançamento, é atribuido uma natureza, podendo ser Débito ou Crédito.

- **Consulta de Saldo Consolidado Diário:** A consulta de saldo deve retornar o resultado da soma de todos os lançamentos realizados no dia. Essa consulta pode ser extraida a qualquer momento, e deve retornar o dado quente (estado atual do saldo).

## Requisitos Não Funcionais

- **O serviço de controle de lançamento não deve ficar indisponível se o sistema
de consolidado diário cair:** O dois serviços devem funcionar de forma independente, sendo o serviço de lançamento o mais crítico, pois gera impacto operacional no comércio.

- **Em dias de picos, o serviço de consolidado diário
recebe 50 requisições por segundo, com no máximo 5% de perda de
requisições:** Considerar mudança de workload na consulta do saldo. 

- **Autenticação:** Todas as chamadas realizadas para as APIs deve ser autenticadas.

- **Escalabilidade:** Os componentes arquiteturais não podem guardar estado, ou seja, deve ser stateless, para a possibilidade de escalonamento horiozontal.


## Architecture Decision Records

- [ADR: 001: Separação dos Serviços de Lançamento e Consolidação](./docs/adrs/adr-001-separacao-dos-servicos.md)
- [ADR: 002: Adicionar Camada de Comunicação entre Portal e APIs](./docs/adrs/adr-002-inclusao-de-camada-portal-api.md)
- [ADR: 003: Escolha do Sistema de Mensageria](./docs/adrs/adr-003-escolha-messagebroker.md)
- [ADR: 004: Escolha das Tecnologias dos Bancos de Dados](./docs/adrs/adr-004-escolha-bancos-de-dados.md)
- [ADR: 005: Adicionar uma Camada de Cache na Consulta de Saldo](./docs/adrs/adr-005-inclusao-cache.md)
- [ADR: 006: Adicionar uma Camada de Autenticação](./docs/adrs/adr-006-inclusao-idp.md)

## Desenho da Solucão

![Desenho Arquitetural](./docs/diagrams/v1/container-diagram.png)

## Diagramas de Sequencia

O fluxo resumido dos sistema é o seguinte: Usuário acessa portal, faz um lançamento de compra ou venda, a chamada passa pelo gateway, e o serviço de lançamento, gera um débito ou crédito, de acordo com o endpoint chamado (compra ou venda). Após isso, é enviado para o rabbitmq o evento de lancamento realizado, a api de consolidação consome esse evento, e soma o valor do presente no evento com o saldo diário de acordo com a data do lançamento.

Os fluxos detalhados das funcionalides podem ser vistos no link abaixo:
- [Diagramas de Sequência](./docs/diagrams/v1/sequence-diagrams.md#Fluxo)

## Monitoramento e Observabilidade
Para monitoramento da solução, os serviços devem fazer implementação de heathcheck, verificando a integradade das comunicações com os componentes consumidos. Para consumo do endpoint de healthcheck utilizaremos o **Prometheus**, consumindo dados também como uso de memória e CPU. Para visualização desses dados, pode-se usar o **Grafana** integrado com o Prometheus.

Para observilidade, tracing e analise de logs, vamos usar **ElasticSearch + Kibana**. Podendo ver a comunicação entre os serviços, através de APM e estratégias de correlationID, além de analisar mais detalhadamente logs gerados pelas aplicações.

## Rodando Localmente

A aplicação possui um docker compose responsável por subir os componentes:
- lancamentoApi
- consolidadorApi
- gateway
- postgresql
- redis
- rabbitmq

Para rodar basta:
1. Ir até o diretório **/source**
2. Rodar o comando ``docker compose build``
3. Rodar o comando ``docker compose up``

Com isso, os serviços estarão acessivies via **localhost** nas seguintes portas:
- gateway: 5000
- lancamentoApi: 5001
- consolidadorApi: 5002
- postgres: 5431
- redis: 6379
- rabbitmq management: 15672

## Possibilidades de Melhoria

Melhorias que poderiam ser desenvolvidas futuramente, podendo ser consideradas como débitos técnicos.

- Controle de idempotência para garantir processamento unico das transações
- Aplicação do Pattern Outbox para aumentar resiliência em caso de insponibiliade do sistema de mensageria.

Essas melhorias, estão contempladas em uma segunda versão das documentações:
[Documentações V2](./docs/diagrams/v2/diagramsv2.md)


## Custo de Hospedagem (Azure)

**App Service Plan** - Premium V2 Tier; 1 P2V2 (2 Core(s), 7 GB RAM, 250 GB Storage) x 730 Hours; Linux OS; | **R$845.71**
 
- WebApp: Gateway 
- WebApp: LancamentoApi
- WebApp: ConsolidadorApi

**PostgreSQL** - Flexible Server Deployment, General Purpose Tier | **R$748.52**

**RabbitMQ** - Virtual Machine A2 (2 Cores, 3.5 GB RAM) x 730 Hours Linux | **R$502.40**

**Redis** - Azure Cache for Redis; Basic tier; 1 C0 instances, 730 Hours | **R$92.11**

**KeyCloack** - Virtual Machine A2 (2 Cores, 3.5 GB RAM) x 730 Hours Linux | **R$502.40**

**obs:** O custo de hospegam, pode variar após um plano de capacidade bem definido e teste de carga para entender o consumo de recursos de cada aplicação.
