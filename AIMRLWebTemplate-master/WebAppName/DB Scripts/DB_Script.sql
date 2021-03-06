

/****** Object:  UserDefinedTableType [dbo].[ArrayInt]    Script Date: 9/26/2017 9:11:18 PM ******/
CREATE TYPE [dbo].[ArrayInt] AS TABLE(
	[ID] [int] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[EnableDisablePermission]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EnableDisablePermission]
    @PermissionId int,
	@IsActive bit,
	@ActivityTime datetime,
	@ActivityBy int
AS
BEGIN
	
	UPDATE dbo.Permissions SET IsActive = @IsActive, ModifiedOn = @ActivityTime, Modifiedby = @ActivityBy
	Where ID = @PermissionId
	
	Select @PermissionId
END
GO
/****** Object:  StoredProcedure [dbo].[EnableDisableRole]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EnableDisableRole]
    @RoleId int,
	@IsActive bit,
	@ActivityTime datetime,
	@ActivityBy int
AS
BEGIN
	
	UPDATE dbo.Roles SET IsActive = @IsActive, ModifiedOn = @ActivityTime, Modifiedby = @ActivityBy
	Where ID = @RoleId
	
	Select @RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[EnableDisableUser]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EnableDisableUser]
    @UserId int,
	@IsActive bit,
	@ActivityTime datetime,
	@ActivityBy int
AS
BEGIN
	
	UPDATE dbo.Users SET IsActive = @IsActive, ModifiedOn = @ActivityTime, Modifiedby = @ActivityBy
	Where UserID = @UserId
	
	Select @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[Find_Text_In_SP]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Find_Text_In_SP]
@StringToSearch varchar(100)
AS
BEGIN
SET @StringToSearch = '%' +@StringToSearch + '%'
SELECT Distinct SO.Name
FROM sysobjects SO (NOLOCK)
INNER JOIN syscomments SC (NOLOCK) on SO.Id = SC.ID
AND SO.Type = 'P'
AND SC.Text LIKE @stringtosearch
ORDER BY SO.Name

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPermissions]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetAllPermissions]
AS 
BEGIN
		-- User Permissions
		Select distinct p.* from dbo.Permissions p
END

GO
/****** Object:  StoredProcedure [dbo].[GetEmailRequestsByUniqueID]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetEmailRequestsByUniqueID]
	@UniqueID varchar(30)
AS 
BEGIN
	
	Select * from dbo.EmailRequests
	Where UniqueID = @UniqueID

END

GO
/****** Object:  StoredProcedure [dbo].[GetRolePermissionById]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetRolePermissionById]
	@UserId int
AS 
BEGIN
		-- User Roles
		Select distinct r.* from dbo.Roles r 
		INNER JOIN dbo.UserRoles ur on r.ID = ur.RoleId 
		and ur.UserId = @UserId

		-- User Permissions
		Select distinct p.*,pm.RoleId from dbo.Permissions p 
		INNER JOIN [dbo].[PermissionsMapping] pm on p.Id = pm.PermissionId
		INNER JOIN dbo.UserRoles ur on pm.RoleId = ur.RoleId and ur.UserId = @UserId

END

GO
/****** Object:  StoredProcedure [dbo].[SavePermission]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SavePermission]
	@Id int,
	@Name varchar(50),
	@Description varchar(50),
	@ActivityTime datetime,
	@ActivityBy int
AS
BEGIN
	if (@Id > 0)
	BEGIN
		Update dbo.Permissions
			SET 
			Name = @Name, 
			Description = @Description,
			ModifiedOn = @ActivityTime,
			Modifiedby = @ActivityBy
			where Id=@Id
	END
	ELSE
	BEGIN
		
		INSERT INTO dbo.Permissions(Name ,Description,CreatedOn,CreatedBy,IsActive)
		VALUES( @Name ,@Description,@ActivityTime,@ActivityBy,1)
		
		Select @Id = SCOPE_IDENTITY()
	END

	Select @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SaveRolePermissionMapping]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SaveRolePermissionMapping]
@pRoleID int,
@pList ArrayInt READONLY --Here ArrayInt is user defined type
AS
BEGIN

	--Declare @pRoleID int = 2
	--Declare @pList ArrayInt
	--insert into @pList Select 1
	--insert into @pList Select 3

	Delete from [dbo].[PermissionsMapping] Where RoleId = @pRoleID and PermissionId NOT IN (select ID from @pList)

	Insert into [dbo].[PermissionsMapping](RoleId,PermissionId)
	select @pRoleID, ID from @pList 
	where ID not IN (select PermissionID from [dbo].[PermissionsMapping] Where RoleId = @pRoleID)

	Select @pRoleID

END


GO

CREATE Procedure [dbo].[SaveUserRoleMapping]
@pUserID int,
@pList ArrayInt READONLY --Here ArrayInt is user defined type
AS
BEGIN

	--Declare @pUserID int = 2
	--Declare @pList ArrayInt
	--insert into @pList Select 1
	--insert into @pList Select 3

	Delete from [dbo].[UserRoles] Where RoleId = @pUserID and RoleId NOT IN (select ID from @pList)

	Insert into [dbo].[UserRoles](UserId,RoleId)
	select @pUserID, ID from @pList 
	where ID not IN (select RoleId from [dbo].[UserRoles] Where RoleId = @pUserID)

	Select @pUserID

END


GO
/****** Object:  StoredProcedure [dbo].[SaveRoles]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveRoles]
	@RoleId int,
	@Name varchar(50),
	@Description varchar(50),
	@ActivityTime datetime,
	@ActivityBy int
AS
BEGIN
	
	if (@RoleId > 0)
	BEGIN
		Update dbo.Roles
			SET 
			Name = @Name, 
			Description = @Description,
			ModifiedBy=@ActivityBy,
			ModifiedOn=@ActivityTime
		WHERE Id = @RoleId

	END
	ELSE
	BEGIN
		
		INSERT INTO dbo.Roles(Name ,Description,CreatedBy,CreatedOn,IsActive)
		VALUES( @Name ,@Description,@ActivityBy,@ActivityTime,1)
		
		Select @RoleId = SCOPE_IDENTITY()
	END

	Select @RoleId
END
GO
/****** Object:  StoredProcedure [dbo].[SaveUsers]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveUsers]
		@UserId int,
	   @Login varchar(50)
	  ,@Password varchar(50)
      ,@Name varchar(100)
      ,@Email varchar(100),
	  @ActivityTime datetime,
	  @ActivityBy int
AS
BEGIN
	
	if (@UserId > 0)
	BEGIN

		Update dbo.Users
			SET 
			Login = @Login, 
			Password = @Password, 
			Name = @Name, 
			Email=@Email, 
			ModifiedOn = @ActivityTime,
			Modifiedby = @ActivityBy
		
		WHERE UserId = @UserId

	END
	ELSE
	BEGIN
		
		INSERT INTO dbo.Users(Login , Password , Name ,Email, CreatedOn,CreatedBy,IsActive)
		VALUES(@Login , @Password , @Name ,@Email,@ActivityTime,@ActivityBy,1)
		
		Select @UserId = SCOPE_IDENTITY()
	END

	Select @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[SearchUserForAutoComplete]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[SearchUserForAutoComplete]
@key varchar(20)
As 
Begin
	
	Select UserId, Login, Name
	from dbo.Users
	where Login like '%' +@key+ '%' 
	OR Name like '%' +@key+ '%' 
	And IsActive = 1
End
GO
/****** Object:  StoredProcedure [dbo].[SearchUsers]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SearchUsers]
	@name varchar(50),
	@email varchar(100)
AS 
BEGIN
	SELECT * from [dbo].[Users] rmd 
	where rmd.Name like @name OR rmd.Email like @email
	And IsActive = 1
END
GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ValidateUser]
	@Login varchar(50),
	@Password varchar(50),
	@CurrTime datetime,
	@MachineIP varchar(20),
	@IgnorePassword bit,
	@LoggerLoginID varchar(50)
AS 
BEGIN

	--DECLARE @Login varchar(50) = ''
	--DECLARE @Password varchar(50) = '123'
	--DECLARE @CurrTime datetime = getdate()
	--DECLARE @MachineIP varchar(20) = ''
	--DECLARE @IgnorePassword bit = 0
	--DECLARE @LoggerLoginID varchar(50) = ''

	Declare @UserId int = 0
	Declare @isActive bit =0

	if(@IgnorePassword = 0)
	BEGIN
		SELECT @UserId=UserId, @isActive = IsActive  
		from dbo.Users u where Login = @Login and Password = @Password 
	END
	else
	BEGIN
		SELECT @UserId=UserId, @isActive = IsActive  
		from dbo.Users u where Login = @Login  
	END

	Select * from dbo.Users where UserID = @UserId

	if @UserId > 0  AND @isActive = 1
	BEGIN
		
		Select distinct r.* from dbo.Roles r 
		INNER JOIN dbo.UserRoles ur on r.ID = ur.RoleId and ur.UserId = @UserId
		Where r.IsActive = 1

		Select distinct p.*,pm.RoleId from dbo.Permissions p 
		INNER JOIN [dbo].[PermissionsMapping] pm on p.Id = pm.PermissionId
		INNER JOIN dbo.UserRoles ur on pm.RoleId = ur.RoleId and ur.UserId = @UserId
		Where p.IsActive = 1
		
		IF @LoggerLoginID != ''
			SET @Login = @Login + '_By_' + @LoggerLoginID

		INSERT INTO dbo.LoginHistory(UserID, LoginID, MachineIP, LoginTime)
		Select @UserId,@Login,@MachineIP,@CurrTime

	END
END
GO
/****** Object:  Table [dbo].[EmailRequests]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailRequests](
	[EmailRequestID] [bigint] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](150) NOT NULL,
	[MessageBody] [varchar](500) NOT NULL,
	[MessageParameters] [varchar](500) NULL,
	[EmailTo] [varchar](200) NOT NULL,
	[EmailCC] [varchar](200) NULL,
	[EmailBCC] [varchar](200) NULL,
	[EmailTemplate] [varchar](50) NULL,
	[ScheduleType] [int] NOT NULL,
	[ScheduleTime] [datetime] NULL,
	[EmailRequestStatus] [int] NOT NULL,
	[EntryTime] [datetime] NULL,
	[UniqueID] [varchar](30) NULL,
 CONSTRAINT [PK_EmailRequests] PRIMARY KEY CLUSTERED 
(
	[EmailRequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginHistoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LoginID] [varchar](50) NOT NULL,
	[MachineIP] [varchar](20) NOT NULL,
	[LoginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Modifiedby] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PermissionsMapping]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionsMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionsMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Modifiedby] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/26/2017 9:11:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[Modifiedby] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EmailRequests] ADD  DEFAULT (getutcdate()) FOR [EntryTime]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO


GO

SET IDENTITY_INSERT dbo.users ON

insert into dbo.Users(UserId, Login, Password, Name, Email, isActive)
Select 1,'Admin','123','Admin','abc@yahoo.com',1

SET IDENTITY_INSERT dbo.users OFF


GO

SET IDENTITY_INSERT dbo.Permissions ON

Insert into dbo.Permissions(Id, Name, Description)

Select '1','perCanLoginAsOtherUser','' UNION ALL
Select '2','perManageSecurityUsers','' UNION ALL
Select '3','perManageSecurityRoles','' UNION ALL
Select '4','perManageSecurityPermissions','' UNION ALL
Select '5','perViewLoginHistoryReport',''

SET IDENTITY_INSERT dbo.Permissions OFF

GO

SET IDENTITY_INSERT dbo.Roles ON

INSERT INTO dbo.Roles(Id, Name, Description,CreatedBy,CreatedOn)
Select '1','Admin','System Admin',1,GetUtCDate()

SET IDENTITY_INSERT dbo.Roles OFF

GO


SET IDENTITY_INSERT [dbo].[PermissionsMapping] ON

INSERT INTO [dbo].[PermissionsMapping](Id, RoleId, PermissionId)
Select '1','1','1' UNION ALL
Select '2','1','2' UNION ALL
Select '3','1','3' UNION ALL
Select '4','1','4' UNION ALL
Select '5','1','5' 

SET IDENTITY_INSERT [dbo].[PermissionsMapping] OFF

GO


Insert into [dbo].[UserRoles](UserId, RoleId)
Select '1','1' 



GO

