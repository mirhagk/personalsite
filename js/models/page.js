Home.Page = DS.Model.extend({
  title: DS.attr('string'),
  active: DS.attr('boolean')
});

Home.Page.FIXTURES = [
 {
   id: 1,
   title: 'Home',
   active: true
 },
 {
   id: 2,
   title: 'Education',
   active: false
 },
 {
   id: 3,
   title: 'Projects',
   active: false
 }
];