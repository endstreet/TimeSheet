FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Trm.MaLogger.App.csproj", "."]
RUN dotnet restore "Trm.MaLogger.App.csproj"
COPY . .
RUN dotnet build "Trm.MaLogger.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trm.MaLogger.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trm.MaLogger.App.dll"]