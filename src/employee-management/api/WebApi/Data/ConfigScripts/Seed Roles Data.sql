DECLARE @tempRolesTable AS TABLE
(
	[Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL
)

INSERT @tempRolesTable (Id, [Name]) VALUES(N''d04efc60-aa14-4f93-8b19-c83da4d349b8'', N''Consultant Level 1'')
INSERT @tempRolesTable (Id, [Name]) VALUES(N''0018107b-e3f3-4235-9351-ba1578d1d1d1'', N''Consultant Level 2'')

--SET IDENTITY_INSERT dbo.Roles ON
MERGE INTO dbo.Roles AS TargetTable
USING @tempRolesTable AS SourceTable
ON TargetTable.Id = SourceTable.Id
WHEN MATCHED THEN
    UPDATE SET
        TargetTable.[Name] = SourceTable.[Name]
WHEN NOT MATCHED THEN 
    INSERT (Id, [Name])
    VALUES (SourceTable.Id, SourceTable.[Name]);

--SET IDENTITY_INSERT dbo.Roles OFF