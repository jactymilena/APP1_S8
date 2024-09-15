FROM alpine:latest

RUN apk add --no-cache openssl

COPY --chmod=755 generate.sh /usr/local/bin/generate.sh

ENTRYPOINT ["/usr/local/bin/generate.sh"]

