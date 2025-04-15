# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build

WORKDIR /src

# Copiar todos os arquivos do projeto
COPY . ./

# Restaurar dependências
RUN dotnet restore "portal.gateway.csproj"
    
# Build
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "portal.gateway.dll"]