FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . ./
RUN dotnet restore "src/WebUI/WebUI.csproj" 
RUN dotnet build "src/WebUI/WebUI.csproj"  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/WebUI/WebUI.csproj"  -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
