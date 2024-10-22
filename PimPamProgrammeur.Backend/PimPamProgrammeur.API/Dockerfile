#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PimPamProgrammeur.API/PimPamProgrammeur.API.csproj", "PimPamProgrammeur.API/"]
COPY ["PimPamProgrammeur.Model/PimPamProgrammeur.Model.csproj", "PimPamProgrammeur.Model/"]
COPY ["PimPamProgrammeur.Dto/PimPamProgrammeur.Dto.csproj", "PimPamProgrammeur.Dto/"]
COPY ["PimPamProgrammeur.Repository/PimPamProgrammeur.Repository.csproj", "PimPamProgrammeur.Repository/"]
COPY ["PimPamProgrammeur.Data/PimPamProgrammeur.Data.csproj", "PimPamProgrammeur.Data/"]
COPY ["PimPamProgrammeur.Utils/PimPamProgrammeur.Utils.csproj", "PimPamProgrammeur.Utils/"]
RUN dotnet restore "PimPamProgrammeur.API/PimPamProgrammeur.API.csproj"
COPY . .
WORKDIR "/src/PimPamProgrammeur.API"
RUN dotnet build "PimPamProgrammeur.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PimPamProgrammeur.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PimPamProgrammeur.API.dll"]