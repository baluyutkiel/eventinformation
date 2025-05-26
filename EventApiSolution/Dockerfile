# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY EventApi/*.csproj ./EventApi/
COPY EventApi.Tests/*.csproj ./EventApi.Tests/

RUN dotnet restore

COPY EventApi ./EventApi/
COPY EventApi.Tests ./EventApi.Tests/
COPY Database ./Database/

WORKDIR /src/EventApi
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
COPY --from=build /src/Database/skillsAssessmentEvents.db ./Database/skillsAssessmentEvents.db

EXPOSE 80
ENTRYPOINT ["dotnet", "EventApi.dll"]