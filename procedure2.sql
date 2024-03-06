CREATE PROCEDURE Procedure2
    @MinTemperature DECIMAL
AS
BEGIN
    SELECT 
        c.Country, 
        MIN(w.Temperature) AS Temperature, 
        MAX(w.WindSpeed) AS WindSpeed
    FROM 
        Weather w
        INNER JOIN City c ON c.CityId = w.CityId 
    GROUP BY 
        c.Country
    HAVING 
        MIN(w.Temperature) < @MinTemperature;
END;