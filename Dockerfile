FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["RuletaOnline.csproj", "./"]
RUN dotnet restore "./RuletaOnline.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RuletaOnline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RuletaOnline.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RuletaOnline.dll"]
