//Splits the input into tokens, basically either text or hanlebar sections
exports.tokenizer = function(text){
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
		//tokens.push(next[0]);
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
exports.parser = function(tokens, field){
	field = field || "root";
	var root = {children:[]};
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
		}
		else
			root.children.push(token);
	}
	return root;
}
exports.tpar