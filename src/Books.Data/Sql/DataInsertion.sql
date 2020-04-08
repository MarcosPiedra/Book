USE [Books]
GO

DECLARE @cnt INT = 0;

WHILE @cnt < 18
BEGIN
	INSERT INTO [dbo].[book]
			   ([bo_title]
			   ,[bo_description]
			   ,[bo_author]
			   ,[bo_publication_date]
			   ,[bo_is_read])
		 VALUES
			   (CONCAT('title ', @cnt)
			   ,CONCAT('description ', @cnt)
			   ,CONCAT('author ', @cnt)
			   ,DATEADD(day, @cnt, '29-02-1980')
			   ,@cnt % 2)
   SET @cnt = @cnt + 1;
END;
GO




