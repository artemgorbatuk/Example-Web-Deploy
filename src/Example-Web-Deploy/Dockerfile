FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Client.WebMvc/Client.WebMvc.csproj", "Client.WebMvc/"]
COPY ["Datasource.Ef/Datasource.Ef.csproj", "Datasource.Ef/"]
RUN dotnet restore "Client.WebMvc/Client.WebMvc.csproj"
COPY . .
WORKDIR "/src/Client.WebMvc"
RUN dotnet build "Client.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Client.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.WebMvc.dll"]