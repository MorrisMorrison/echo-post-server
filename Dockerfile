FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj" , "."]
RUN dotnet restore "src/WebUI/WebUI.csproj" 
COPY . .
RUN dotnet build "src/WebUI/WebUI.csproj"  -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/WebUI/WebUI.csproj"  -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]