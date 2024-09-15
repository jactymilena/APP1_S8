#!/bin/sh


CERT_DIR=/etc/ssl/certs
CERT_KEY=$CERT_DIR/server.key
CERT_CRT=$CERT_DIR/server.crt

mkdir -p $CERT_DIR

if [ ! -f "$CERT_KEY" ] || [ ! -f "$CERT_CRT" ]; then
    openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout "$CERT_KEY" \
    -out "$CERT_CRT" \
    -subj "/C=US/ST=State/L=City/O=Organization/OU=Department/CN=localhost"

    openssl pkcs12 -export -out /etc/ssl/certs/server.pfx -inkey /etc/ssl/certs/server.key -in /etc/ssl/certs/server.crt -password pass:toto
fi