CREATE DATABASE IF NOT EXISTS AdminDeskDatabase;
USE AdminDeskDatabase;

CREATE TABLE IF NOT EXISTS Serviceordre (
    id INT AUTO_INCREMENT PRIMARY KEY,
    Ordrenummer INT,
    Navn VARCHAR(255),
    Email VARCHAR(255),
    Telefon VARCHAR(20),
    Mottatt_dato DATE,
    Adresse VARCHAR(255),
    ServiceordreStatus VARCHAR(50),
    Arbeidstimer INT,
    Reparatør VARCHAR(255)
);
