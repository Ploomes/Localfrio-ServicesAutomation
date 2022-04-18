FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-stage
WORKDIR /src
ARG MSBUILD_CONFIGURATION
ENV ENV_MSBUILD_CONFIGURATION=$MSBUILD_CONFIGURATION
COPY . .
RUN dotnet build
RUN dotnet publish -c ${ENV_MSBUILD_CONFIGURATION} -o /publish



FROM mcr.microsoft.com/dotnet/aspnet:6.0 as serve-stage
WORKDIR /app
COPY startup.sh .
COPY --from=build-stage /publish/ .
ARG BUILD_TIMESTAMP
ARG GIT_COMMIT
ENV ASPNETCORE_URLS "http://+:80"
ENV BUILD_TIMESTAMP=$BUILD_TIMESTAMP
ENV GIT_COMMIT=$GIT_COMMIT
ENV TZ=America/Sao_Paulo

#begin-links:serve
#end-links:serve

RUN apt-get update && apt-get install -y dos2unix && dos2unix startup.sh
<@ DEBUG_ONLY_BEGIN @>
    RUN apt-get update && apt-get install -y vim ssh
    RUN echo "root:debug" | chpasswd && echo "PermitRootLogin yes" >> /etc/ssh/sshd_config
    ENTRYPOINT service ssh restart && bash startup.sh && dotnet "ServicesAutomation.dll"
<@ DEBUG_ONLY_END @>

<@ RELEASE_ONLY_BEGIN @>
    ENTRYPOINT bash startup.sh && dotnet "ServicesAutomation.dll"
<@ RELEASE_ONLY_END @>
