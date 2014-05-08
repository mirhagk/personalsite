import fs = require('fs');
function OutputLine(text: string):string{
	return "Write('"+text.slice(1,-1)+"');";
};
export function CompileFile(filename: string):string{
	var data = fs.readFileSync(filename, {encoding: 'utf8'});
	return CompileTemplate(data);
};
export function CompileLine(text: string){
	text = text.trimRight('\r');
	var endLine : boolean = text[text.length-1]=='\\';
	var result = "";
	var expressionRegex = /@{[^}]}/;
	var regexResult = expressionRegex.execute(text);
	var index = 0;
	while(regexResult){
		
	}
	return OutputLine(text);
};
export function CompileTemplate(data: string){
		var lines = data.split('\n');
		lines = lines.map((x)=>{
			if (x.length==0)
				return "";
			if (x[0] == ".")
				return CompileLine(x);
			return x;
		});
		var text = "function Write(text){coonsole.log(text);};\n";
		text = text + lines.join('\n');
		console.log(text);
		var result = new Function(text);
		return result;
};
function Indent(text:string, level:number = 1){
	return text;
};