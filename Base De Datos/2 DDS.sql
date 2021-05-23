
CREATE TABLE IF NOT EXISTS Paises (
	id_pais INTEGER PRIMARY KEY AUTOINCREMENT,
	iso CHAR(2) NOT NULL,
	nombre_pais VARCHAR(80) NOT NULL,
	codigo_telefono INTEGER
);

CREATE TABLE IF NOT EXISTS Tipo_Persona (
	id_tipo INTEGER PRIMARY KEY AUTOINCREMENT,
	nombre VARCHAR(30) NOT NULL,
	descripcion VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS Personas (
	id_persona INTEGER PRIMARY KEY AUTOINCREMENT,
	identificacion CHAR(12) NOT NULL,
    primer_nombre VARCHAR(50) NOT NULL,
    segundo_nombre VARCHAR(50),
    primer_apellido VARCHAR(50) NOT NULL,
    segundo_apellido VARCHAR(50),
    id_pais_origen INTEGER NOT NULL,
    id_tipo INTEGER NOT NULL,
    FOREIGN KEY (id_pais_origen) REFERENCES Paises (id_pais) 
			ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY (id_tipo) REFERENCES Tipo_Persona (id_tipo) 
			ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS Telefonos (
    id INTEGER,
	numero INTEGER NOT NULL,
    id_pais INTEGER NOT NULL,
    id_persona INTEGER NOT NULL,
    FOREIGN KEY (id_pais) REFERENCES Paises (id_pais) 
			ON DELETE CASCADE ON UPDATE NO ACTION,
    FOREIGN KEY (id_persona) REFERENCES Personas (id_persona) 
			ON DELETE CASCADE ON UPDATE NO ACTION,
    CONSTRAINT telefono_unico UNIQUE (numero, id_pais, id_persona)
);


CREATE TABLE IF NOT EXISTS Control_Actividad (
	id_control INTEGER PRIMARY KEY AUTOINCREMENT,
    id_persona INTEGER NOT NULL,
    actividad  CHAR(6) CHECK( actividad IN ('ENTRADA','SALIDA') ),
    fecha  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,    
    FOREIGN KEY (id_persona) REFERENCES Personas (id_persona) 
			ON DELETE CASCADE ON UPDATE NO ACTION
);



  