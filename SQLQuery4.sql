use QLJob
go
CREATE TABLE dbo.Companies (
    CompanyID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Website NVARCHAR(255) NULL,
    Address NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO


-- ============================
-- 2) Jobs
-- ============================
CREATE TABLE dbo.Jobs (
    JobID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Requirements NVARCHAR(MAX) NULL,
    Location NVARCHAR(255) NULL,
    Industry NVARCHAR(100) NULL,
    SalaryMin INT NULL,
    SalaryMax INT NULL,
    IsPublished BIT DEFAULT 0,      -- 0 = chýa duy?t, 1 = ð? duy?t
    PublishDate DATETIME NULL,
    ExpireDate DATETIME NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
-- FK
ALTER TABLE dbo.Jobs
    ADD CONSTRAINT FK_Jobs_Companies FOREIGN KEY (CompanyID)
    REFERENCES dbo.Companies(CompanyID) ON DELETE CASCADE;
-- Indexes
CREATE INDEX IDX_Jobs_Title ON dbo.Jobs(Title);
CREATE INDEX IDX_Jobs_Industry ON dbo.Jobs(Industry);
CREATE INDEX IDX_Jobs_Company ON dbo.Jobs(CompanyID);
GO

-- ============================
-- 3) Applicants
-- ============================
CREATE TABLE dbo.Applicants (
    ApplicantID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(50) NULL,
    Skills NVARCHAR(MAX) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
-- Unique email constraint (n?u mu?n)
CREATE UNIQUE INDEX UQ_Applicants_Email ON dbo.Applicants(Email);
GO

-- ============================
-- 4) Resumes (CV metadata — NFR: không lýu file trong DB)
-- ============================
CREATE TABLE dbo.Resumes (
    ResumeID INT IDENTITY(1,1) PRIMARY KEY,
    ApplicantID INT NOT NULL,
    OriginalFileName NVARCHAR(255) NOT NULL,
    StorageKey NVARCHAR(255) NOT NULL,   -- ví d?: UUID ho?c key trên S3 (không dùng tên g?c)
    ContentType NVARCHAR(100) NULL,
    FileSize BIGINT NULL,
    UploadedAt DATETIME DEFAULT GETDATE()
);
ALTER TABLE dbo.Resumes
    ADD CONSTRAINT FK_Resumes_Applicants FOREIGN KEY (ApplicantID)
    REFERENCES dbo.Applicants(ApplicantID) ON DELETE CASCADE;
CREATE INDEX IDX_Resumes_Applicant ON dbo.Resumes(ApplicantID);
GO

-- ============================
-- 5) Applications (?ng tuy?n)
-- ============================
CREATE TABLE dbo.Applications (
    ApplicationID INT IDENTITY(1,1) PRIMARY KEY,
    JobID INT NOT NULL,
    ApplicantID INT NOT NULL,
    ResumeID INT NULL,
    CoverLetter NVARCHAR(MAX) NULL,
    Status NVARCHAR(50) DEFAULT N'applied',  -- applied/screening/interviewing/offered/rejected/hired/withdrawn
    AppliedAt DATETIME DEFAULT GETDATE()
);
ALTER TABLE dbo.Applications
    ADD CONSTRAINT FK_Applications_Jobs FOREIGN KEY (JobID)
    REFERENCES dbo.Jobs(JobID) ON DELETE CASCADE;
ALTER TABLE dbo.Applications
    ADD CONSTRAINT FK_Applications_Applicants FOREIGN KEY (ApplicantID)
    REFERENCES dbo.Applicants(ApplicantID) ON DELETE CASCADE;
ALTER TABLE dbo.Applications
    ADD CONSTRAINT FK_Applications_Resumes FOREIGN KEY (ResumeID)
    REFERENCES dbo.Resumes(ResumeID) ON DELETE SET NULL;
CREATE INDEX IDX_Applications_Job ON dbo.Applications(JobID);
CREATE INDEX IDX_Applications_Applicant ON dbo.Applications(ApplicantID);
GO

-- ============================
-- 6) Interviews
-- ============================
CREATE TABLE dbo.Interviews (
    InterviewID INT IDENTITY(1,1) PRIMARY KEY,
    ApplicationID INT NOT NULL,
    ScheduledAt DATETIME NOT NULL,
    DurationMinutes INT NULL,
    Mode NVARCHAR(50) DEFAULT N'online',  -- onsite/online/phone
    Location NVARCHAR(255) NULL,
    Interviewer NVARCHAR(255) NULL,
    Result NVARCHAR(50) DEFAULT N'pending', -- pending/pass/fail/no_show
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
ALTER TABLE dbo.Interviews
    ADD CONSTRAINT FK_Interviews_Applications FOREIGN KEY (ApplicationID)
    REFERENCES dbo.Applications(ApplicationID) ON DELETE CASCADE;
CREATE INDEX IDX_Interviews_ScheduledAt ON dbo.Interviews(ScheduledAt);
GO

-- ============================
-- 7) Notifications (b?ng trung gian ð? worker g?i email)
-- ============================
CREATE TABLE dbo.Notifications (
    NotificationID INT IDENTITY(1,1) PRIMARY KEY,
    UserEmail NVARCHAR(255) NULL,
    Type NVARCHAR(20) DEFAULT N'email', -- email/in_app/sms
    Subject NVARCHAR(255) NULL,
    Body NVARCHAR(MAX) NULL,
    Status NVARCHAR(20) DEFAULT N'pending', -- pending/sent/failed
    Attempts INT DEFAULT 0,
    LastAttemptAt DATETIME NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
CREATE INDEX IDX_Notifications_Status ON dbo.Notifications(Status);
GO