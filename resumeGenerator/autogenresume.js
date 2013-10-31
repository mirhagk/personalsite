var sys = require('sys')
var exec = require('child_process').exec;
require('./lib.js');
var parser = require('./parser.js');
require('../website/js/resume.js');

var fs = require('fs');
var contents = fs.readFileSync('../template/resume.tex').toString();
var tokens = parser.tokenize(contents);
console.log(tokens);
console.log('================');
console.log(parser.parse(tokens));