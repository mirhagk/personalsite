//Splits the input into tokens, basically either text or hanlebar sections
exports.tokenizer = function(text){
	var handlebar = /{{[^{}]*}}/;
	var next = handlebar.exec(text);
	var tokens = [];
	console.log(next);
	console.log(next[0]);
	console.log(next.index);
	console.log('=============');
	while(next!=null){
		tokens.push({
			type: "content", 
			text: text.substr(0,next.index)
		});
		var token = next[0];
		token = token.substr(2,token.length-4);
		console.log(token);
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
