FROM nginx:alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:20-alpine AS build
WORKDIR /src

COPY WebDisassembler.Api/ClientApp "/src"
RUN npm install
COPY . .
WORKDIR "/src"
RUN npm run build

FROM base AS final
COPY --from=build /src/dist ./client
COPY Docker/ReverseProxy/nginx.conf /etc/nginx/nginx.conf
