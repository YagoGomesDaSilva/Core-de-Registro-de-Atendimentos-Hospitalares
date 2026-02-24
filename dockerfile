FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

COPY init.sql /init.sql

CMD /bin/bash -c "\
/opt/mssql/bin/sqlservr & \
sleep 20 && \
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'Password@123!' -C -i /init.sql && \
wait"