///<reference path="node.d.ts" />
import http = require ('http');
export class Server{
	Start(): void{
		http.createServer(function(req,res){
			res.writeHead(200, {'Content-Type': 'text/plain'});
			res.end('hello better typescript module squirrel\n');
		}).listen(8080);
	}
}