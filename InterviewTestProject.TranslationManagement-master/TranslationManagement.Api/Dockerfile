#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7729

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TranslationManagement.Api/TranslationManagement.Api.csproj", "TranslationManagement.Api/"]
RUN dotnet restore "TranslationManagement.Api/TranslationManagement.Api.csproj"
COPY . .
WORKDIR "/src/TranslationManagement.Api"
RUN dotnet build "TranslationManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TranslationManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TranslationManagement.Api.dll"]