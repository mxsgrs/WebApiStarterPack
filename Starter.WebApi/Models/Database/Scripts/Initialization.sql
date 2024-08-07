CREATE DATABASE "AdmxAccount";

\c "AdmxAccount";

ALTER USER postgres WITH REPLICATION;

GRANT ALL PRIVILEGES ON DATABASE "AdmxAccount" TO postgres;

CREATE TABLE "UserCredentials"
(
    "Id" BIGSERIAL PRIMARY KEY NOT NULL,
    "EmailAddress" VARCHAR(255) NOT NULL,
    "HashedPassword" VARCHAR(255),
    "UserRole" VARCHAR(100)
);

INSERT INTO "UserCredentials" ("EmailAddress", "HashedPassword", "UserRole")
VALUES ('robert.durand@gmail.com', '369b62d459de8a74683f87c276ff8a264d6b247add4beaa02a1c7f9f3134f495', 'superadmin');

CREATE TABLE "UserProfile"
(
    "Id" BIGSERIAL PRIMARY KEY NOT NULL,
    "FirstName" VARCHAR(100),
    "LastName" VARCHAR(100),
    "Birthday" DATE,
    "Gender" VARCHAR(20),
    "Position" VARCHAR(255),
    "EmailAddress" VARCHAR(255),
    "PersonalPhone" VARCHAR(100),
    "ProfessionalPhone" VARCHAR(100),
    "PostalAddress" VARCHAR(255),
    "AddressSupplement" VARCHAR(255),
    "City" VARCHAR(100),
    "ZipCode" VARCHAR(20),
    "StateProvince" VARCHAR(100),
    "Country" VARCHAR(100),
    "UserCredentialsId" BIGINT REFERENCES "UserCredentials"("Id") NOT NULL
);

INSERT INTO "UserProfile" ("FirstName", "LastName", "Birthday", "Gender",
    "Position", "EmailAddress", "PersonalPhone", "ProfessionalPhone", "PostalAddress", "AddressSupplement",
    "City", "ZipCode", "StateProvince", "Country", "UserCredentialsId")
VALUES ('Robert', 'Durand', '1989-11-04', 'Male', 'Menuisier', 
    'robert.durand@gmail.com', '+3376856705', NULL, '1153 route de l''aerodrome', NULL, 
    'Avignon', '84140', 'Provence-Alpes-Cotes d''Azur', 'France', 1);

CREATE PUBLICATION "AccountPublications" FOR ALL TABLES;

DROP DATABASE postgres;