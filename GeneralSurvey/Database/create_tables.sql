DROP TABLE IF EXISTS USER;
DROP TABLE IF EXISTS SURVEY;
DROP TABLE IF EXISTS QUESTION;
DROP TABLE IF EXISTS CHOICE;
DROP TABLE IF EXISTS QUESTION_CHOICE;
DROP TABLE IF EXISTS SURVEY_QUESTION;
DROP TABLE IF EXISTS SURVEY_USER;
DROP TABLE IF EXISTS ANSWER;
 
CREATE TABLE USER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username CHAR(50) NOT NULL, 
    password CHAR(50) NOT NULL 
);

CREATE TABLE SURVEY (
    id INTEGER PRIMARY KEY AUTOINCREMENT, 
    title CHAR(50)
);

CREATE TABLE QUESTION (
    id INTEGER PRIMARY KEY AUTOINCREMENT, 
    title CHAR(50),
    id_survey INTEGER NOT NULL, 
    FOREIGN KEY(id_survey)
        REFERENCES SURVEY (id)
);

CREATE TABLE CHOICE (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    letter CHAR(50),
    id_question INTEGER NOT NULL,
    response CHAR(50) NOT NULL,
    FOREIGN KEY(id_question)
        REFERENCES QUESTION (id)
);

CREATE TABLE SURVEY_USER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_user INTEGER,
    id_survey INTEGER,
    is_filled INTEGER DEFAULT 0,
    FOREIGN KEY(id_user) 
        REFERENCES USER (id),
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY(id)
);

CREATE TABLE ANSWER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_choice INTEGER,
    id_survey INTEGER,
    answer_date CHAR(50),
    FOREIGN KEY(id_choice) 
        REFERENCES CHOICE (id),
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY (id)
);

INSERT INTO USER (id, username, password) 
VALUES (1, 'alice', 'password1'),
       (2, 'bob', 'password2'),
       (3, 'charlie', 'password3');

INSERT INTO SURVEY (id, title)
VALUES (1, 'Sondage 1'),
       (2, 'Sondage 2');

INSERT INTO QUESTION (id, title, id_survey) 
VALUES (1, 'À quelle tranche d''âge appartenez-vous?', 1000),
       (2, 'Êtes-vous une femme ou un homme?', 1000),
       (3, 'Quel journal lisez-vous à la maison?', 1000),
       (4, 'Combien de temps accordez-vous à la lecture de votre journal quotidiennement?', 1000),
       (5, 'À quelle tranche d''âge appartenez-vous?', 2000),
       (6, 'Êtes-vous une femme ou un homme?', 2000),
       (7, 'Combien de tasses de café buvez-vous chaque jour?', 2000),
       (8, 'Combien de consommations alcoolisées buvez-vous chaque jour?', 2000);

INSERT INTO CHOICE (id, letter, id_question, response) 
VALUES (0001, 'A', 0100, '0-25 ans'),                     -- Réponse pour la question 0100
       (0002, 'B', 0100, '25-50 ans'),
       (0003, 'C', 0100, '50-75 ans'),
       (0004, 'D', 0100, '75 ans et plus'),

       (0005, 'A', 0200, 'Femme'),                        -- Réponse pour la question 0200
       (0006, 'B', 0200, 'Homme'),
       (0007, 'C', 0200, 'Je ne veux pas répondre'),

       (0008, 'A', 0300, 'La Presse'),                    -- Réponse pour la question 0300
       (0009, 'B', 0300, 'Le Journal de Montréal'),
       (0010, 'C', 0300, 'The Gazette'),
       (0011, 'D', 0300, 'Le Devoir'),

       (0012, 'A', 0400, 'Moins de 10 minutes'),          -- Réponse pour la question 0400
       (0013, 'B', 0400, 'Entre 10 et 30 minutes'),
       (0014, 'C', 0400, 'Entre 30 et 60 minutes'),
       (0015, 'D', 0400, '60 minutes ou plus'),

       (0016, 'A', 0500, '0-25 ans'),                     -- Réponse pour la question 0500
       (0017, 'B', 0500, '25-50 ans'),
       (0018, 'C', 0500, '50-75 ans'),
       (0019, 'D', 0500, '75 ans et plus'),

       (0020, 'A', 0600, 'Femme'),                        -- Réponse pour la question 0600
       (0021, 'B', 0600, 'Homme'),
       (0022, 'C', 0600, 'Je ne veux pas répondre'),

       (0023, 'A', 0700, 'Je ne bois pas de café'),       -- Réponse pour la question 0700
       (0024, 'B', 0700, 'Entre 1 et 5 tasses'),
       (0025, 'C', 0700, 'Entre 6 et 10 tasses'),
       (0026, 'D', 0700, '10 tasses ou plus'),

       (0027, 'A', 0800, '0'),                            -- Réponse pour la question 0800
       (0028, 'B', 0800, '1'),
       (0029, 'C', 0800, '2'),
       (0030, 'D', 0800, '3 ou plus');

INSERT INTO SURVEY_USER (id, id_user, id_survey, is_filled) 
VALUES (1, 1, 1000, 1),  -- L'utilisateur 1 a rempli le sondage 1
       (2, 2, 1000, 0);  -- L'utilisateur 2 n'a pas rempli le sondage 1

-- -- Insertion de réponses données par des utilisateurs
-- INSERT INTO ANSWER (id_choice, answer_date) 
-- VALUES (1, '2024-09-13'),  -- L'utilisateur a choisi la réponse A pour une question
--        (2, '2024-09-13');  -- L'utilisateur a choisi la réponse B pour une autre question
