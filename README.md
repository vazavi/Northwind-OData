# Northwind OData

Exploring what it takes to expose a MS-SQL database as an OData Service.

Two parallel implementations use Microsoft.OData and either EF or Dapper.

Note: The Dapper implementation is not as complete as the EF one and serves to demonstrate that while other technologies can be used it takes almost no effort when EF is used.

### Chagelog

- Only the Products table is supported now.
- Added new column named ProductUniqueID of type uniqueidentifier (Required by Dynamics 365 OData Sources).
- Added new columns ReferenceUniqueID and ProductUri to test Dynamics 365 Virtual Entities.