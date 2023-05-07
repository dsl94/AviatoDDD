FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.17-arm64v8 AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17-arm64v8 AS build
WORKDIR /src
COPY ["AviatoDDD.Api/AviatoDDD.Api.csproj", "AviatoDDD.Api/"]
COPY ["AviatoDDD.Core/AviatoDDD.Core.csproj", "AviatoDDD.Core/"]
COPY ["AviatoDDD.Infrastructure/AviatoDDD.Infrastructure.csproj", "AviatoDDD.Infrastructure/"]
RUN dotnet restore "AviatoDDD.Api/AviatoDDD.Api.csproj"
COPY . .
WORKDIR "/src/AviatoDDD.Api"
RUN dotnet build "AviatoDDD.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AviatoDDD.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AviatoDDD.Api.dll"]
