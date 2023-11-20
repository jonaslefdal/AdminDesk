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

INSERT INTO users (FirstName, LastName, Email, Passord, AnsattNummer, CreatedDate) VALUES
('Bruker',  '1', 'user1gmail.com', '1passord123', 'B342', '2023-11-17 11:15:00'),
('Bruker', '2', 'user2gmail.com', '2passord123', 'A456', '2023-11-17 11:15:00'),
('Bruker', '3', 'user3gmail.com', '3passord123', 'B789', '2023-11-17 11:15:00'),
('Bruker','4', 'user4gmail.com', '4passord123', 'C123', '2023-11-17 11:15:00'), 
('Bruker','5', 'user5gmail.com', '5passord123', 'D456', '2023-11-17 11:15:00'),
('Bruker','6', 'user6gmail.com', '6passord123', 'E789', '2023-11-17 11:15:00'),
('Bruker','7', 'user7gmail.com', '7passord123', 'F123', '2023-11-17 11:15:00');

INSERT INTO admindeskdatabase.serviceorders (CustomerId, Mechanic, SerialNumber, CreatedDate, Comment, FutureMaintenance, ReserveDeler, CreatedById, OrderStatus, TotalArbeidsTimer) VALUES
(1,'Mekaniker 1', 'SN123456', '2023-11-15 09:30:00', 'Kunden sier den ikke funker', 'Bytt olje og filter', true, 4, 'Venter', '5 timer'),
(2,'Mekaniker 2', 'SN789012', '2023-11-16 14:45:00', 'Endre bremseklosser', 'Sjekk bremsesystemet', false, 5, 'Pågående', '0 timer'),
(3,'Mekaniker 3', 'SN654321', '2023-11-17 11:15:00', 'Undersøk motoren', 'Bytt skrue', true, 7, 'Fullført', '8 timer'),
(4,'Mekaniker 4', 'SN111111', '2023-11-18 10:00:00', 'Ukjent Problem', 'Null', false, 2, 'Venter', '0 timer'),
(5,'Mekaniker 5', 'SN222222', '2023-11-19 13:30:00', 'Sjekk motoren', 'Burde oljes', false, 6, 'Pågående', '11 timer'),
(6,'Mekaniker 6', 'SN333333', '2023-11-20 09:45:00', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', true, 3, 'Fullført', '20 timer'),
(7,'Mekaniker 7', 'SN233333', '2023-11-20 09:45:00', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', true, 3, 'Fullført', '20 timer'),
(8,'Mekaniker 8', 'SN354333', '2023-11-20 09:45:00', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', true, 3, 'Fullført', '20 timer'),
(9,'Mekaniker 9', 'SN3387633', '2023-11-20 09:45:00', 'Vinsj kroken funker ikke', 'Bytt bremsevæske', true, 3, 'Fullført', '20 timer');

INSERT INTO admindeskdatabase.spareparts (PartName, Quantity, DeliveryTime)
VALUES
	('Krok', 200, 3),
	('Kjetting', 0, 2),
	('Hjul', 0, 3),
	('Belte', 0, 2),
	('Skrue', 10, 3);
    
INSERT INTO admindeskdatabase.orderspareparts (OrderId, SparePartId)
VALUES
	('1', 1),
    ('1', 2),
    ('1', 3),
	('3', 1),
    ('3', 3),
    ('7', 2),
    ('7', 5),
    ('8', 4),
    ('8', 5);
