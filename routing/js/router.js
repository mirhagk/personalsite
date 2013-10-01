App.Router.map(function () {
  this.resource('nav', { path: '/' });
  this.resource('page', {path: '/page'});
});

App.NavRoute = Ember.Route.extend({
  model: function () {
    return this.store.find('page');
  }
});
App.PageRoute = Ember.Route.extend({
    model: function() {
        return this.store.filter('page', function (page) {
            return page.get('active');
        });
    }
});