# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet tool install -g dotnet-ef

WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY RSSFeeds/*.csproj ./RSSFeeds/
COPY RSSFeeds.Test/*.csproj ./RSSFeeds.Test/
RUN dotnet restore

# copy everything else and build app
COPY RSSFeeds/. ./RSSFeeds/
COPY RSSFeeds.Test/. ./RSSFeeds.Test/
WORKDIR /source/RSSFeeds
#RUN dotnet publish -c release -o /app --no-restore
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 8082 
ENTRYPOINT ["dotnet", "RSSFeeds.dll"]