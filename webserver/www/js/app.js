App = Ember.Application.create({});

App.Router.map(function() {
  this.resource('about');
  this.resource('projects');
  this.resource('experience');
  this.resource('education');
  this.resource('ad');
  this.resource('posts', function() {
    this.resource('post', { path: ':post_id' });
  });
});

App.ExperienceRoute = Ember.Route.extend({
    model: function(){
        return experience;
    }
});

App.EducationRoute = Ember.Route.extend({
	model: function(){
		return education;
	}
});

App.PostsRoute = Ember.Route.extend({
  model: function() {
    return posts;
  }
});

App.PostRoute = Ember.Route.extend({
  model: function(params) {
    return posts.findBy('id', params.post_id);
  }
});

App.PostController = Ember.ObjectController.extend({
  isEditing: false,

  edit: function() {
    this.set('isEditing', true);
  },

  doneEditing: function() {
    this.set('isEditing', false);
    this.get('store').commit();
  }
});

var showdown = new Showdown.converter();

Ember.Handlebars.helper('format-markdown', function(input) {
  return new Handlebars.SafeString(showdown.makeHtml(input));
});

Ember.Handlebars.helper('format-date', function(date) {
  return moment(date).fromNow();
});