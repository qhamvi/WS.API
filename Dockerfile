FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WS.API/WS.API.csproj", "WS.API/"]
COPY ["WS.API.DTO/WS.API.DTO.csproj", "WS.API.DTO/"]
COPY ["WS.API.Models/WS.API.Models.csproj", "WS.API.Models/"]
COPY ["WS.API.Service/WS.API.Service.csproj", "WS.API.Service/"]
RUN dotnet restore "WS.API/WS.API.csproj"
COPY . .
WORKDIR "/src/WS.API"
RUN dotnet build "WS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WS.API.dll"]
