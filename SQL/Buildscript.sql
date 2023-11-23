CREATE DATABASE IF NOT EXISTS admindeskdatabase;

USE admindeskdatabase;
 
CREATE TABLE IF NOT EXISTS `admindeskdatabase`.`users` (
 `UserId` INT NOT NULL AUTO_INCREMENT,
 `FirstName` VARCHAR(255) NULL DEFAULT NULL,
 `LastName` VARCHAR(255) NULL DEFAULT NULL,
 `Email` VARCHAR(255) NULL DEFAULT NULL,
 `Passord` VARCHAR(255) NULL DEFAULT NULL,
 `AnsattNummer` VARCHAR (255) NULL DEFAULT NULL,
 `CreatedDate` DATETIME NULL DEFAULT NULL,
 PRIMARY KEY (`UserId`),
 UNIQUE INDEX `Id` (`UserId` ASC) VISIBLE,
 UNIQUE INDEX `Email` (`Email` ASC) VISIBLE);

CREATE TABLE IF NOT EXISTS `admindeskdatabase`.`customer` (
 `CustomerID` INT NOT NULL AUTO_INCREMENT,
 `CustomerFirstName` VARCHAR(255) NULL,
 `CustomerLastName` VARCHAR(255) NULL,
 `CustomerEmail` VARCHAR(255) NULL,
 `CustomerStreet` VARCHAR(255) NULL,
 `CustomerCity` VARCHAR(255) NULL,
 `CustomerZipcode` VARCHAR(255) NULL,
 `CustomerPhoneNumber` VARCHAR(255) NULL,
 `CustomerComment` TEXT NULL,
 PRIMARY KEY (`CustomerID`),
 UNIQUE INDEX `CustomerID_UNIQUE` (`CustomerID` ASC) VISIBLE);

CREATE TABLE IF NOT EXISTS `admindeskdatabase`.`serviceorders` (
 `ServiceOrderId` INT NOT NULL AUTO_INCREMENT,
 `CustomerId` INT NOT NULL,
 `Mechanic` VARCHAR(255) NULL DEFAULT NULL,
 `SerialNumber` VARCHAR(255) NULL DEFAULT NULL,
 `CreatedDate` DATETIME NULL DEFAULT NULL,
 `Comment` TEXT NULL DEFAULT NULL,
 `FutureMaintenance` TEXT NULL DEFAULT NULL,
 `CreatedById` INT NOT NULL,
 `OrderStatus` VARCHAR(45) NULL DEFAULT NULL,
 `ReserveDeler` BOOLEAN NULL DEFAULT NULL,
 `TotalWorkHours` VARCHAR (255) NULL DEFAULT NULL,
 PRIMARY KEY (`ServiceOrderId`),
 UNIQUE INDEX `Id_UNIQUE` (`ServiceOrderId` ASC) VISIBLE,
 INDEX `FK_CreatedById_UserId_idx` (`CreatedById` ASC) VISIBLE,
 INDEX `FK_customerId_customerId_idx` (`CustomerId` ASC) VISIBLE, 
 CONSTRAINT `FK_customerId_customerId`
	FOREIGN KEY (`CustomerId`)
	REFERENCES `admindeskdatabase`.`customer` (`CustomerID`)
  ON DELETE NO ACTION
	ON UPDATE NO ACTION,
 CONSTRAINT `FK_CreatedById_UserId`
    FOREIGN KEY (`CreatedById`)
    REFERENCES `admindeskdatabase`.`users` (`UserId`)
  ON DELETE NO ACTION
	ON UPDATE NO ACTION);


CREATE TABLE IF NOT EXISTS `admindeskdatabase`.`spareparts` (
  `SparePartId` INT NOT NULL AUTO_INCREMENT,
  `PartName` VARCHAR(255) NULL,
  `Quantity` INT NULL,
  `DeliveryTime` INT NULL,
  PRIMARY KEY (`SparePartId`),
  UNIQUE INDEX `SparePartId_UNIQUE` (`SparePartId` ASC) VISIBLE);
  
  
CREATE TABLE IF NOT EXISTS `admindeskdatabase`.`orderspareparts` (
  `OrderId` INT NOT NULL,
  `SparePartId` INT NOT NULL,
  PRIMARY KEY (`SparePartId`, `OrderId`),
  INDEX `FK_SparePart_idx` (`SparePartId` ASC) VISIBLE,
  CONSTRAINT `FK_OrderId`
    FOREIGN KEY (`OrderId`)
    REFERENCES `admindeskdatabase`.`serviceorders` (`ServiceOrderId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_SparePart`
    FOREIGN KEY (`SparePartId`)
    REFERENCES `admindeskdatabase`.`spareparts` (`SparePartId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE IF NOT EXISTS `report` (
  `ReportId` INT NOT NULL AUTO_INCREMENT,
  `ServiceOrderId` INT NOT NULL,
  `Mechanic` VARCHAR(255) NULL,
  `ServiceType` VARCHAR(45) NULL,
  `MechanicComment` TEXT NULL,
  `ServiceDescription` TEXT NULL,
  `ReportWriteDate` DATE NULL,
  `UserSign` INT NOT NULL,
  PRIMARY KEY (`ReportId`),
  INDEX `FK_ServiceOrderId_idx` (`ServiceOrderId` ASC),
  CONSTRAINT `FK_ServiceOrderId`
    FOREIGN KEY (`ServiceOrderId`)
    REFERENCES `serviceorders` (`ServiceOrderId`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedName` VARCHAR(256) NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `RoleNameIndex` (`NormalizedName` ASC) VISIBLE);


CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `RoleId` VARCHAR(255) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetRoleClaims_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `aspnetroles` (`Id`)
    ON DELETE CASCADE);

CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` VARCHAR(255) NOT NULL,
  `UserName` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedUserName` VARCHAR(256) NULL DEFAULT NULL,
  `Email` VARCHAR(256) NULL DEFAULT NULL,
  `NormalizedEmail` VARCHAR(256) NULL DEFAULT NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL DEFAULT NULL,
  `SecurityStamp` LONGTEXT NULL DEFAULT NULL,
  `ConcurrencyStamp` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumber` LONGTEXT NULL DEFAULT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEnd` DATETIME(6) NULL DEFAULT NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT(11) NOT NULL,
  `Disabled` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserNameIndex` (`NormalizedUserName` ASC) VISIBLE,
  INDEX `EmailIndex` (`NormalizedEmail` ASC) VISIBLE);

      CREATE TABLE IF NOT EXISTS .`aspnetuserclaims` (
  `Id` INT(11) NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(255) NOT NULL,
  `ClaimType` LONGTEXT NULL DEFAULT NULL,
  `ClaimValue` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_AspNetUserClaims_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnetusers` (`Id`)
    ON DELETE CASCADE);

   CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` VARCHAR(255) NOT NULL,
  `ProviderKey` VARCHAR(255) NOT NULL,
  `ProviderDisplayName` LONGTEXT NULL DEFAULT NULL,
  `UserId` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`),
  INDEX `IX_AspNetUserLogins_UserId` (`UserId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnetusers` (`Id`)
    ON DELETE CASCADE);

CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` VARCHAR(255) NOT NULL,
  `RoleId` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `IX_AspNetUserRoles_RoleId` (`RoleId` ASC) VISIBLE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES .`aspnetroles` (`Id`)
    ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnetusers` (`Id`)
    ON DELETE CASCADE);

CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` VARCHAR(255) NOT NULL,
  `LoginProvider` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(255) NOT NULL,
  `Value` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `aspnetusers` (`Id`)
    ON DELETE CASCADE);