FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EventPlanner.WebAPI/EventPlanner.WebAPI.csproj", "EventPlanner.WebAPI/"]
RUN dotnet restore "EventPlanner.WebAPI/EventPlanner.WebAPI.csproj"
COPY . .
WORKDIR "/src/EventPlanner.WebAPI"
RUN dotnet build "EventPlanner.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventPlanner.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventPlanner.WebAPI.dll"]
