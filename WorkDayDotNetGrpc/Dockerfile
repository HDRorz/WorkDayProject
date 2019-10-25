FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["WorkDayDotNetGrpc/WorkDayDotNetGrpc.csproj", "WorkDayDotNetGrpc/"]
RUN dotnet restore "WorkDayDotNetGrpc/WorkDayDotNetGrpc.csproj"
COPY . .
WORKDIR "/src/WorkDayDotNetGrpc"
RUN dotnet build "WorkDayDotNetGrpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkDayDotNetGrpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkDayDotNetGrpc.dll"]