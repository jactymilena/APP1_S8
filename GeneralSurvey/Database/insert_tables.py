# import sqlite3

# # Connexion à la base de données (création si elle n'existe pas)
# conn = sqlite3.connect('database.db')
# cursor = conn.cursor()

# # Fonction pour insérer des données dans la table USER
# def insert_users():
#     users = [
#         ('alice', 'password1'),
#         ('bob', 'password2'),
#         ('charlie', 'password3')
#     ]
#     cursor.executemany('INSERT INTO USER (username, password) VALUES (?, ?)', users)

# # Fonction pour insérer des données dans la table SURVEY
# def insert_surveys():
#     surveys = [
#         ('Sondage 1',), 
#         ('Sondage 2',)
#     ]
#     cursor.executemany('INSERT INTO SURVEY (title) VALUES (?)', surveys)


# # Fonction pour insérer des données dans la table QUESTION
# def insert_question():
#     question = [
#         ("À quelle tranche d'âge appartenez-vous?",),  # Ajout de la virgule pour former un tuple
#         ('Êtes-vous une femme ou un homme?',),
#         ('Quel journal lisez-vous à la maison?',),
#         ('Combien de temps accordez-vous à la lecture de votre journal quotidiennement?',),
#         ("À quelle tranche d'âge appartenez-vous?",),
#         ('Êtes-vous une femme ou un homme?',),
#         ('Combien de tasses de café buvez-vous chaque jour?',),
#         ('Combien de consommations alcoolisées buvez-vous chaque jour?',)
#     ]
#     cursor.executemany('INSERT INTO QUESTION (title) VALUES (?)', question)

# # Fonction pour insérer des données dans la table SURVEY_QUESTION
# def insert_survey_question():
#     survey_question = [
#         (1, 1),  # Survey 1, Question 1
#         (1, 2),  # Survey 1, Question 2
#         (1, 3),  # Survey 1, Question 3
#         (1, 4),  # Survey 1, Question 4
#         (2, 5),  # Survey 2, Question 1
#         (2, 6),  # Survey 2, Question 2
#         (2, 7),  # Survey 2, Question 3
#         (2, 8)   # Survey 2, Question 4 
#     ]
#     cursor.executemany('INSERT INTO SURVEY_QUESTION (id_survey, id_question) VALUES (?, ?)', survey_question)

# # Fonction pour insérer des données dans la table SURVEY_USER
# def insert_survey_users():
#     survey_users = [
#         (1, 1, 0),  # User 1  Survey 1 did not fill
#         (1, 2, 0),  # User 1  Survey 2 did not fill
#         (2, 1, 0),  # User 2  Survey 1 did not fill
#         (2, 2, 0),  # User 2  Survey 2 did not fill
#         (3, 1, 0),   # User 3  Survey 1 did not fill
#         (3, 2, 0)   # User 3  Survey 2 did not fill
#     ]
#     cursor.executemany('INSERT INTO SURVEY_USER (id_user, id_survey, is_filled) VALUES (?, ?, ?)', survey_users)

# # Exécuter les fonctions pour insérer les données
# insert_users()
# insert_surveys()
# insert_question()
# insert_survey_question()
# insert_survey_users()

# # Commit les changements et ferme la connexion
# conn.commit()
# conn.close()

# print("Base de données peuplée avec succès !")

import sqlite3

with open('create_tables.sql', 'r') as sql_file:
    sql_script = sql_file.read()

db = sqlite3.connect('GeneralSurvey.db')
cursor = db.cursor()
cursor.executescript(sql_script)
db.commit()
db.close()