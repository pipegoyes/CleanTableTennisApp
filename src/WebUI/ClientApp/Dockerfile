#FROM node:16
#EXPOSE 4200
#WORKDIR /usr/src/app
#COPY package*.json ./
#RUN npm install
#COPY . .
#RUN npm run build
#ENTRYPOINT ["npm", "run", "start:dev"]


# Stage 1: Build an Angular Docker Image
FROM node:16 as build
WORKDIR /apps
COPY . .
RUN npm install
COPY . /apps
ARG configuration=production
#RUN npm run build ——outputPath=./dist/out —configuration $configuration
RUN npm run build
# Stage 2, use the compiled app, ready for production with Nginx
FROM nginx:alpine
COPY ./nginx-custom.conf /etc/nginx/conf.d/
RUN rm -rf /usr/share/nginx/html/*
#COPY —-from=build /apps/dist/ /usr/share/nginx/html
