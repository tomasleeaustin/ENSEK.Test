# ENSEK.Test

Thank you for the opportunity to take this test, it was a fun project!

I've included the basic database schema in the Database Schema folder. This assumes a database called "ensek" with a user called "ensek" with password 1234 
(as used in the connectionstrings). I was unsure whether you'd want to set this up to run so I will also include the entity framework scaffolding script I used here:

dotnet ef dbcontext scaffold "Server=127.0.0.1;Port=5432;Database=ensek;User Id=ensek;Password=1234;" Npgsql.EntityFrameworkCore.PostgreSQL -c ENSEKDbContext --context-dir DbContexts -t "customers.account" -t "customers.meter_reading"

Note: I moved the generated entities into an Entities folder at the same level as DbContext after they were generated.
