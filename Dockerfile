FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /
COPY Animals.sln ./
COPY src/Animals.Core/*.csproj ./Animals.Core/
COPY src/Animals.API/*.csproj ./Animals.API/

RUN dotnet restore
COPY . .
WORKDIR /Animals.Core
RUN dotnet build -c Release -o /app

WORKDIR /Animals.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Animals.API.dll"]