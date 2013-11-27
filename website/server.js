var static = require('node-static');
var port = process.env.port || 1337;
var http = require('http');
var fs = require('fs');
var util = require('util');

var fileServer = new static.Server('./www');

http.createServer(function (req, res) {
	util.log(req.url);
	fileServer.serve(req, res);
}).listen(port);