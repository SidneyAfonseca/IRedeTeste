# Use a imagem base do ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Usado para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar os projetos para dentro do container
COPY ["Clean.Architecture.Inventory.API/Clean.Architecture.Inventory.API.csproj", "Clean.Architecture.Inventory.API/"]
COPY ["Clean.Architecture.Inventory.Application/Clean.Architecture.Inventory.Application.csproj", "Clean.Architecture.Inventory.Application/"]
COPY ["Clean.Architecture.Inventory.Domain/Clean.Architecture.Inventory.Domain.csproj", "Clean.Architecture.Inventory.Domain/"]
COPY ["Clean.Architecture.Inventory.Infrastructure/Clean.Architecture.Inventory.Infrastructure.csproj", "Clean.Architecture.Inventory.Infrastructure/"]
COPY ["Clean.Architecture.Inventory.Persistence/Clean.Architecture.Inventory.Persistence.csproj", "Clean.Architecture.Inventory.Persistence/"]

# Restaurar as dependências
RUN dotnet restore "./Clean.Architecture.Inventory.API/Clean.Architecture.Inventory.API.csproj"

# Instalar a ferramenta dotnet-ef (Entity Framework Core)
RUN dotnet tool install --global dotnet-ef --version 8.0.8

# Adicionar o diretório de ferramentas ao PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar os arquivos restantes e compilar
COPY . .
WORKDIR "/src/Clean.Architecture.Inventory.API"
RUN dotnet build "./Clean.Architecture.Inventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/build
# Baixar o script 'wait-for-it' para aguardar o banco de dados
#COPY ./Clean.Architecture.Inventory.API/wait-for-it.sh /wait-for-it.sh
#COPY ./Clean.Architecture.Inventory.API/entrypoint.sh /entrypoint.sh
#RUN chmod +x /entrypoint.sh /wait-for-it.sh

# Publicar o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clean.Architecture.Inventory.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Finalizar o contêiner de produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Definir o entrypoint
ENTRYPOINT ["dotnet", "Clean.Architecture.Inventory.API.dll"]
# Rodar o comando de migração somente quando o MySQL estiver pronto e então iniciar a aplicação
#ENTRYPOINT ["/bin/bash", "-c", "/entrypoint.sh"]