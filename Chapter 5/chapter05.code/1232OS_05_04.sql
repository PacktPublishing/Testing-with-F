-- create insert test data
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SetupTestData
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- local variables

	DECLARE @PersonID1 int,
			@PersonID2 int,
			@PersonID3 int,
			@AddressID1 int,
			@AddressID2 int;

	-- Create test persons
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)		
		VALUES ('193808209005', 'Ingela Ping', 'Ingela', 'Forsman', '1938-08-20');
	SET @PersonID1 = SCOPE_IDENTITY();
	
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)
		VALUES ('194212259005', 'Inge Pong', 'Inge', 'Forsman', '1942-12-25');
	SET @PersonID2 = SCOPE_IDENTITY();
	
	INSERT INTO dbo.Person (SSN, FirstName, GivenName, LastName, BirthDate)
		VALUES ('196403063374', 'Jesper', 'Jesper', 'Forsman', '1964-03-06');
	SET @PersonID3 = SCOPE_IDENTITY();
	-- ... more test persons

	-- Create test addresses
	INSERT INTO dbo.[Address] (StreetName, StreetNumber, PostalCode, Town)
		VALUES ('Lantgatan', '38', '12559', 'SOLNA');
	SET @AddressID1 = SCOPE_IDENTITY();

	INSERT INTO dbo.[Address] (StreetName, StreetNumber, StreetLetter, ApartmentNumber, [Floor], PostalCode, Town)
		VALUES ('Stångmästarevägen', '11-13', 'A', '1201', '2', '15955', 'STOCKHOLM');
	SET @AddressID2 = SCOPE_IDENTITY();
	-- ... more test addresses

	-- Create relations
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID1, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID2, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID3, @AddressID1, '1970-01-01');
	INSERT INTO dbo.NationalRegistrationAddress (Person, [Address], [Date])
		VALUES (@PersonID3, @AddressID2, '1982-03-06');
	-- ...more test relations
END
GO
