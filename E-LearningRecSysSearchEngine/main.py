import os

from flask import Flask, jsonify, request, g
from flask_restful import Resource, Api, reqparse, abort, marshal, fields
import pyodbc
import pandas as pd
import spacy
from tqdm import tqdm
from rank_bm25 import BM25Okapi
import csv
import xlsxwriter


def init_db():
    conn = pyodbc.connect('Driver={ODBC Driver 17 for SQL Server};'
                          'Server=(localdb)\mssqllocaldb;'
                          'Database=RecSysApiDb;'
                          'Trusted_Connection=yes;')
    return conn


def get_db():
    if 'db' not in g:
        g.db = init_db()
    return g.db


def saveVideosTextDataAsCsv(db_conn):
    courses_csv_file_header = ['Name', 'LargeDescription', 'SmallDescription']
    courses_csv_file = open('courses-data.csv', 'w', encoding='UTF8')

    if os.path.isfile('courses-data.xlsx'):
        os.remove('courses-data.xlsx')
    workbook = xlsxwriter.Workbook('courses-data.xlsx')
    worksheet = workbook.add_worksheet()

    row = 0
    column = 0
    for header in courses_csv_file_header:
        worksheet.write(row, column, header)
        column += 1

    cursor = db_conn.execute("SELECT * FROM dbo.courses")
    columns = [column[0] for column in cursor.description]
    courses = cursor.fetchall()

    selected_columns = []
    for column in courses_csv_file_header:
        selected_columns.append(columns.index(column))

    row = 1
    column = 0
    for course in courses:
        for index, course_column in enumerate(course):
            if index in selected_columns:
                worksheet.write(row, column, course_column)
                column += 1
        row += 1
        column = 0

    print(columns)
    print(courses)
    # DF = pd.read_sql_query("SELECT * FROM dbo.courses", db_conn).to_csv('courses-data.csv', index=False)
    courses_csv_file.close()
    workbook.close()


saveVideosTextDataAsCsv(init_db())


def preprocess():
    pd.set_option('display.max_colwidth', -1)
    df = pd.read_excel('courses-data.xlsx', engine='openpyxl')
    # python -m spacy download en_core_web_sm
    # python -m spacy download en
    nlp = spacy.load("en_core_web_sm")
    tok_text = []  # for our tokenised corpus
    # Tokenising using SpaCy:
    for doc in tqdm(nlp.pipe(df.columns, disable=["tagger", "parser", "lemmatizer", "ner"])):
        tok = [t.text for t in doc if t.is_alpha]
        tok_text.append(tok)
    return tok_text, df


(tok_text, df) = preprocess()


def processSearchQuery(query):
    bm25 = BM25Okapi(tok_text)
    tokenized_query = query.lower().split(" ")
    import time

    t0 = time.time()
    results = bm25.get_top_n(tokenized_query, df.values, n=3)
    t1 = time.time()
    print(f'Searched 50,000 records in {round(t1 - t0, 3)} seconds \n')
    for i in results:
        print(i)


processSearchQuery("java")


class Video(Resource):
    def __init__(self):
        self.reqparse = reqparse.RequestParser()
        self.reqparse.add_argument("keywords", type=str, help="", location="json")
        super(Video, self).__init__()

    def post(self):
        args = request.get_json(force=True)
        keywords = args["keywords"]
        return "queryResult"


app = Flask(__name__)
with app.app_context():
    conn = init_db()
api = Api(app)
api.add_resource(Video, "/videos")

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5005)
