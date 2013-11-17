var static = require('node-static');
var port = process.env.port || 1337;
var http = require('http');
var fs = require('fs');


const templateFolder = 'template/';
var fileServer = new static.Server('./www');
//var templateServer = new static.Server('./template');


function CreateSite(path, res){
	var files = fs.readdirSync(templateFolder+'pages/'+path);
	for(var i=0;i<files.length;i++){
		if (files[i].match(/\.handlebars$/g)){//handlebar file
			var file = templateFolder+'pages/'+path+files[i];
			console.log(file);
			var part = '<script type="text/x-handlebars" id="'+path+files[i].replace(/\.handlebars$/,"")+'">\n' 
						+ fs.readFileSync(file)
						+ '\n</script>\n';
			res.write(part);
		}
		else if (files[i].match(/^[^\.]*$/g)){//folder
			CreateSite(path+files[i]+'/',res);
		}
	}
}

http.createServer(function (req, res) {
	//console.log(req.url);
	if (req.url == '/' || req.url == '/index.html'){
		var files = fs.readdirSync(templateFolder+'pages');
		console.log(files);
		res.writeHead(200);
		res.write(fs.readFileSync(templateFolder+'start.html'));
		CreateSite('',res);
		res.end(fs.readFileSync(templateFolder+'end.html'));
		//fs.readFileSync(templateFolder+'index.html');
		//for(var x=0;x<files.length;x++)
		//{
		//}
		/*fs.readFile(templateFolder+'index.html',function(err,data){
			if (err){
				res.writeHead(404);
				//res.end(JSON.stringify(err));//TODO: Log this somewhere
				res.end('Bad things happened');
				return;
			}
			res.writeHead(200);
			res.end(data);
		});*/
		//console.log('main code');
		//templateServer.serveFile('/index.html',200,{},req,res);
	}
	else
		fileServer.serve(req, res);
}).listen(port);