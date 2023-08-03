FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebDisassembler.Analyzer.Service/WebDisassembler.Analyzer.Service.csproj", "WebDisassembler.Analyzer.Service/"]
RUN dotnet restore "WebDisassembler.Analyzer.Service/WebDisassembler.Analyzer.Service.csproj"
COPY . .
WORKDIR "/src/WebDisassembler.Analyzer.Service"
RUN dotnet build "WebDisassembler.Analyzer.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebDisassembler.Analyzer.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebDisassembler.Analyzer.Service.dll"]
