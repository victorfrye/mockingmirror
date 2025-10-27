FROM node:24-alpine AS base
LABEL com.github.owner="leadingedje"
LABEL com.github.repo="mockingmirror"

ENV APP_UID=1554

RUN addgroup \
    --gid=$APP_UID \
    app \
    && adduser \
    --uid=$APP_UID \
    --ingroup=app \
    --disabled-password \
    app

USER $APP_UID

FROM node:24-alpine AS build
WORKDIR /work

COPY src/WebClient/package*.json ./
RUN npm ci

COPY src/WebClient/ .
RUN npm run build

FROM base AS run
WORKDIR /app
COPY --from=build --chown=$APP_UID:$APP_UID /work/.next/standalone ./
COPY --from=build --chown=$APP_UID:$APP_UID /work/.next/static ./.next/static
COPY --from=build --chown=$APP_UID:$APP_UID /work/public ./public

ENV PORT=3000
ENV HOSTNAME="0.0.0.0"

EXPOSE 3000

CMD ["node", "server.js"]
