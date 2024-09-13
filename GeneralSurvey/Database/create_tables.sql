DROP TABLE USER;
DROP TABLE SURVEY;
DROP TABLE QUESTION;
DROP TABLE QUESTION_CHOICE;
DROP TABLE SURVEY_QUESTION;
DROP TABLE SURVEY_USER;
DROP TABLE ANSWER;
 
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
    title CHAR(50)
);

CREATE TABLE QUESTION_CHOICE (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    letter_response CHAR(50),
    id_question INTEGER NOT NULL,
    response CHAR(50) NOT NULL,
    FOREIGN KEY(id_question)
        REFERENCES QUESTION (id)
);

CREATE TABLE SURVEY_QUESTION (
    id INTEGER PRIMARY KEY AUTOINCREMENT, 
    id_survey INTEGER NOT NULL, 
    id_question INTEGER NOT NULL,
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY (id),
    FOREIGN KEY(id_question)
        REFERENCES QUESTION (id)
);

CREATE TABLE SURVEY_USER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_user INTEGER,
    id_survey INTEGER,
    is_filled INTEGER,
    FOREIGN KEY(id_user) 
        REFERENCES USER (id),
    FOREIGN KEY(id_survey) 
        REFERENCES SURVEY(id)
);

CREATE TABLE ANSWER (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    id_choice INTEGER,
    answer_date CHAR(50),
    FOREIGN KEY(id_choice) 
        REFERENCES QUESTION_CHOICE (id)
);

-- INSERT INTO USER (username, password) 
-- VALUES ('alice', 'password1'),
--        ('bob', 'password2'),
--        ('charlie', 'password3');

-- -- Insertion d'un sondage sans spécifier l'ID
-- INSERT INTO SURVEY (title) 
-- VALUES ('Sondage 1'),
--        ('Sondage 2');

-- -- Insertion de questions
-- INSERT INTO QUESTION (title) 
-- VALUES ('How satisfied are you with our product?'),
--        ('Would you recommend us to others?');

-- -- Insertion de choix de réponse pour les questions
-- INSERT INTO QUESTION_CHOICE (letter_response, id_question, response) 
-- VALUES ('A', 1, 'Very Satisfied'),   -- Réponse pour la question 1
--        ('B', 1, 'Satisfied'),
--        ('C', 1, 'Neutral'),
--        ('D', 1, 'Dissatisfied'),
--        ('A', 2, 'Yes'),              -- Réponse pour la question 2
--        ('B', 2, 'No');

-- -- Associer des questions à un sondage
-- INSERT INTO SURVEY_QUESTION (id_survey, id_question) 
-- VALUES (1, 1),  -- Question 1 pour le sondage 1
--        (1, 2);  -- Question 2 pour le sondage 1

-- -- Associer des utilisateurs à des sondages
-- INSERT INTO SURVEY_USER (id_user, id_survey, is_filled) 
-- VALUES (1, 1, 1),  -- L'utilisateur 1 a rempli le sondage 1
--        (2, 1, 0);  -- L'utilisateur 2 n'a pas rempli le sondage 1

-- -- Insertion de réponses données par des utilisateurs
-- INSERT INTO ANSWER (id_choice, answer_date) 
-- VALUES (1, '2024-09-13'),  -- L'utilisateur a choisi la réponse A pour une question
--        (2, '2024-09-13');  -- L'utilisateur a choisi la réponse B pour une autre question
