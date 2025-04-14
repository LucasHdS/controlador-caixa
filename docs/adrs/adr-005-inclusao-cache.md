# ADR: 005: Adicionar uma Camada de Cache na Consulta de Saldo

## Contexto

O serviço de leitura de saldo consolidado pode ter picos de acesso, e o sistema precisa garantir a entrega dos dados consultados. 

## Decisão

Para entrega rápida do resultado consolidado em momentos de pico, foi considerado a inclusão de uma camada de Cache distribuido para ser consultada antes de um acesso direto a base.

## Alternativas

A outra opção era manter todas as consultas sendo realizadas no banco de dados, mas em cenáriios de alto fluxo de consulta e lançamentos sendo realizados, o banco seria muito consumido, podendo se tornar um gargalo no sistema. Outra opção seria adicionar um cache em memória, já que a informação é unica para qualquer usuário que consulta, mas em cenários de escalonamento horizontal, os valores cacheados em alguns momentos não seriam os mesmos em todas as réplicas, podendo gerar inconsistência de dados.

## Pontos Positivos

- Melhora na perfomance das consultas
- Possibilidade de escalonamento horizontal por ser cache distribuido.

## Pontos Negativos

- Aumento de complexidade das operações.
- Um serviço a mais para gerenciar.
