CREATE DATABASE projectAMJS

USE projectAMJS

CREATE TABLE MsUser
(
    UserID INT PRIMARY KEY IDENTITY(1, 1),
    Username VARCHAR(255),
    UserPassword VARCHAR(10),
    UserRoles VARCHAR(255) CHECK(UserRoles = 'Admin' OR UserRoles = 'User'),
    UserPhoto VARCHAR (255),
    UserStatus VARCHAR (255)
)

CREATE TABLE MsProject
(
    ProjectID INT IDENTITY(1, 1) PRIMARY KEY,
    ProjectName VARCHAR(255),
    ProjectDescription VARCHAR(255)
)

CREATE TABLE HeaderProject
(
    HeaderProjectID INT PRIMARY KEY IDENTITY(1, 1),
    UserID INT FOREIGN KEY REFERENCES MsUser(UserID),
    ProjectID INT FOREIGN KEY REFERENCES MsProject(ProjectID)
)

GO
ALTER PROC sp_getUserAuth
    @Username VARCHAR(255),
    @Password VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM MsUser
    WHERE Username = @Username
        AND UserPassword = @Password

    SELECT 1 AS [Success]
END

GO
CREATE PROC sp_getOneUserAuth
    @Username VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM MsUser
    WHERE Username = @Username

    SELECT 1 AS [Success]
END


INSERT INTO MsUser
VALUES
    (
        'Putud',
        'HelloWorld',
        'Admin'
)

INSERT INTO MsUser
VALUES
    (
        'Adit',
        'tesr',
        'User'
)

INSERT INTO MsUser
VALUES
    (
        'Jhon',
        'nothingwei',
        'User',
        'jhon.png',
        'Active'
)

INSERT INTO MsProject
VALUES
    (
        'Binus MVC',
        'Nothing'
)




EXEC sp_getUserAuth 'Putud', 'HelloWorld'



SELECT @@SERVERNAME

SELECT *
FROM HeaderProject



CREATE TABLE WorkItems
(
    WorkItemID INT IDENTITY(1, 1) PRIMARY KEY,
    ProjectID INT FOREIGN KEY REFERENCES MsProject(ProjectID),
    WorkItemName VARCHAR(255),
    WorkItemState VARCHAR(255),
    WorkItemStartDate DATE,
    WorkItemEndDate DATE
)

CREATE TABLE WorkItemTask
(
    ItemTaskID INT IDENTITY(1, 1) PRIMARY KEY,
    ItemTaskUserID INT FOREIGN KEY REFERENCES MsUser
(UserID),
    ItemWorkID INT FOREIGN KEY REFERENCES WorkItems(WorkItemID),
    ItemTaskName VARCHAR(255),
    ItemTaskState VARCHAR(255)

)

INSERT INTO WorkItems
VALUES (
    1,
    'Sprint 1',
    'To Do',
    '2020-01-11',
    '2020-01-14'
)

INSERT INTO WorkItems
VALUES
    (
        1,
        'Sprint 2',
        'To Do',
        '2020-01-24',
        '2020-01-26'
)

SELECT *
FROM WorkItems
