Home.Router.map(function () {
  this.resource('home', { path: '/' });
});

Home.HomeRoute = Ember.Route.extend({
  model: function () {
    //return Ember.$.getJSON('content/page.json');
    return this.store.find('page');
  }
});