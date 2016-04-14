SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetPagesOfPageType
	@PageType varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF;

	DECLARE @Properties varchar(MAX),
			@Query AS NVARCHAR(MAX)


	-- extract columns as comma separated string from page type
	SELECT @Properties = STUFF(
		(SELECT ',' + property.[Name]
         FROM [Property] property
		 INNER JOIN [PageType] pageType ON property.PageType_FK = pageType.ID
	     WHERE pageType.Name = @PageType
         FOR XML PATH('')), 1, 1, '')

SET @Query = N'SELECT pageID, ' + @Properties + N' FROM
	(
		SELECT [page].ID as pageID,
			   property.[Name] as name,
			   propertyValue.[Value] as value
		FROM [Property] property
			INNER JOIN [PageType] pageType ON property.PageType_FK = pageType.ID
			INNER JOIN [Page] [page] ON [page].PageType_FK = pageType.ID
			INNER JOIN [PropertyValue] propertyValue ON propertyValue.Property_FK = property.ID AND propertyValue.Page_FK = [page].ID
		WHERE pageType.Name = ''' + @PageType + N'''
	) x

	PIVOT
	(
		max(value)
		FOR name IN (' + @Properties + N')
		) p'

exec sp_executesql @query
END
GO
