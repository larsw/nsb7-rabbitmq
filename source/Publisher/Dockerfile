FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY source/Publisher/Publisher.csproj source/Publisher/
COPY source/Publisher.Contracts/Publisher.Contracts.csproj source/Publisher.Contracts/
RUN dotnet restore source/Publisher/Publisher.csproj
COPY . .
WORKDIR /src/source/Publisher
RUN dotnet build Publisher.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Publisher.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Publisher.dll"]
