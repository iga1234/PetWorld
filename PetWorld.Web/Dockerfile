FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetWorld.Web/PetWorld.Web.csproj", "PetWorld.Web/"]
COPY ["PetWorld.Application/PetWorld.Application.csproj", "PetWorld.Application/"]
COPY ["PetWorld.Infrastructure/PetWorld.Infrastructure.csproj", "PetWorld.Infrastructure/"]
COPY ["PetWorld.Domain/PetWorld.Domain.csproj", "PetWorld.Domain/"]
RUN dotnet restore "PetWorld.Web/PetWorld.Web.csproj"
COPY . .
WORKDIR "/src/PetWorld.Web"
RUN dotnet build "PetWorld.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetWorld.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetWorld.Web.dll"]
