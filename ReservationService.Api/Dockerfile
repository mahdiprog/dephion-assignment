#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ReservationService.Api/ReservationService.Api.csproj", "ReservationService.Api/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
COPY ["ReservationService.Infra/ReservationService.Infra.csproj", "ReservationService.Infra/"]
COPY ["ReservationService.Application/ReservationService.Application.csproj", "ReservationService.Application/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["ReservationService.Domain/ReservationService.Domain.csproj", "ReservationService.Domain/"]
RUN dotnet restore "ReservationService.Api/ReservationService.Api.csproj"
COPY . .
WORKDIR "/src/ReservationService.Api"
RUN dotnet build "ReservationService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReservationService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReservationService.Api.dll"]