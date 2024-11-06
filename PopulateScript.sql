Use Hogar_Petfecto

INSERT INTO TiposPerfil (Descripcion)
VALUES
('Adoptante'),
('Cliente'),
('Veterinaria'),
('Protectora');

--INSERT INTO TiposPerfil (Descripcion)
--VALUES
--('Adoptante'),
--('Cliente'),

Use Hogar_Petfecto
INSERT INTO Provincias (ProvinciaNombre) VALUES ('Buenos Aires');
INSERT INTO Provincias (ProvinciaNombre) VALUES ('Córdoba');
INSERT INTO Provincias (ProvinciaNombre) VALUES ('Santa Fe');
INSERT INTO Provincias (ProvinciaNombre) VALUES ('Mendoza');
INSERT INTO Provincias (ProvinciaNombre) VALUES ('Salta');
Use Hogar_Petfecto
-- Localidades para Buenos Aires (ProvinciaId = 1)
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('La Plata', 1);
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Mar del Plata', 1);

-- Localidades para Córdoba (ProvinciaId = 2)
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Córdoba Capital', 2);
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Villa Carlos Paz', 2);

-- Localidades para Santa Fe (ProvinciaId = 3)
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Rosario', 3);
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Santa Fe', 3);

-- Localidades para Mendoza (ProvinciaId = 4)
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Mendoza Capital', 4);
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('San Rafael', 4);

-- Localidades para Salta (ProvinciaId = 5)
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Salta Capital', 5);
INSERT INTO Localidades (LocalidadNombre, ProvinciaId) VALUES ('Cafayate', 5);


--{
--  "dni": "12345678",
--  "razonSocial": "Juan Perez",
--  "localidadId": 1,
--  "direccion": "Calle Falsa 123",
--  "telefono": "+54 9 11 1234 5678",
--  "fechaNacimiento": "1990-05-15T00:00:00",
--  "email": "juan.perez@example.com",
--  "password": "password123"
--}
