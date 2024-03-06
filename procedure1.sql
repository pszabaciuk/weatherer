CREATE PROCEDURE Procedure1
    @CountryName NVARCHAR(100)
AS
BEGIN
    SELECT 
        MAX(w.WindSpeed), 
        c.Country 
    FROM 
        Weather w
        INNER JOIN City c ON c.CityId = w.CityId 
    WHERE 
        c.Country = @CountryName
    GROUP BY 
        c.Country;
END;