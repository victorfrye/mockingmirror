FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
LABEL com.github.owner="victorfrye"
LABEL com.github.repo="mockingmirror"
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /work

COPY VictorFrye.MockingMirror.slnx ./
COPY src/ServiceDefaults/ServiceDefaults.csproj ./src/ServiceDefaults/
COPY src/WebApi/WebApi.csproj ./src/WebApi/
COPY test/WebApi/WebApi.Tests.csproj ./test/WebApi/

RUN dotnet restore

COPY . .
RUN dotnet build -c Release --no-restore

FROM build AS test
RUN dotnet test --no-build

FROM build AS publish
RUN dotnet publish src/WebApi/WebApi.csproj -c Release --no-build -o ./out

FROM base AS run
WORKDIR /app
COPY --from=publish /work/out .

ENTRYPOINT ["dotnet", "VictorFrye.MockingMirror.WebApi.dll"]
