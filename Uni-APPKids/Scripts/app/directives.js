// Define you directives here. Directives can be added to same module as 'main' or a seperate module can be created.

var angularStartDirectives = angular.module('angularStart.directives', []);     //Define the directive module

angularStartDirectives.directive('testDirective', function () {             //use as 'test-directive' in HTML
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            console.log('Directive linked.');
        }
    };
});
angularStartDirectives.directive('myDirective', function() {

    var uniqueId = 1;
    return {
        restrict: 'E',
        scope: true,
        template: '<input type="checkbox"/><label>open</label>',
        link: function(scope, elem, attrs) {
            var item = 'item' + uniqueId++;
            elem.find('input').attr('id' , item);
            elem.find('label').attr('for', item);
        }
    }
})
