var sys = require('sys')
var exec = require('child_process').exec;
require('./lib.js');
var parser = require('./parser.js');
require('../webserver/www/js/resume.js');

var fs = require('fs');
var contents = fs.readFileSync('../template/resume.tex').toString();
var tokens = parser.tokenize(contents);
fs.writeFileSync('resume.tex',parser.format(contents,resumeData));
console.log('done');