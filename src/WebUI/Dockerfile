#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used for VS debugging on Docker
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# replace shell with bash so we can source files
#RUN rm /bin/sh && ln -s /bin/bash /bin/sh
#
## update the repository sources list
## and install dependencies
#RUN apt-get update \
    #&& apt-get install -y curl \
    #&& apt-get -y autoclean
        #
## nvm environment variables
#ENV NVM_DIR /usr/local/nvm
#ENV NODE_VERSION v16.15.0
#
## install nvm
## https://github.com/creationix/nvm#install-script
#RUN curl --silent -o- https://raw.githubusercontent.com/creationix/nvm/v0.31.2/install.sh | bash
#
## install node and npm
#RUN source $NVM_DIR/nvm.sh \
    #&& nvm install $NODE_VERSION \
    #&& nvm alias default $NODE_VERSION \
    #&& nvm use default
#
## add node and npm to path so the commands are available
#ENV NODE_PATH $NVM_DIR/v$NODE_VERSION/lib/node_modules
#ENV PATH $NVM_DIR/versions/node/v$NODE_VERSION/bin:$PATH


# Initial way to install nodejs
#RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
#RUN apt install -y nodejs

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanTableTennisApp.WebUI.dll"]