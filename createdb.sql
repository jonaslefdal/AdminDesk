CREATE DATABASE IF NOT EXISTS AdminDeskDatabase;
USE AdminDeskDatabase;

CREATE TABLE ServiceOrdrer (
    ServiceOrderId INT AUTO_INCREMENT PRIMARY KEY,
    Mechanic VARCHAR(255),
    IsAdministrator TINYINT(1),
    SerialNumber VARCHAR(255),
    CreatedDate DATETIME,
    ConsumedHours DECIMAL(10, 2),
    MechanicComment TEXT,
    CustomerComment TEXT,
    FutureMaintenance TEXT,
    CustomerName VARCHAR(255),
    CustomerEmail VARCHAR(255),
    CustomerStreet VARCHAR(255),
    CustomerCity VARCHAR(255),
    CustomerZipcode VARCHAR(255),
    CustomerTelephoneNumber VARCHAR(20)
);