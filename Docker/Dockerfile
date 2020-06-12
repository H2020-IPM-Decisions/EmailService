FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base

ARG URL_EXPOSE_PORT=80
ARG URL_EXPOSE_PORT_HTTPS=443
ARG BUILDER_VERSION=latest
ARG RELEASE_DATE="2020-04-02"

LABEL h2020.ipmdecisions.eml.version=${BUILDER_VERSION} \
    vendor="RSK ADAS Ltd" \
    h2020.ipmdecisions.eml.release-date=${RELEASE_DATE}


WORKDIR /app
EXPOSE ${URL_EXPOSE_PORT}
EXPOSE ${URL_EXPOSE_PORT_HTTPS}

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["H2020.IPMDecisions.EML.API/H2020.IPMDecisions.EML.API.csproj", "H2020.IPMDecisions.EML.API/"]
RUN dotnet restore "H2020.IPMDecisions.EML.API/H2020.IPMDecisions.EML.API.csproj"

COPY . .
WORKDIR /src/H2020.IPMDecisions.EML.API
RUN dotnet build "H2020.IPMDecisions.EML.API.csproj" -c Release -o /app/build

WORKDIR /src/H2020.IPMDecisions.EML.API
FROM build AS publish
RUN dotnet publish "H2020.IPMDecisions.EML.API.csproj" -c Release -o /app/publish

FROM base AS final
ARG URL_PORT=5506
ARG URL_PORT_HTTPS=443

WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://*:${URL_PORT}
ENV JwtSettings:SecretKey=""
ENV JwtSettings:IssuerServerUrl=""
ENV JwtSettings:ValidAudiences=""
ENV AllowedHosts="*"
ENV EmailSettings:SmtpServer=""
ENV EmailSettings:SmtpPort=""
ENV EmailSettings:SmtpUsername=""
ENV EmailSettings:SmtpPassword=""
ENV EmailSettings:FromAddress=""
ENV EmailSettings:FromName=""
ENV EmailSettings:EnableSsl=""

ENTRYPOINT ["dotnet", "H2020.IPMDecisions.EML.API.dll"]