CREATE PROCEDURE [dbo].[InsertNewUser]
@Email			TEXT,
@Password		TEXT,
@FileMatrices	varbinary(MAX),
@EigFacesSize	INT
AS
BEGIN
INSERT INTO Users
	(
	Email,
Password,
FileMatrices,
EigFacesSize
)
VALUES
(
@Email			,
@Password		,
@FileMatrices	,
@EigFacesSize
)
END;