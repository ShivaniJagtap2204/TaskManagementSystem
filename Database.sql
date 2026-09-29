create database TaskManagement;

GO

-- 1. Roles
CREATE TABLE Roles
(
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Roles (RoleName) VALUES ('Admin'),('Manager'),('User');


-- 2. Users
CREATE TABLE Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL UNIQUE,
    PasswordHash VARCHAR(500) NOT NULL,
    RoleId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Users_Roles
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

select * from Users



-- 3. Teams
CREATE TABLE Teams
(
    TeamId INT IDENTITY(1,1) PRIMARY KEY,
    TeamName VARCHAR(100) NOT NULL UNIQUE,
    ManagerId INT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Teams_Manager
        FOREIGN KEY (ManagerId) REFERENCES Users(UserId)
);


-- 4. Team Members
CREATE TABLE TeamMembers
(
    TeamMemberId INT IDENTITY(1,1) PRIMARY KEY,
    TeamId INT NOT NULL,
    UserId INT NOT NULL,
    JoinedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_TeamMembers_Teams
        FOREIGN KEY (TeamId) REFERENCES Teams(TeamId),

    CONSTRAINT FK_TeamMembers_Users
        FOREIGN KEY (UserId) REFERENCES Users(UserId),

    CONSTRAINT UQ_TeamMembers
        UNIQUE (TeamId, UserId)
);


-- 5. Tasks
CREATE TABLE Tasks
(
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    Description VARCHAR(MAX) NULL,
    AssignedTo INT NOT NULL,
    AssignedBy INT NOT NULL,
    TeamId INT NULL,
    Priority VARCHAR(20) NOT NULL DEFAULT 'Medium',
    Status VARCHAR(20) NOT NULL DEFAULT 'To Do',
    Deadline DATETIME NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedDate DATETIME NULL,

    CONSTRAINT FK_Tasks_AssignedTo
        FOREIGN KEY (AssignedTo) REFERENCES Users(UserId),

    CONSTRAINT FK_Tasks_AssignedBy
        FOREIGN KEY (AssignedBy) REFERENCES Users(UserId),

    CONSTRAINT FK_Tasks_Team
        FOREIGN KEY (TeamId) REFERENCES Teams(TeamId),

    CONSTRAINT CK_Tasks_Status
        CHECK (Status IN ('To Do', 'In Progress', 'Done')),

    CONSTRAINT CK_Tasks_Priority
        CHECK (Priority IN ('Low', 'Medium', 'High'))
);


-- 6. Comments
CREATE TABLE Comments
(
    CommentId INT IDENTITY(1,1) PRIMARY KEY,
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    CommentText VARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Comments_Tasks
        FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),

    CONSTRAINT FK_Comments_Users
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


-- 7. Notifications
CREATE TABLE Notifications
(
    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TaskId INT NULL,
    NotificationType VARCHAR(50) NOT NULL,
    Message VARCHAR(500) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Notifications_Users
        FOREIGN KEY (UserId) REFERENCES Users(UserId),

    CONSTRAINT FK_Notifications_Tasks
        FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId)
);


