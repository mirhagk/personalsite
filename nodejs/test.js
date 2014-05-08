var http = require ('http');
http.createServer(function(req,res){
	res.writeHead(200, {'Content-Type': 'text/plain'});
	res.end('hello better squirrel\n');
}).listen(8080);
console.log('Server running');
