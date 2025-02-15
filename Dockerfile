# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 

WORKDIR /app

# Copia apenas o csproj e restaura as dependências
COPY ["wsapi.csproj", "./"]
RUN dotnet restore "./wsapi.csproj"

# Copia o restante do código-fonte
COPY . . 

# Compila e publica a aplicação em uma pasta otimizada
RUN dotnet publish "./wsapi.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
EXPOSE 8080

# Copia os arquivos publicados na etapa anterior
COPY --from=build /app/publish .

# Define o comando de entrada para rodar a API 
ENTRYPOINT ["dotnet", "wsapi.dll"]
