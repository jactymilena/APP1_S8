DROP TABLE IF EXISTS USER;
DROP TABLE IF EXISTS SURVEY;
DROP TABLE IF EXISTS QUESTION;
DROP TABLE IF EXISTS CHOICE;
DROP TABLE IF EXISTS QUESTION_CHOICE;
DROP TABLE IF EXISTS SURVEY_QUESTION;
DROP TABLE IF EXISTS SURVEY_USER;
DROP TABLE IF EXISTS ANSWER;
DROP TABLE IF EXISTS API_KEY;
 
CREATE TABLE USER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username CHAR(50) NOT NULL, 
    password CHAR(50) NOT NULL,
    salt CHAR(50) NOT NULL 
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
    id_user INTEGER NOT NULL,
    id_survey INTEGER NOT NULL,
    FOREIGN KEY(id_user) 
        REFERENCES USER (id),
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY(id)
);

CREATE TABLE ANSWER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_choice INTEGER NOT NULL,
    id_survey INTEGER NOT NULL,
    answer_date DATE DEFAULT CURRENT_DATE,
    FOREIGN KEY(id_choice) 
        REFERENCES CHOICE (id),
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY (id)
);

CREATE TABLE API_KEY (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    key CHAR(50) NOT NULL,
    id_user INTEGER,
    FOREIGN KEY(id_user) 
        REFERENCES USER (id)
);

INSERT INTO API_KEY (key) 
VALUES ('638bb43e-35f4-479f-8dbe-6b77ced30c14'),
       ('42a7f5ce-9446-4ddd-87b9-f4e4924c9c89'),
       ('20d2c108-005d-40a4-b193-2d0f86fbc7e4'),
       ('e5036a72-c232-41de-8a50-11ead9999ac7');

INSERT INTO SURVEY (title)
VALUES ('Sondage 1'),
       ('Sondage 2');

INSERT INTO QUESTION (title, id_survey) 
VALUES ('À quelle tranche d''âge appartenez-vous?', 1000),
       ('Êtes-vous une femme ou un homme?', 1000),
       ('Quel journal lisez-vous à la maison?', 1000),
       ('Combien de temps accordez-vous à la lecture de votre journal quotidiennement?', 1000),
       ('À quelle tranche d''âge appartenez-vous?', 2000),
       ('Êtes-vous une femme ou un homme?', 2000),
       ('Combien de tasses de café buvez-vous chaque jour?', 2000),
       ('Combien de consommations alcoolisées buvez-vous chaque jour?', 2000);

INSERT INTO CHOICE (letter, id_question, response) 
VALUES ('A', 1, '0-25 ans'),                     -- Réponse pour la question 0100
       ('B', 1, '25-50 ans'),
       ('C', 1, '50-75 ans'),
       ('D', 1, '75 ans et plus'),

       ('A', 2, 'Femme'),                        -- Réponse pour la question 0200
       ('B', 2, 'Homme'),
       ('C', 2, 'Je ne veux pas répondre'),

       ('A', 3, 'La Presse'),                    -- Réponse pour la question 0300
       ('B', 3, 'Le Journal de Montréal'),
       ('C', 3, 'The Gazette'),
       ('D', 3, 'Le Devoir'),

       ('A', 4, 'Moins de 10 minutes'),          -- Réponse pour la question 0400
       ('B', 4, 'Entre 10 et 30 minutes'),
       ('C', 4, 'Entre 30 et 60 minutes'),
       ('D', 4, '60 minutes ou plus'),

       ('A', 5, '0-25 ans'),                     -- Réponse pour la question 0500
       ('B', 5, '25-50 ans'),
       ('C', 5, '50-75 ans'),
       ('D', 5, '75 ans et plus'),

       ('A', 6, 'Femme'),                        -- Réponse pour la question 0600
       ('B', 6, 'Homme'),
       ('C', 6, 'Je ne veux pas répondre'),

       ('A', 7, 'Je ne bois pas de café'),       -- Réponse pour la question 0700
       ('B', 7, 'Entre 1 et 5 tasses'),
       ('C', 7, 'Entre 6 et 10 tasses'),
       ('D', 7, '10 tasses ou plus'),

       ('A', 8, '0'),                            -- Réponse pour la question 0800
       ('B', 8, '1'),
       ('C', 8, '2'),
       ('D', 8, '3 ou plus');

-- INSERT INTO SURVEY_USER (id_user, id_survey, is_filled) 
-- VALUES (1, 1, 1),  -- L'utilisateur 1 a rempli le sondage 1
--        (2, 1, 0);  -- L'utilisateur 2 n'a pas rempli le sondage 1

-- -- Insertion de réponses données par des utilisateurs
-- INSERT INTO ANSWER (id_choice, answer_date) 
-- VALUES (1, '2024-09-13'),  -- L'utilisateur a choisi la réponse A pour une question
--        (2, '2024-09-13');  -- L'utilisateur a choisi la réponse B pour une autre question
