FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app 
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Customers.Api ./Customers/Customers.Api/
COPY Customers.Application ./Customers/Customers.Application/
COPY Customers.Domain ./Customers/Customers.Domain/
COPY Customers.Infrastructure ./Customers/Customers.Infrastructure/
COPY Customers.Persistence ./Customers/Customers.Persistence/
COPY SharedKernal ./Customers/SharedKernal/

WORKDIR /src/Customers/Customers.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet","Customers.Api.dll"]
