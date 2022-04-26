--
-- File generated with SQLiteStudio v3.3.3 on Mon Apr 25 19:34:11 2022
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: fjord
CREATE TABLE fjord (Id integer primary key, Species string, Depth int, Weight REAL);
INSERT INTO fjord (Id, Species, Depth, Weight) VALUES (1, 'SteedlHead', 600, 12.0);
INSERT INTO fjord (Id, Species, Depth, Weight) VALUES (2, 'Hornfish', 400, 1.0);
INSERT INTO fjord (Id, Species, Depth, Weight) VALUES (3, 'Salmon', 500, 4.9);

-- Table: fjordfish
CREATE TABLE fjordfish (Id integer primary key, Species string, Depth int, Weight REAL, Texture string);
INSERT INTO fjordfish (Id, Species, Depth, Weight, Texture) VALUES (1, 'SteedlHead', 600, 12.0, 'SteelHead');
INSERT INTO fjordfish (Id, Species, Depth, Weight, Texture) VALUES (2, 'Hornfish', 400, 1.0, 'HornFish');
INSERT INTO fjordfish (Id, Species, Depth, Weight, Texture) VALUES (3, 'Salmon', 500, 4.9, 'Salmon');

-- Table: halloffish
CREATE TABLE halloffish (Id integer primary key, UserId int, Species string, Weight REAL);

-- Table: highscore
CREATE TABLE highscore (Id integer primary key, Score);

-- Table: river
CREATE TABLE river (Id integer primary key, Species string, Depth int, Weight REAL);
INSERT INTO river (Id, Species, Depth, Weight) VALUES (1, 'NorthernPike', 300, 1.4);
INSERT INTO river (Id, Species, Depth, Weight) VALUES (2, 'Eel', 200, 3.6);
INSERT INTO river (Id, Species, Depth, Weight) VALUES (3, 'RainbowTrout', 200, 12.0);

-- Table: riverfish
CREATE TABLE riverfish (Id integer primary key, Species string, Depth int, Weight REAL, Texture string);
INSERT INTO riverfish (Id, Species, Depth, Weight, Texture) VALUES (1, 'NorthernPike', 300, 1.4, 'NorthernPike');
INSERT INTO riverfish (Id, Species, Depth, Weight, Texture) VALUES (2, 'Eel', 200, 3.6, 'Eel');
INSERT INTO riverfish (Id, Species, Depth, Weight, Texture) VALUES (3, 'RainbowTrout', 200, 12.0, 'RainbowTrout');

-- Table: rods
CREATE TABLE rods (Id integer primary key, Type string);
INSERT INTO rods (Id, Type) VALUES (1, 'SeaRod');
INSERT INTO rods (Id, Type) VALUES (2, 'RiverRod');
INSERT INTO rods (Id, Type) VALUES (3, 'FjordRod');

-- Table: sea
CREATE TABLE sea (Id integer primary key, Species string, Depth int, Weight REAL);
INSERT INTO sea (Id, Species, Depth, Weight) VALUES (1, 'Cod', 600, 40.0);
INSERT INTO sea (Id, Species, Depth, Weight) VALUES (2, 'Flatfish', 400, 1.0);
INSERT INTO sea (Id, Species, Depth, Weight) VALUES (3, 'Sturgeon', 700, 450.0);

-- Table: seafish
CREATE TABLE seafish (Id integer primary key, Species string, Depth int, Weight REAL, Texture string);
INSERT INTO seafish (Id, Species, Depth, Weight, Texture) VALUES (1, 'Cod', 600, 40.0, 'Cod');
INSERT INTO seafish (Id, Species, Depth, Weight, Texture) VALUES (2, 'Flatfish', 400, 1.0, 'FlatFish');
INSERT INTO seafish (Id, Species, Depth, Weight, Texture) VALUES (3, 'Sturgeon', 700, 450.0, 'Sturgeon');

-- Table: userslots
CREATE TABLE userslots (Id integer primary key, Score int);
INSERT INTO userslots (Id, Score) VALUES (1, 0);
INSERT INTO userslots (Id, Score) VALUES (2, 0);
INSERT INTO userslots (Id, Score) VALUES (3, 0);
INSERT INTO userslots (Id, Score) VALUES (4, 0);

-- View: view fjord
CREATE VIEW "view fjord" AS Select * from fjord;

-- View: View fjordfish
CREATE VIEW "View fjordfish" AS Select * from fjordfish;

-- View: view river
CREATE VIEW "view river" AS Select * from river;

-- View: view riverfish
CREATE VIEW "view riverfish" AS select * from riverfish;

-- View: view sea
CREATE VIEW "view sea" AS select * from sea;

-- View: View seafish
CREATE VIEW "View seafish" AS select * from seafish;

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
