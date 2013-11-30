Bliss = require("bliss");
fs = require('fs');
bliss = new Bliss();


function Title(file){
	return capitaliseFirstLetter(file.match(/[^\.]*/)[0]);
}
function capitaliseFirstLetter(string){
    return string.charAt(0).toUpperCase() + string.slice(1);
}

template = bliss.compileFile(__dirname+'/src/layout');
var pages = fs.readdirSync(__dirname+'/src/pages');
console.log('found pages: ', pages);

pages.forEach(function(page){
	var things = template(Title(page),function(){
		return bliss.render(__dirname+"/src/pages/"+page);
	});
	fs.writeFileSync(__dirname+'/www/'+Title(page)+'.html',things);
});