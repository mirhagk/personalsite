Bliss = require("bliss");
fs = require('fs');
bliss = new Bliss();


function Title(file){
	return capitaliseFirstLetter(file.match(/[^\.]*/)[0]);
}
function capitaliseFirstLetter(string)
{
    return string.charAt(0).toUpperCase() + string.slice(1);
}

template = bliss.compileFile('src/layout');
var pages = fs.readdirSync('src/pages');
console.log(pages);
//var pages = ["home","about"];
pages.forEach(function(page){
	var things = template(Title(page),function(){
		return bliss.render("src/pages/"+page);
	});
	console.log(things);
	fs.writeFileSync('www/'+Title(page)+'.html',things);
});