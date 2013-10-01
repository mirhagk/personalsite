App = Ember.Application.create();

posts = [{
  title: "Rails is omakase",
  body: "There are lots of à la carte software environments in this world.",
  active: true
},{
  title: "Rails is dedew",
  body: "There are lots of à la deded software environments in this world.",
  active: true
},
{
  title: "Broken Promises",
  body: "James Coglan wrote a lengthy article about Promises in node.js.",
  active: false
}];

App.IndexRoute = Ember.Route.extend({
  model: function() {
    return posts.filter(function(data){
        return data.active;
    }); 
  }
});