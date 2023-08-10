FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG PROJECT_NAME

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["$PROJECT_NAME/$PROJECT_NAME.csproj", "$PROJECT_NAME/"]
RUN dotnet restore "$PROJECT_NAME/$PROJECT_NAME.csproj"
COPY . .
WORKDIR "/src/$PROJECT_NAME"
RUN dotnet build "$PROJECT_NAME.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "$PROJECT_NAME.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "$PROJECT_NAME.dll"]
