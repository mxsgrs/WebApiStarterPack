# Use the official Microsoft SQL Server image as a base
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=MatrixReloaded!
ENV MSSQL_PID=Express

# Copy the migration script and entrypoint script to the container
COPY ./scripts/ /scripts/

# Make the scripts executable
RUN chmod +x /scripts/*.sh

# Set the entrypoint to the script that initializes the database
ENTRYPOINT ["/bin/bash", "/scripts/entrypoint.sh"]