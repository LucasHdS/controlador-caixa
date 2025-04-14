# ADR: 002: Adicionar Camada de Comunicação entre Portal e APIs

## Contexto

O sistema possui duas APIs e apenas um portal que consome endpoints de ambas. Portanto, deve ser decidido se o portal terá comunicação direta com os dois serviços, ou será incluida alguma camada abastrair essa comunicação.

## Decisão

Foi decidido que nesse momento, não será incluido um BFF, considerando que o sistema tem apenas dois serviços, e o BFF nesse primeiro só funcionaria como um gateway, pois não precisaria agregar requests a partir de uma chamada do Portal. Todas as funcionalidades mapeadas atualmente, só consome um endpoint. Como solução sera incluida uma solução de API Gateway stateless com Ocelot.

## Alternativas

Uma das alternavias foi não incluir nada entre o portal e as APIs. Porém, para isso as duas APIs precisariam ficar expostas para internet, e o Portal precisaria conhecer os dois serviços. Por questões de segurança, essa solução foi discartada.


## Pontos Positivos

- Não precisa expor os dois serviços para internet.
- Não tem tanta complexidade quanto o BFF, e não tem necessidade de alteração na inclusão de novas rotas nos serviços.

## Pontos Negativos

- Não tem capacidade de fazer agragação de consultas e transformação complexa de dados como o BFF.