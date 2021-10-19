FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build --prod

### STAGE 2: Run ###
FROM nginx:alpine
COPY --from=node /app/dist/FirmyMichalowiceSPA /usr/share/nginx/html