# Образ asp.net для выполнения приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Образ sdk для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файлы проекта
COPY ["Client.WebMvc/Client.WebMvc.csproj", "Client.WebMvc/"]
COPY ["Datasource.Ef/Datasource.Ef.csproj", "Datasource.Ef/"]

# Восстанавливаем зависимости
RUN dotnet restore "Client.WebMvc/Client.WebMvc.csproj"

# Копируем остальные файлы
COPY . .

# Сборка проекта
WORKDIR "/src/Client.WebMvc"
RUN dotnet build "Client.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация проекта
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Client.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Копируем результат публикации
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.WebMvc.dll"]