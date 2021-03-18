#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Meetings.Web/Meetings.Web.csproj", "Meetings.Web/"]
COPY ["Meetings.Application/Meetings.Application.csproj", "Meetings.Application/"]
COPY ["ReservationService.Messaging/ReservationService.Messaging.csproj", "ReservationService.Messaging/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Meetings.Domain/Meetings.Domain.csproj", "Meetings.Domain/"]
COPY ["Meetings.Infra/Meetings.Infra.csproj", "Meetings.Infra/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
RUN dotnet restore "Meetings.Web/Meetings.Web.csproj"
COPY . .
WORKDIR "/src/Meetings.Web"
RUN dotnet build "Meetings.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Meetings.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Meetings.Web.dll"]