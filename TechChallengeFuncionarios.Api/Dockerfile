FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY TechChallengeFuncionarios.Api.csproj ./
RUN dotnet restore "TechChallengeFuncionarios.Api.csproj"
COPY . ./

RUN apt-get update && apt-get install -y curl
RUN curl -o /wait-for-it.sh https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh
RUN chmod +x /wait-for-it.sh
RUN dotnet publish "TechChallengeFuncionarios.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0  AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /src/TechChallengeFuncionarios.Api.csproj .
COPY --from=build /wait-for-it.sh /wait-for-it.sh
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

EXPOSE 3050
