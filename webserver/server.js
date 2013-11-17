var static = require('node-static');
var port = process.env.port || 1337;
var http = require('http');

var fileServer = new static.Server('./www');
var templateServer = new static.Server('./template');

http.createServer(function (req, res) {
	//console.log(req.url);
	if (req.url == '/' || req.url == '/index.html'){
		//console.log('main code');
		templateServer.serveFile('/index.html',200,{},req,res);
	}
	else
		fileServer.serve(req, res);
}).listen(port);