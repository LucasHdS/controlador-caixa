# ADR: 006: Adicionar uma Camada de Autenticação

## Contexto

Um requisito não funcional da aplicação é garantir que todas as chamadas para as apis sejam autenticadas. Com isso, precisamos decidir a estratégia para atender esse requisito.

## Decisão

Como estamos trabalhando em uma arquitetura distribuida, o serviço de autenticação deve ser capaz de gerenciar multiplos recursos. Para isso, escolhemos utilizar o IDP (Identity Provider) Keycloak. Por ser uma solução robusta, open-source com capacidade de self-hosted e sem custo de licença.

Para garantir que todas as chamadas que cheguem nas apis estejam autenticadas, o gateway vai se comunicar com o idp, validando a autenticação de todas as requests que passar por ele.

## Alternativas

Algumas alternativas que poderiam ser consideradas, era usar o Okta ou PingID, mas são soluções pagas por serem mais robustas e corporativas.

## Pontos Positivos

- Segurança que toda a comunicação com as apis estarão autenticadas.

## Pontos Negativos

- Aumento de latência nas chamadas para validação de autenticação.
