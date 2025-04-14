FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app
COPY . ./

RUN dotnet publish /src/lancamento.api/Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0

COPY --from=build-env /app/publish ./
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll", "--urls", "http://0.0.0.0:80"]