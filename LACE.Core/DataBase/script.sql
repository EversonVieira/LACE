DROP DATABASE IF EXISTS LACE;
CREATE DATABASE IF NOT EXISTS LACE;

USE LACE;

CREATE TABLE AuthUser(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    
    Cpf VARCHAR(20),
    Rg VARCHAR(20),
    
	Name VARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(2048),
    IsActive BIT,
    IsLocked BIT,
    
    CreatedBy VARCHAR(255),
    CreatedOn DATETIME,
    ModifiedBy VARCHAR(255),
    ModifiedOn DATETIME
);

CREATE TABLE AuthSession(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    UserID INT,
    SessionKey VARCHAR(1000),
    LastRenewDate DATETIME
);
CREATE TABLE ExamReport(
	Id INT PRIMARY KEY AUTO_INCREMENT,
    
    UserId INT,
    SourcePatientID VARCHAR(255),
    SourceExamId VARCHAR(255),
    
    PatientCPF VARCHAR(20),
    PatientRG VARCHAR(20),
    
    ExamName VARCHAR(255),
    FileExtension VARCHAR(100),
    FilePath VARCHAR(50000),
    ExamDate DATETIME,
    UploadDate DATETIME,
    
    
	CreatedBy VARCHAR(255),
    CreatedOn DATETIME,
    ModifiedBy VARCHAR(255),
    ModifiedOn DATETIME
);
