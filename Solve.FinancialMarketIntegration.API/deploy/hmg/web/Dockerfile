FROM alpine/git AS git-env
WORKDIR /app

RUN git clone -b master --depth 1 https://cddlutkzlywfcooqu3xmprrerc2lagvsjpzbajmjcow46oq7t67a@dev.azure.com/itlabbrasil/Solve.FinancialMarketIntegration.Web/_git/Solve.FinancialMarketIntegration.Web

FROM node:10.15.3-alpine AS build-env
WORKDIR /app

COPY --from=git-env /app/Solve.FinancialMarketIntegration.Web/. ./

RUN apk --no-cache add --virtual native-deps g++ gcc libgcc libstdc++ linux-headers make python
RUN npm install -g @angular/cli
RUN npm install
RUN ng build --output-hashing=all -c staging --aot

FROM nginx
WORKDIR /usr/share/nginx/html
COPY --from=build-env /app/dist/. ./
# RUN sed -i 's/localhost:5000/localhost:4000/g' main.js
RUN sed -i 's/localhost:5000/server300/g' main.*.js
