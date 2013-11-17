var sys = require('sys')
var exec = require('child_process').exec;
require('../webserver/www/js/resume.js');

var fs = require('fs');

function GenerateLatexFile(data){
	var resume = '\\documentclass[11pt,letterpaper,sans]{moderncv}';
	resume+='\\moderncvstyle{banking}\n\\moderncvcolor{green}';
	resume+='\\usepackage[utf8]{inputenc}\n\\usepackage{multicol}';
	resume+='\\usepackage{verbatim}\n\\usepackage{amssymb,amsmath}';
	resume+='\\usepackage[scale=0.75]{geometry}\n\\setlength{\\hintscolumnwidth}{3cm}';
	resume+='\\firstname{'+data.firstName+'}\n';
	resume+='\\familyname{'+data.lastName+'}\n';
	resume+='\\title{Resume}\n\\address{'+data.address+'}{'+data.postalCode+'}\n';
}

fs.writeFileSync('resume.tex',GenerateLatexFile(resumeData));
console.log('done');