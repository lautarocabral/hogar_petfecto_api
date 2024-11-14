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

INSERT INTO TiposPerfil (Descripcion)
VALUES
('Adoptante'), --1
('Cliente'), --2
('Veterinaria'), --3
('Protectora'); --4

-- MODULO DE SEGURIDAD
INSERT INTO dbo.Permisos(Descripcion, NombrePermiso )
VALUES
('Modulo Adoptante', 'Adoptante'), --1
('Modulo Cliente', 'Cliente'), --2
('Modulo Veterinaria', 'Veterinaria'), --3
('Modulo Protectora', 'Protectora'), --4
('Modulo SignUp Adoptante', 'AdoptanteSignUp'), -- 5
('Modulo SignUp Cliente', 'ClienteSignUp'), --6 
('Modulo SignUp Veterinaria', 'VeterinariaSignUp'), --7
('Modulo SignUp Protectora', 'ProtectoraSignUp'),--8
('Modulo de Seguridad', 'Seguridad');  --9

INSERT INTO dbo.Grupos(Descripcion )
VALUES
('Admin'), --1
('Invitado'), --2
('Adoptante'), --3
('Cliente'), --4
('Veterinaria'), --5
('Protectora'); --6

-- INSERT INTO dbo.Usuarios(Nombre,[User],[Password], Email )
-- VALUES
-- ('Lautaro Admin', 'administrador','$2a$11$ONN8NyjdtON8XB74xL2aWup73LEOQGp8wG7dnVDvFnwNAFOJKbjrC', 'lautacabral96@gmail.com');

-- INSERT INTO dbo.GrupoUsuario(GrupoId, UsuariosId )
-- VALUES
-- (1, 1);

INSERT INTO dbo.GrupoPermiso(GruposId, PermisosId )
VALUES
(1, 1), -- Admin --> Modulo Adoptante
(1, 2), -- Admin --> Modulo Cliente
(1, 3), -- Admin --> Modulo Veterinaria
(1, 4), -- Admin --> Modulo Protectora
(1, 5), -- Admin --> Modulo SignUp Adoptante
(1, 6), -- Admin --> Modulo SignUp Cliente
(1, 7), -- Admin --> Modulo SignUp Veterinaria
(1, 8), -- Admin --> Modulo SignUp Protectora
(1, 9), -- Admin --> Modulo De Seguridad
(3, 1), -- Adoptante --> Modulo Adoptante
(4, 2), -- Cliente --> Modulo Cliente
(5, 3), -- Veterinaria --> Modulo Veterinaria
(6, 4), -- Protectora --> Modulo Protectora
(2, 5), -- Invitado --> Modulo SignUp Adoptante
(2, 6), -- Invitado --> Modulo SignUp Cliente
(2, 7), -- Invitado --> Modulo SignUp Veterinaria
(2, 8); -- Invitado --> Modulo SignUp Protectora


INSERT INTO dbo.TiposMascota(Tipo )
VALUES
('Perro'), --1
('Gato'); --2

INSERT INTO dbo.EstadosPostulacion(Estado )
VALUES
('Adoptado'), --1
('No adoptado'); --2