FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SendEmailAplication.csproj", "./"]
RUN dotnet restore "SendEmailAplication.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SendEmailAplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SendEmailAplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SendEmailAplication.dll"]
