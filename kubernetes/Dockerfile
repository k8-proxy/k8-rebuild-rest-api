#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV UseFileProxy=1

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Source/Service/Glasswall.CloudSdk.AWS.Rebuild.csproj", "Source/Service/"]
COPY ["Source/Common/Glasswall.CloudSdk.AWS.Common/Glasswall.CloudSdk.AWS.Common.csproj", "Source/Common/Glasswall.CloudSdk.AWS.Common/"]
COPY ["Source/Common/Glasswall.CloudSdk.Common/Glasswall.CloudSdk.Common.csproj", "Source/Common/Glasswall.CloudSdk.Common/"]
COPY ["Source/Common/Glasswall.Core.Engine.Common/Glasswall.Core.Engine.Common.csproj", "Source/Common/Glasswall.Core.Engine.Common/"]
COPY ["Source/Common/Glasswall.Core.Engine.Messaging/Glasswall.Core.Engine.Messaging.csproj", "Source/Common/Glasswall.Core.Engine.Messaging/"]
COPY ["Source/Common/Glasswall.Core.Engine/Glasswall.Core.Engine.csproj", "Source/Common/Glasswall.Core.Engine/"]
RUN dotnet restore "Source/Service/Glasswall.CloudSdk.AWS.Rebuild.csproj"
COPY . .
RUN git submodule update --init --recursive || true
WORKDIR "/src/Source/Service"

FROM build AS publish
RUN dotnet publish "Glasswall.CloudSdk.AWS.Rebuild.csproj" -c Release -o /app/publish

WORKDIR /proxy-src
COPY ["icap/cloud-proxy-app/source/cloud-proxy-app.csproj", "icap/cloud-proxy-app/source/"]
RUN dotnet restore "icap/cloud-proxy-app/source/cloud-proxy-app.csproj"
COPY . .
WORKDIR "/proxy-src/icap/cloud-proxy-app/source"
RUN dotnet build "cloud-proxy-app.csproj" -c Release -o /proxy/build


FROM base AS final

RUN mkdir /var/temp
RUN mkdir /var/testdata
COPY kubernetes/testdata/* /var/testdata

WORKDIR /proxy
COPY --from=publish /proxy/build .
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Glasswall.CloudSdk.AWS.Rebuild.dll"]