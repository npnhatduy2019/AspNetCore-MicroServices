using docker-compose
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans

# docker application URLs - Local environment (docker container)

- portainer: http://localhost:9000 username admin pass  
- kibana : http//localhost:5601  username elastic pass admin
- rabbitMQ: http//localhost:15672 username guest pass guest

