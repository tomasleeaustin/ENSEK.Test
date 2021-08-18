-- DROP TABLE customers.account;
CREATE TABLE customers.account (
	id serial NOT NULL,
	first_name varchar NULL,
	last_name varchar NULL,
	CONSTRAINT account_pk PRIMARY KEY (id)
);

-- DROP TABLE customers.meter_reading;
CREATE TABLE customers.meter_reading (
	id serial NOT NULL,
	account_id int4 NOT NULL,
	date_time timestamp NOT NULL,
	value numeric(5) NOT NULL,
	CONSTRAINT meter_reading_pk PRIMARY KEY (id),
	CONSTRAINT meter_reading_fk FOREIGN KEY (account_id) REFERENCES customers.account(id)
);

-- Powerscript Entity Framework scaffolding.
--dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=5432;Database=ensek;User Id=ensek;Password=1234;" Npgsql.EntityFrameworkCore.PostgreSQL -c ENSEKDbContext --context-dir DbContexts -t "customers.account" -t "customers.meter_reading"