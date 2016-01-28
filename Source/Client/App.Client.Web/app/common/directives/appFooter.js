/***********************
 * App Footer Directive
 ***********************/
(function () {
    'use strict';

    angular.module('app.directives')
        .directive('appFooter', appFooter);

    function appFooter() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/views/app-footer.html',
            link: footer
        };

        function footer(scope, element) {
            // TODO: Implement
        }
    }

}());
