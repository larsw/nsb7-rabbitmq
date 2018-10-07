FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY source/BlueService/BlueService.csproj source/BlueService/
COPY source/BlueService.Contracts/BlueService.Contracts.csproj source/BlueService.Contracts/
RUN dotnet restore source/BlueService/BlueService.csproj
COPY . .
WORKDIR /src/source/BlueService
RUN dotnet build BlueService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BlueService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlueService.dll"]
