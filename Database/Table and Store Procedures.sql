-------------------------------Creating Hospital Table------------------------------------------------------

Create table Hospital(
	HospitalId int identity(1000,1) primary key not null,
	HospitalName varchar(50) not null,
	HospitalSpeciality varchar(50) not null,
	HospitalAddress varchar(50) not null,
	HospialCity varchar(50) not null,
	HospitalContactNumber varchar(50) not null
)
Insert into Hospital values
	('Apex','Dental','Old market','Bhopal','0755-2368440')

SELECT * FROM Hospital

--------------------------------Creating Procedure Add-Hospital----------------------------------------------

Create procedure SP_Add_Hospital(
	@HospitalName varchar(50),
	@HospitalSpeciality varchar(50),
	@HospitalAddress varchar(50),
	@HospialCity varchar(50),
	@HospitalContactNumber varchar(50)
)
AS
BEGIN
  INSERT INTO Hospital(HospitalName,HospitalSpeciality,HospitalAddress,HospialCity,HospitalContactNumber)
  VALUES (@HospitalName,@HospitalSpeciality,@HospitalAddress,@HospialCity,@HospitalContactNumber)
END

EXEC SP_Add_Hospital 'Bansal Hospital','Multi-Specialist','Sahapura','Bhopal','0755-2325490'
SELECT * FROM Hospital

--------------------------------Creating Procedure Update-Hospital----------------------------------------------

Create procedure SP_Update_Hospital(
	@HospitalId int,
	@HospitalName varchar(50),
	@HospitalSpeciality varchar(50),
	@HospitalAddress varchar(50),
	@HospialCity varchar(50),
	@HospitalContactNumber varchar(50)
)
AS
Update Hospital set 
	HospitalName=@HospitalName,
	HospitalSpeciality=@HospitalSpeciality,
	HospitalAddress=@HospitalAddress,
	HospialCity=@HospialCity,
	HospitalContactNumber=@HospitalContactNumber
Where HospitalId = @HospitalId
GO

exec SP_Update_Hospital 1000,'Apex Hospital','Dental','Old-market','Bhopal','0755-2368440'
SELECT * FROM Hospital

--------------------------------Creating Procedure Delete-Hospital----------------------------------------------

Create procedure SP_Delete_Hospital(
	@HospitalId int
)
AS
	Delete from Hospital Where HospitalId=@HospitalId
GO

--------------------------------Creating Procedure Get All-Hospital----------------------------------------------

Create procedure SP_Get_All_Hospital
AS
	SELECT * FROM Hospital
GO

--------------------------------Creating Procedure Get Particular-Hospital----------------------------------------------

Create procedure SP_Get_Hospital_By_Id(
	@HospitalId int
)
AS
	SELECT * FROM Hospital Where @HospitalId=HospitalId
GO

-------------------------------------------------------------------------------------------------------------------------


