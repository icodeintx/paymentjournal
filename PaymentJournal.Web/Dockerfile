# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Build and publish stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PaymentJournal.Web.csproj", "."]
RUN dotnet restore "PaymentJournal.Web.csproj"

COPY . .
RUN dotnet publish "PaymentJournal.Web.csproj" -c Release -o .\out

# Final runtime stage
FROM base AS final
WORKDIR /app
#COPY --from=build /app/publish .
COPY ./out .
ENTRYPOINT ["dotnet", "PaymentJournal.Web.dll"]