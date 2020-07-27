FROM alpine/git AS git-env
WORKDIR /app

RUN git clone -b master --depth 1 https://cddlutkzlywfcooqu3xmprrerc2lagvsjpzbajmjcow46oq7t67a@dev.azure.com/itlabbrasil/Solve.FinancialMarketIntegration.Web/_git/Solve.FinancialMarketIntegration.API

FROM microsoft/dotnet:2.2-sdk-alpine AS build-env
WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY --from=git-env /app/Solve.FinancialMarketIntegration.API/src/*.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY --from=git-env /app/Solve.FinancialMarketIntegration.API/src/. ./

