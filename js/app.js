App = Ember.Application.create({});

var experience = [{
    name: 'Population Health Research Institute',
    location: 'Hamilton, ON',
    title: 'Software Developer',
    tech: [{name: 'C#'},{name: 'ExtJS'}],
    respon: [{text: ''}],
    achiev: [{text: ''}]
}];

var posts = [{
  id: 'hello_world',
  title: "Hello World",
  author: { name: "mirhagk" },
  date: new Date('2013-10-01 11:46 '),
  excerpt: "This is my first blog post.",
  body: "Hello Blog! If you are reading this then you are one of the lucky people who gets to look at my website. Isn't it pretty? Well not really, but there are soon to be some major enhancements so stay tuned"
}, {
  id: 'ember',
  title: "On Ember",
  author: { name: "mirhagk" },
  date: new Date('2013-10-01 11:46'),
  excerpt: "A quick post about Ember JS",
  body: "For this site I have decided to use Ember JS, and to make the entire site without a backend. That means that it will work in any file server, which will make it cheap to run. In order to make it without a backend, and not pull my hair out, I needed a front-end MVC framework. I've only ever worked with AngularJS so I decided to try out EmberJS, and this website is my experiment with it. I'm not going to lie, it's not really that easy to understand what's actually going on, but once everything is understood, it is *very* powerful. I like it"  
}, {
	id: 'licenses',
	title: 'Software Licenses',
	author: { name: "mirhagk" },
	date: new Date('2013-10-01 13:49'),
	excerpt: 'Explaining open source software licenses',
	body: 'Open source software licenses can be very tricky to understand, and most people just randomly select one without knowing the consequences of the license. Hopefully this page will clear up a little of the confusion.'
}];

App.Router.map(function() {
  this.resource('about');
  this.resource('projects');
  this.resource('experience');
  this.resource('education');
  this.resource('posts', function() {
    this.resource('post', { path: ':post_id' });
  });
});

App.ExperienceRoute = Ember.Route.extend({
    model: function(){
        return experience;
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
