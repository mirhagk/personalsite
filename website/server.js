var static = require('node-static');
var port = process.env.port || 1337;
var http = require('http');
var fs = require('fs');
var util = require('util');

var fileServer = new static.Server('./www');

http.createServer(function (req, res) {
	util.log(req.url);
	if (req.url == '/blog' || req.url == '/blog/index.html'){
		var files = fs.readdirSync('./www/blog');
		
		res.writeHead(200);
		res.end(fs.readFileSync('./www/blog/hello_world.html'));
	}
	fileServer.serve(req, res);
}).listen(port);