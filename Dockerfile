#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./src/sb_accounts.csproj", "src/"]
#COPY ["./src/bin/Debug/net5.0/appsettings.Production.json", "."]
RUN dotnet restore "./src/sb_accounts.csproj"
COPY . .
WORKDIR "/src/src"
RUN dotnet build "sb_accounts.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "sb_accounts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sb_accounts.dll"]