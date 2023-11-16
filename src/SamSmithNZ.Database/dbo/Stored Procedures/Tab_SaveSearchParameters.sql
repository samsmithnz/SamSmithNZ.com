CREATE PROCEDURE [dbo].[Tab_SaveSearchParameters]
	@SearchText VARCHAR(100)
AS
DECLARE @RecordId UNIQUEIDENTIFIER
SELECT @RecordId = NEWID()
 
INSERT INTO tab_search_parameters
SELECT @RecordId, @SearchText, GETDATE()

SELECT @RecordId AS RecordId