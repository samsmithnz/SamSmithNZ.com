CREATE PROCEDURE [dbo].[spError_LogError]
	@source varchar(100),
	@error_message varchar(1000),
	@error_text varchar(8000)
AS

INSERT INTO ErrorLog
VALUES (getdate(), @source, @error_message, @error_text)