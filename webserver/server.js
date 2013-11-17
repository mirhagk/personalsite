var static = require('node-static');
var port = process.env.port || 1337;
var http = require('http');
var fs = require('fs');
var util = require('util');


const templateFolder = 'template/';
var fileServer = new static.Server('./www');


function CreateSite(path, res){
	var files = fs.readdirSync(templateFolder+'pages/'+path);
	for(var i=0;i<files.length;i++){
		if (files[i].match(/\.handlebars$/g)){//handlebar file
			var file = templateFolder+'pages/'+path+files[i];
			var part = '<script type="text/x-handlebars" id="'+path+files[i].replace(/\.handlebars$/,"")+'">\n' 
						+ fs.readFileSync(file)
						+ '\n</script>\n';
			res.write(part);
		}
		else if (files[i].match(/^[^\.]*$/g)){//folder
			CreateSite(path+files[i]+'/',res);
		}
		else{//we have an issue here, log it
			util.error('Found '+path+files[i]+'which isn\'t a handlebar file or folder');
		}
	}
}

http.createServer(function (req, res) {
	if (req.url == '/' || req.url == '/index.html'){
		var files = fs.readdirSync(templateFolder+'pages');
		res.writeHead(200);
		res.write(fs.readFileSync(templateFolder+'start.html'));
		CreateSite('',res);
		res.end(fs.readFileSync(templateFolder+'end.html'));
	}
	else
		fileServer.serve(req, res);
}).listen(port);