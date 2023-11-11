#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CandidateBrowserCleanArch.API/CandidateBrowserCleanArch.API.csproj", "CandidateBrowserCleanArch.API/"]
COPY ["CandidateBrowserCleanArch.Application/CandidateBrowserCleanArch.Application.csproj", "CandidateBrowserCleanArch.Application/"]
COPY ["CandidateBrowserCleanArch.Domain/CandidateBrowserCleanArch.Domain.csproj", "CandidateBrowserCleanArch.Domain/"]
COPY ["CandidateBrowserCleanArch.Identity/CandidateBrowserCleanArch.Identity.csproj", "CandidateBrowserCleanArch.Identity/"]
COPY ["CandidateBrowserCleanArch.Infrastructure/CandidateBrowserCleanArch.Infrastructure.csproj", "CandidateBrowserCleanArch.Infrastructure/"]
COPY ["CandidateBrowserCleanArch.Persistence/CandidateBrowserCleanArch.Persistence.csproj", "CandidateBrowserCleanArch.Persistence/"]
RUN dotnet restore "CandidateBrowserCleanArch.API/CandidateBrowserCleanArch.API.csproj"
COPY . .
WORKDIR "/src/CandidateBrowserCleanArch.API"
RUN dotnet build "CandidateBrowserCleanArch.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CandidateBrowserCleanArch.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CandidateBrowserCleanArch.API.dll"]