# ADR: 001: Separação dos Serviços de Lançamento e Consolidação

## Contexto

Um dos requisitos não funcionais do sistema, é que o serviço de lançamento, não seja impactado em casos de indisponibilidade do serviço de consolidação. Além disso, os dois serviços tendem a ter workloads diferentes.

## Decisão

Para garantir o desacoplamento das funcionalidades, foi decidido então separar os serviços em duas APIs. Além disso, para garantiar a resiliencia da api de lançamento principalmente, a comunicação entre as duas APIs deve ser realizada através de um serviço de mensageria e cada um terá um banco de dados.

## Alternativas

Como esse era um requisito, não foi considerado outro cenários, senão a separação dos serviços. Com relação ao uso do messagem broker, umas das opções, seria realizar a comunicação REST entre as duas APIs, porém isso iria impactar o serviço de lançamento, em cenários de indisponibilidade do serviço de consolidação.

## Pontos Positivos

- Descoplamento dos serviços e capacidade de escalonamento individualizado.
- Resiliencia do sistema de lançamento em caso de indispobilidade.

## Pontos Negativos

- Aumento de complexidade, por ter mais um serviço para gerenciar.
