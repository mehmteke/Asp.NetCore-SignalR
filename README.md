# Asp.NetCore-SignalR
SELECT TOP 1000 [Id]
      ,[City]
      ,[Count]
      ,[CovidDate]
  FROM [CovidChart].[dbo].[Covids]

--TRUNCATE TABLE [CovidChart].[dbo].[Covids]

SELECT TARIH, 
       [16],[22],[41],[34],[65]
	   FROM
	   (SELECT City,[Count],CAST(CovidDate AS DATE) AS TARIH FROM [CovidChart].[dbo].[Covids])
	   AS QUERY
PIVOT
(
   SUM([COUNT]) FOR City IN ([16],[22],[41],[34],[65])
)
       AS PTABLE
ORDER BY TARIH
