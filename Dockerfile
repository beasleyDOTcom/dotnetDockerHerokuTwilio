#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["dotnetDockerHerokuTwilio.csproj", "."]
RUN dotnet restore "./dotnetDockerHerokuTwilio.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnetDockerHerokuTwilio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnetDockerHerokuTwilio.csproj" -c Release -o /app/publish

RUN adduser \
  --disabled-password \
  --home /app \
  --gecos '' app \
  && chown -R app /app
USER app
RUN dotnet dev-certs https
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "dotnetDockerHerokuTwilio.dll"]