INSERT INTO admindeskdatabase.aspnetroles (Id, Name, NormalizedName)
VALUES
	(1, 'Admin', 'ADMIN'),
    (2, 'User', 'USER');

INSERT INTO admindeskdatabase.aspnetusers (Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount) VALUES
	 ('ABCDEF-GHIJK-LMNOP','root@gmail.com','ROOT@GMAIL.COM','root@gmail.com','ROOT@GMAIL.COM',1,'AQAAAAIAAYagAAAAEMsqn5YNGzEl588OzxruQC43dmy+bu4MEctma1H7tpnJNF1mMHZQCgkKdraq9jtWWg==','XX6B46VQPQVSU2AUUZZXLKJV7OXURZ4J','bdb77259-5c3f-482f-b8eb-cca77d2d6097',NULL,0,0,NULL,1,0);

INSERT INTO admindeskdatabase.aspnetuserroles (userId, RoleId)
VALUES
	('ABCDEF-GHIJK-LMNOP', 1);

INSERT INTO admindeskdatabase.customer (CustomerFirstName, CustomerLastName,  CustomerEmail, CustomerStreet, CustomerCity, CustomerZipcode, CustomerPhoneNumber, CustomerComment) VALUES
('Kunde','1', 'kunde1@gmail.com', 'Universitetsveien 12', 'Kristiansand', '4621', '+4748567890', 'Oljeskift'),
('Kunde','2', 'kunde2@gmail.com', 'Karasjoksgata 90', 'Karasjok', '7243', '+4796543210', 'Knirker'),
('Kunde', '3', 'kunde3@gmail.com', 'Gategata 17', 'Moss', '2381', '+4742334455', 'Bøyd kant'),
('Kunde',  '4', 'kunde4@gmail.com', 'Kokleheia 19', 'Kristiansand', '4529', '+4748776655', 'Stand sjekk'),
('Kunde', '5', 'kunde5@gmail.com', 'GateEksempel 1', 'Bergen', '9865', '+4792334455', 'Lekker veske'),
('Kunde','6', 'kunde6@gmail.com', 'GateEksempel 7', 'Arendal', '4578', '+4798776655', 'Lager rar lyd'),
('Kunde','7', 'kunde7@gmail.com', 'Trymgata 73', 'Oslo', '0088', '+4742334455', 'Denne vinsjen er helt ny altså'),
('Kunde','8', 'kunde8@gmail.com', 'GateEksempel 9', 'Bergen', '5223', '+4748776655', 'Vinsjen er 10 år gammel, den ble brukt av bestefar'),
('Kunde','9', 'kunde9@gmail.com', 'GateEksempel 12', 'Bergen', '5221', '+4797233445', 'Den funker ikke, vært slik en måned cirka');

INSERT INTO admindeskdatabase.serviceorders (CustomerId, Mechanic, SerialNumber, CreatedDate, Comment, FutureMaintenance, CreatedById, OrderStatus, TotalWorkHours) VALUES
(1,'Mekaniker 1', 'SN123456', '2023-11-15', 'Kunden sier den ikke funker', 'Bytt olje og filter', 'ABCDEF-GHIJK-LMNOP', 'Venter', '5 timer'),
(2,'Mekaniker 2', 'SN789012', '2023-11-16', 'Endre bremseklosser', 'Sjekk bremsesystemet', 'ABCDEF-GHIJK-LMNOP', 'Pågående', '0 timer'),
(3,'Mekaniker 3', 'SN654321', '2023-11-17', 'Undersøk motoren', 'Bytt skrue', 'ABCDEF-GHIJK-LMNOP', 'Fullført', '8 timer'),
(4,'Mekaniker 4', 'SN111111', '2023-11-18', 'Ukjent Problem', 'Null', 'ABCDEF-GHIJK-LMNOP', 'Venter', '0 timer'),
(5,'Mekaniker 5', 'SN222222', '2023-11-19', 'Sjekk motoren', 'Burde oljes', 'ABCDEF-GHIJK-LMNOP', 'Pågående', '11 timer'),
(6,'Mekaniker 6', 'SN333333', '2023-11-20', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', 'ABCDEF-GHIJK-LMNOP', 'Fullført', '20 timer'),
(7,'Mekaniker 7', 'SN233333', '2023-11-20', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', 'ABCDEF-GHIJK-LMNOP', 'Fullført', '20 timer'),
(8,'Mekaniker 8', 'SN354333', '2023-11-20', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', 'ABCDEF-GHIJK-LMNOP', 'Fullført', '20 timer'),
(9,'Mekaniker 9', 'SN3387633', '2023-11-20', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', 'ABCDEF-GHIJK-LMNOP', 'Fullført', '20 timer');

INSERT INTO report (ReportId, ServiceOrderId, Mechanic, ServiceType, MechanicComment, ServiceDescription, ReportWriteDate, UserSign) VALUES
(1, 1, 'Per makaniker', 'Sjekk', 'Funker ikke','Avklart','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(3, 1, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(4, 2, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(5, 2, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(6, 3, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(7, 4, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP'),
(8, 5, 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00','2023-11-17 11:15:00', 'ABCDEF-GHIJK-LMNOP');
