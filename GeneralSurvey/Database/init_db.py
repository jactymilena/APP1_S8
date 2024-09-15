import sqlite3

with open('create_tables.sql', 'r') as sql_file:
    sql_script = sql_file.read()

db = sqlite3.connect('GeneralSurvey.db')
cursor = db.cursor()
cursor.executescript(sql_script)
db.commit()
db.close()