FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV LANG C.UTF-8
WORKDIR /app
EXPOSE 8080
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PaymentJournal.Web.csproj", "."]
RUN dotnet restore "PaymentJournal.Web.csproj"
COPY . .
RUN dotnet build "PaymentJournal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentJournal.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentJournal.Web.dll"]