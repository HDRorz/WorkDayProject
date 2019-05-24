FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["WorkDayAspCore/WorkDayAspCore.csproj", "WorkDayAspCore/"]
RUN dotnet restore "WorkDayAspCore/WorkDayAspCore.csproj"
COPY . .
WORKDIR "/src/WorkDayAspCore"
RUN dotnet build "WorkDayAspCore.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WorkDayAspCore.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WorkDayAspCore.dll"]