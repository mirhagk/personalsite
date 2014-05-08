import ServerModule = require('./server');
import TemplateModule = require('./template');
var server = new ServerModule.Server();
server.Start();
console.log('Server running');
console.log(TemplateModule.CompileFile('index.tpl')());