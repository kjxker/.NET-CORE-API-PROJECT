FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
WORKDIR /src
COPY ["marvel-site-api/marvel-site-api.csproj", "marvel-site-api/"]
RUN dotnet restore "marvel-site-api/marvel-site-api.csproj"
COPY . .
WORKDIR "/src/marvel-site-api"
RUN dotnet build "marvel-site-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "marvel-site-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "marvel-site-api.dll"]
