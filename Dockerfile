FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore SponsorshipWeb.csproj
RUN dotnet publish SponsorshipWeb.csproj -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html

COPY --from=build /app/publish/wwwroot .

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80