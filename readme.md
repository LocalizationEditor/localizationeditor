# Localization editor
Localization editor is a project for managing localization which is stored in relational database.

### Prerequisites
 * [.NET 5.0](https://dot.net/core) 
 * [Node.js](https://nodejs.org/).

To install this example application, run the following commands:

```bash
git clone https://bitbucket.x-plarium.com/scm/pr/rocks-localization-backend.git
cd rocks-localization-backend
```
This will download a copy of the project.
### Start the app

To install all of the dependencies and start the ASP.NET 5.0 Web API, run:
``` sh
cd LocalizationEditor/LocalizationEditor.Web
dotnet run
```
To make it work with your database (currently supported only **MSSQL**) create this tables:

``` sql
CREATE TABLE [dbo].[CORE_Localization_Type] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (300) NOT NULL,
    [LastUpdatedTime] BIGINT         CONSTRAINT [DF__CORE_Loca__LastU__39788055] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CORE_Localization_Type_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CORE_Localization_Type$UX_CORE_Localization_Type_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[CORE_Localization_Strings] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [LocalizationTypeId] BIGINT         NOT NULL,
    [StringKey]          NVARCHAR (300) NOT NULL,
    [TextEn]             NVARCHAR (MAX) NULL,
    [TextRu]             NVARCHAR (MAX) NULL,
    [TextUa]             NVARCHAR (MAX) NULL,
    [TextHe]             NVARCHAR (MAX) NULL,
    [LastUpdatedTime]    BIGINT         CONSTRAINT [DF__CORE_Loca__LastU__38845C1C] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CORE_Localization_Strings_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CORE_Localization_Strings$FK_CORE_Localization_Strings_To_CORE_Localization_Type] FOREIGN KEY ([LocalizationTypeId]) REFERENCES [dbo].[CORE_Localization_Type] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [FK_CORE_Localization_Strings_To_CORE_Localization_Type_idx]
    ON [dbo].[CORE_Localization_Strings]([LocalizationTypeId] ASC);
```
### Publish the app

To publish application, run:
``` sh
cd LocalizationEditor/LocalizationEditor.Web
dotnet publish LocalizationEditor.Web.csproj
```

### Roadmap
- [ ] Add users registration form
- [ ] Add password change and recovery form
- [x] Make sidebar on merge form hideable
- [ ] Make site mobile adaptive
- [ ] Add possibility to change themes
- [ ] Add possibility to configure columns and their names
- [ ] Add possibility to configure table names
- [ ] Implement factory method for MySql


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.