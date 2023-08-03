FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebDisassembler.Api/WebDisassembler.Api.csproj", "WebDisassembler.Api/"]
RUN dotnet restore "WebDisassembler.Api/WebDisassembler.Api.csproj"
COPY . .
WORKDIR "/src/WebDisassembler.Api"
RUN dotnet build "WebDisassembler.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebDisassembler.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebDisassembler.Api.dll"]
