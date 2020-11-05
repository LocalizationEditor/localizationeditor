CREATE TABLE AUTH_UserData(
    Id bigint not null primary key identity(1,1),
    Email nvarchar(MAX) not null,
    Password nvarchar(MAX) not null
);