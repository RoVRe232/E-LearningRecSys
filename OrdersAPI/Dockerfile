#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OrdersAPI/OrdersAPI.Presentation.csproj", "OrdersAPI/"]
COPY ["OrdersAPI.Application/OrdersAPI.Application.csproj", "OrdersAPI.Application/"]
COPY ["OrdersAPI.Domain/OrdersAPI.Domain.csproj", "OrdersAPI.Domain/"]
COPY ["OrdersAPI.Infrastructure/OrdersAPI.Infrastructure.csproj", "OrdersAPI.Infrastructure/"]
RUN dotnet restore "OrdersAPI/OrdersAPI.Presentation.csproj"
COPY . .
WORKDIR "/src/OrdersAPI"
RUN dotnet build "OrdersAPI.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersAPI.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersAPI.Presentation.dll"]
