FROM postgres:16.3

# Set environment variables for PostgreSQL
ENV POSTGRES_PASSWORD=B1q22MPXUgosXiqZ

# Expose the default PostgreSQL port
EXPOSE 5432

# Copy initialization scripts to the Docker container
# These scripts will be run when the container is initialized
COPY ./Initialization.sql /docker-entrypoint-initdb.d/

# Include custom postgresql.conf file and set it as default
COPY ./postgresql.conf /etc/postgresql/postgresql.conf
CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]

# Define a volume to persist database data
VOLUME ["/var/lib/postgresql/data"]

# The official PostgreSQL image already includes the necessary entrypoint and CMD
# so we do not need to specify them here