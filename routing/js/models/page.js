App.Page = DS.Model.extend({
  title: DS.attr('string'),
  active: DS.attr('boolean')
});

App.Page.FIXTURES = [
 {
   id: 1,
   title: 'Home',
   active: true,
   content: 'test'
 },
 {
   id: 2,
   title: 'Education',
   active: false,
   content: 'test'
 },
 {
   id: 3,
   title: 'Projects',
   active: false,
   content: 'test'
 }
];