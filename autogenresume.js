var sys = require('sys')
var exec = require('child_process').exec;
require('./website/js/resume.js');
function puts(error, stdout, stderr) { console.log(stdout) }
//exec("dir", puts);

function GetEachTags(text){
    var eachStart = /{{#each ?([^{}]*)}}/g;
    var each = eachStart.exec(text);
}

function FormatTemplate(template, object){
    var result = template;
    var eachRegex = /{{#each ?([^{}]*)}}([\s\S]*?){{\/each}}/g;
    var each = eachRegex.exec(result);
    while(each!=null){
        var array = object[each[1]];
        var section = "";
        for(var i = 0;i<array.length;i++){
            section+=FormatTemplate(each[2],array[i]);
        }
        result = result.substring(0,each.index)+
                    section+
                   template.substring(each.index+each[0].length,result.length);
        console.log(section);
        each = eachRegex.exec(result);
    }
    var matches = template.match(/{{[^#/{}][^{}]*}}/g);
    if (matches!=null){
        for(var i=0;i<matches.length;i++){
            var originalKey = matches[i].substring(2,matches[i].length-2);
            var key = originalKey.substring(0,originalKey.length);
            var obj = object;
            var kip = key.indexOf('.');
            while (kip>-1){
                if (obj!=null)
                    obj =obj[key.substring(0,kip)];
                key = key.substring(kip+1,key.length);
                kip=key.indexOf('.');
            }
            if (obj!=null && obj[key]!=null){
                result = result.replace('{{'+originalKey+'}}',obj[key]);
            }
        }
    }
    //for(var i=0;i<each.length;i++){
        //console.log(each[i]);
    //}
    //console.log(each[0]);
    //console.log(each[1]);
    console.log('-------');
    return result;
}

var fs = require('fs');
var contents = fs.readFileSync('template/resume.tex').toString();
var resume = {thing: 'stuff', test: 'things'}
contents = FormatTemplate(contents,resumeData);
console.log(contents);