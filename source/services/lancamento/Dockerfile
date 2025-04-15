# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:9b0a4330cb3dac23ebd6df76ab4211ec5903907ad2c1ccde16a010bf25f8dfde AS build

WORKDIR /src

# Copiar todos os arquivos do projeto
COPY . ./

# Restaurar dependências
RUN dotnet restore "lancamento/src/lancamento.api/Api.csproj"

# Build
WORKDIR /src/lancamento/src/lancamento.api
RUN dotnet publish -c Release -o /app/publish
RUN rm -rf /app/publish/BuildHost-net427 /app/publish/BuildHost-netcore

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:c3aee4ea4f51369d1f906b4dbd19b0f74fd34399e5ef59f91b70fcd332f36566 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Api.dll", "--urls", "http://0.0.0.0:80"]