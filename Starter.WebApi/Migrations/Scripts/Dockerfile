FROM mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04

# Set environment variables
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=B1q22MPXUgosXiqZ
ENV MSSQL_PID=Express

# Create a config directory
USER root
RUN mkdir -p /usr/src/app
RUN chown mssql /usr/src/app

# Copy the initialization script to the container
COPY *.sql /usr/src/app/
COPY *.sh /usr/src/app/
RUN chmod +x /usr/src/app/DbBuilder.sh
USER mssql

# Expose the SQL Server port
EXPOSE 1433

# Run the SQL Server process and the initialization script
CMD /bin/bash /usr/src/app/DbBuilder.sh & /opt/mssql/bin/sqlservr