Bliss = require("bliss");
fs = require('fs');
bliss = new Bliss();


function Title(file){
	return capitaliseFirstLetter(file.match(/[^\.]*/)[0]);
}
function capitaliseFirstLetter(string){
    return string.charAt(0).toUpperCase() + string.slice(1);
}


function CompileFolder(folder,outputFolder){
	var template = bliss.compileFile(__dirname+'/src/'+folder+'.layout');
	var items = fs.readdirSync(__dirname + '/src/'+folder);
	console.log('compiling '+folder+' found items: ',items);
	items.forEach(function(item){
		var things = template(Title(item),function(){
			return bliss.render(__dirname+'/src/'+folder+'/'+item);
		});
		fs.writeFileSync(__dirname+'/www/'+outputFolder+Title(item)+'.html',things);
	});
}

CompileFolder('pages','');
CompileFolder('blog','blog');

/*
template = bliss.compileFile(__dirname+'/src/pages.layout');
var pages = fs.readdirSync(__dirname+'/src/pages');
console.log('found pages: ', pages);

pages.forEach(function(page){
	var things = template(Title(page),function(){
		return bliss.render(__dirname+"/src/pages/"+page);
	});
	fs.writeFileSync(__dirname+'/www/'+Title(page)+'.html',things);
});*/