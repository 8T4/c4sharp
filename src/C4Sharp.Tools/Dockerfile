FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/C4Sharp.Tools/C4Sharp.Tools.csproj", "C4Sharp.Tools/"]
RUN dotnet restore "src/C4Sharp.Tools/C4Sharp.Tools.csproj"
COPY . .
WORKDIR "/src/C4Sharp.Tools"
RUN dotnet build "C4Sharp.Tools.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "C4Sharp.Tools.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "C4Sharp.Tools.dll"]
