CREATE PROCEDURE [dbo].[spKS_Tab_SaveSearchParameters]
	@search_text varchar(100)
AS
DECLARE @record_id uniqueidentifier
SELECT @record_id = NEWID()
 
INSERT INTO tab_search_parameters
SELECT @record_id, @search_text, GETDATE()

SELECT @record_id as record_id