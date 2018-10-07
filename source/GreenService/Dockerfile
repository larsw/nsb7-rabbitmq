FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY source/GreenService/GreenService.csproj source/GreenService/
COPY source/GreenService.Contracts/GreenService.Contracts.csproj source/GreenService.Contracts/
RUN dotnet restore source/GreenService/GreenService.csproj
COPY . .
WORKDIR /src/source/GreenService
RUN dotnet build GreenService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GreenService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GreenService.dll"]
