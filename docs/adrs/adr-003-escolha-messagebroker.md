# ADR: 003: Escolha do Sistema de Mensageria

## Contexto

Para desacoplamento dos serviços, foi decidido que a comunicação entre eles será feita por eventos e de forma assincrona, com isso deve ser escolhido o sitema de mensageria a ser utilizado.

## Decisão

Foi decidido o RabbitMQ pode ser uma tecnologia consolidada, com um throughput que antende as necessidades do sistema, e de gestão simplificada.

## Alternativas

Foram consideradas tenologias nativas de Cloud Providers, como Azure Service Bus, SQS ou Pub/Sub, mas para melhor portabilidade do sistema e até uma possivel implantação OnPremisses, essas opções foram desconsideradas. Outra alternativa seria o Kafka, mas a primeiro momento, os requistos do sistema não necessita de uma solução tão robusta.

## Pontos Positivos

- Gestão e implantação simples.
- Capacidade de configurações de politicas de Dead Letter e Retry para casos de indisponibilidade dos serviços.

## Pontos Negativos

- Necessidade de implantação e gestão do serviço por conta própria.