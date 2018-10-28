FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 26282
EXPOSE 44375

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["SSNZ.Steam2019.Service/SSNZ.Steam2019.Service.csproj", "SSNZ.Steam2019.Service/"]
RUN dotnet restore "SSNZ.Steam2019.Service/SSNZ.Steam2019.Service.csproj"
COPY . .
WORKDIR "/src/SSNZ.Steam2019.Service"
RUN dotnet build "SSNZ.Steam2019.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SSNZ.Steam2019.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SSNZ.Steam2019.Service.dll"]