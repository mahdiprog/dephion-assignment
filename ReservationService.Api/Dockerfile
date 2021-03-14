#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CryptoRate.Web/CryptoRate.Web.csproj", "CryptoRate.Web/"]
RUN dotnet restore "CryptoRate.Web/CryptoRate.Web.csproj"
COPY . .
WORKDIR "/src/CryptoRate.Web"
RUN dotnet build "CryptoRate.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoRate.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoRate.Web.dll"]