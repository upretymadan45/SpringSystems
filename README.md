# SpringSystems
## This application is based on .NET6 and it requires .NET6 SDK to be installed to run
## Requirements
1. .NET6 SDK
2. SQL Server Database
## To Run the project
1. Update the connection string in appsettings.json/appsettings.development.json
2. Run the sql script below to create the database
3. Publish the project to run it or open the project in visual studio to run and debug

## Sql Scripts
```
Create Database CompanyDb
use companydb
Go
Create Table Company(
Id bigint primary key identity(1,1),
AddedOn datetime not null,
UpdatedOn datetime null,
AddedBy nvarchar(200) not null,
IsActive bit not null,
Name nvarchar(500) not null,
Address nvarchar(500) not null
)

Create Table Employee(
	Id bigint primary key identity(1,1),
	AddedOn datetime not null,
	UpdatedOn datetime null,
	AddedBy nvarchar(200) not null,
	IsActive bit not null,
	FirstName nvarchar(200) not null,
	MiddleName nvarchar(200) null,
	LastName nvarchar(200) not null,
	Address nvarchar(300) not null,
	Email nvarchar(200) not null,
	ContactNo nvarchar(20) not null,
	CompanyId bigint not null foreign key references Company(Id)
)
```
