import sqlite3

with open('create_tables.sql', 'r') as sql_file:
    sql_script = sql_file.read()

db = sqlite3.connect('GeneralSurvey.db')
cursor = db.cursor()

# cursor.execute('SELECT key, id_user FROM API_KEY WHERE key = "638bb43e-35f4-479f-8dbe-6b77ced30c14"')

# cursor.execute("INSERT INTO User (username, password, salt) VALUES ('yo', 'EED52C2B30555C72124CA5D20614569C5B25B0952913819C84B3E71361596C734539E48018ADFAA1463E9BE07DDEDB93FE96E83E70CBBFC3A2897AA7DB686DA1', 'AED837FAE1E3032EDF7F98B94C4A71EA1973BA8140743EEC9ED54BF8BD2474DD5FFEFD4647DE4A884FF923364B0C2BBFE6B2752DC08C9E4D5530B99ED097470F')")
# print(cursor.fetchall())
cursor.executescript(sql_script)
db.commit()
db.close()