from flask import Flask,jsonify,request

app = Flask(__name__)

data = {"name":"John", "age":30, "city":"New York"}

@app.route('/post', methods=['POST'])
def post():
    json = request.get_json()
    global data
    data= json
    print(data,json,request.content_length)
    print()
    return jsonify(data)

@app.route('/get',methods=['GET'])
def get():
    return  str(data).replace("'",'"') 

if __name__ == '__main__':
    app.run(debug=True,host='0.0.0.0',threaded=True)