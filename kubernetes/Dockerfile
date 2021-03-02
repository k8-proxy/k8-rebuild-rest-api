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
RUN dotnet build "Glasswall.CloudSdk.AWS.Rebuild.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Glasswall.CloudSdk.AWS.Rebuild.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

FROM base AS ms-package-signing
RUN  apt-get update \
  && apt-get install -y wget \
  && rm -rf /var/lib/apt/lists/*
  
RUN wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
RUN dpkg -i packages-microsoft-prod.deb

FROM ms-package-signing AS dotnet-sdk 
RUN apt-get install -y apt-transport-https && \
  apt-get update && \
  apt-get install -y dotnet-sdk-3.1
  
FROM dotnet-sdk AS dotnet-package-restore
COPY ./icap/cloud-proxy-app/cloud-proxy-app.sln /src/cloud-proxy-app/cloud-proxy-app.sln 
COPY ./icap/cloud-proxy-app/source/cloud-proxy-app.csproj /src/cloud-proxy-app/source/cloud-proxy-app.csproj
COPY ./icap/cloud-proxy-app/test/cloud-proxy-app.test.csproj /src/cloud-proxy-app/test/cloud-proxy-app.test.csproj
RUN dotnet restore /src/cloud-proxy-app/cloud-proxy-app.sln 

FROM dotnet-package-restore AS dotnet-builder
COPY ./icap/cloud-proxy-app /src/cloud-proxy-app
RUN dotnet publish -c Release /src/cloud-proxy-app/cloud-proxy-app.sln 

FROM ms-package-signing AS dotnet-runtime
RUN apt-get install -y apt-transport-https && \
  apt-get update && \
  apt-get install -y dotnet-runtime-3.1
    
FROM dotnet-runtime
COPY --from=dotnet-builder /src/cloud-proxy-app/source/bin/Release/netcoreapp3.1/publish /usr/local/bin

ENTRYPOINT ["dotnet", "Glasswall.CloudSdk.AWS.Rebuild.dll"]