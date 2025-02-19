FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app    
EXPOSE 80

FROM  mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src 
COPY ["Presentation/Presentation.csproj", "./Presentation/"]
COPY ["Entities/Entities.csproj", "./Entities/"]
COPY ["Persistence/Persistence.csproj", "./Persistence/"]
RUN dotnet restore "Presentation/Presentation.csproj"


COPY . .
WORKDIR "/src/."
RUN dotnet build "Presentation/Presentation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Presentation.dll"]






