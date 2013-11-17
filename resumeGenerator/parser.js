function EscapeInput(text){
	var result = text.replace("#","\\#");
	return result;
}

//Splits the input into tokens, basically either text or hanlebar sections
function Tokenize(text){
	var handlebar = /{{[^{}]*}}/;
	var next = handlebar.exec(text);
	var tokens = [];
	while(next!=null){
		tokens.push({
			type: "content", 
			text: text.substr(0,next.index)
		});
		var token = next[0];
		token = token.substr(2,token.length-4);
		switch(token[0])
		{
			case '#':
				if (token.startsWith("#each "))
					tokens.push({
						type: "eachBegin",
						text: token.substr("#each ".length)
					});
				break;
			case '/':
				if (token.startsWith("/each"))
					tokens.push({
						type: "eachEnd"
					});
				break;
			default:
				tokens.push({
					type: "replacement",
					text: token
				});
				break;
		}
		text = text.substr(next.index+next[0].length);
		next = handlebar.exec(text);
	}
	if (text.length!=0)
		tokens.push({
			type: "content", 
			text: text
		});
	return tokens;
}
function Parse(tokens, field){
	var root = {children:[], field: field || "root", type:"parseTree"};
	for(var i = 0;i<tokens.length;i++){
		var token = tokens[i];
		if (token.type=="eachBegin"){
			var eachCount = 1;
			var subtokens=[];
			while(eachCount>0){
				i++
				if (tokens[i].type=="eachBegin")
					eachCount++;
				else if (tokens[i].type=="eachEnd")
					eachCount--;
				subtokens.push(tokens[i]);
			}
			//remove the last eachEnd
			subtokens.pop();
			//Parse the section inbetween the each tokens
			var inner = Parse(subtokens,token.text);
			//and add the parsed object to the children
			root.children.push(inner);
		}
		else
			root.children.push(token);
	}
	return root;
}
function FormatEach(tree, objects){
	if (objects == null)
		return;
	var result = "";
	for(var i=0;i<objects.length;i++){
		result += Format(tree,objects[i]);
	}
	return result;
}
function GetFromObject(object, field){
	var index = field.indexOf('.')
	if (index<0)//no subfields
		return object[field];
	var start = field.substr(0,index);
	var end = field.substr(index+1);
	return GetFromObject(object[start],end);
}
function Format(tree, object){
	var result = "";
	for(var i =0;i<tree.children.length;i++){
		var token = tree.children[i];
		if (token.type == "parseTree"){//handle parse tree
			result+=FormatEach(token,object[token.field]);
		}
		else{
			if (token.type=="content")
				result += token.text;
			else if (token.type=="replacement"){
				result += EscapeInput(GetFromObject(object,token.text));
			}
		}
	}
	return result;
}
exports.parse = Parse;
exports.tokenize = Tokenize;
exports.format = function(text,object){
	return Format(Parse(Tokenize(text)),object);
};