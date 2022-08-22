go
create DATABASE hospital
go
go
use hospital
go
CREATE TABLE Employe (
    EmployeID int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(255),
    LastName varchar(255),
	mobile nvarchar(50) UNIQUE 
);
CREATE TABLE EmployeDetail (
    EmployeDetailID int IDENTITY(1,1) PRIMARY KEY,
    Address varchar(255),
    City varchar(255),
	salary Int,
	Ken DATETIME,
	Aprove nvarchar(10),
	EmployeID int FOREIGN KEY REFERENCES Employe(EmployeID)
);

CREATE TABLE EmployeUser (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    UserName nvarchar(255),
    Password nvarchar(50),
	Spaciality nvarchar(100),
	EmployeID int FOREIGN KEY REFERENCES Employe(EmployeID)
);
CREATE TABLE patient (
    PatientID int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(255),
    LastName varchar(255),
	mobile nvarchar(50),
	EmployeID int FOREIGN KEY REFERENCES Employe(EmployeID)
);
CREATE TABLE VitalSign (
    VitalID int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(255),
	BloodPressure nvarchar(50),
	Temprature nvarchar(50),
	Symptom nvarchar(2000),
	PatientID int FOREIGN KEY REFERENCES patient(PatientID)
);
CREATE TABLE Lab (
    LabID int IDENTITY(1,1) PRIMARY KEY,
    LabName nvarchar(1000),

);
CREATE TABLE PatientRecord (
    RecID int IDENTITY(1,1) PRIMARY KEY,
	Details nvarchar(2000),
	PatientID int FOREIGN KEY REFERENCES patient(PatientID),
	LabID int FOREIGN KEY REFERENCES Lab(LabID)
);
CREATE TABLE LabDetail (
    LabDID int IDENTITY(1,1) PRIMARY KEY,
	Prize int,
	LabID int FOREIGN KEY REFERENCES Lab(LabID)
);

CREATE TABLE Drug (
    DrugID int IDENTITY(1,1) PRIMARY KEY,
    DrugName nvarchar(1000),
	PatientID int FOREIGN KEY REFERENCES patient(PatientID)
);
CREATE TABLE DrugDetail (
    DrugDID int IDENTITY(1,1) PRIMARY KEY,
	Prize int,
	Quantity int,
	Avialablity nchar,
	DrugID int FOREIGN KEY REFERENCES Drug(DrugID)
);



GO
create PROCEDURE RigisterEmploye (@FirstName nvarchar(30), @LastName nvarchar(50)
,@Address nvarchar(255),@City varchar(255),
@salary Int,@mobile nvarchar(50),@UserName nvarchar(255),@Password nvarchar(50),@Spaciality nvarchar(100))
AS

DECLARE @tmp DATE
SET @tmp =CURRENT_TIMESTAMP 
DECLARE @EmpID int 

begin
INSERT INTO Employe (FirstName, LastName, mobile)
VALUES (@FirstName, @LastName,@mobile );
set @EmpID=(select EmployeID from employe where mobile=@mobile)
INSERT INTO EmployeDetail (Address, City, salary,ken,EmployeID)
VALUES (@Address, @City,@salary,@tmp,@EmpID);

INSERT INTO EmployeUser (UserName, Password,Spaciality,EmployeID)
VALUES (@UserName,@Password,@Spaciality,@EmpID);

END
go
create PROCEDURE AproveAccaunt(@aprove nvarchar(10))
as
begin
insert into EmployeDetail(Aprove)
VALUES (@aprove);
end
go

exec AproveAccaunt @aprove='';



create PROCEDURE EmployeIdFinder(@mobile nvarchar(255))
as
DECLARE @Empid int
begin
set @Empid=(select EmployeID from EmployeUser where UserName=@mobile)
END


eXEC EmployeIdFinder @mobile = 'shewa';


go
create procedure AprovalCheck(@emID int)
as
begin
select Aprove ,
CASE 
WHEN Aprove = 'yes' THEN 'yes'
    WHEN Aprove = 'no' THEN 'no'
  
END AS aprove
from EmployeDetail
where EmployeID=@emID
end
go
exec AprovalCheck @emID='';




EXEC RigisterEmploye @FirstName = 'shewa', @LastName = 'sirak',
@Address = 'keble11', @City = 'bd', @salary = '235432'
, @mobile = '099600060', @UserName = 'shewa', @Password ='belay123',@Spaciality= 'Admin'; 




