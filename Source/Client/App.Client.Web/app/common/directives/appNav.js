(function () {
    'use strict';

    angular.module('app.directives')
        .directive('appNav', appNav);

    function appNav() {
        return {
            resstrict: 'A',
            templateUrl: '/app/common/views/app-nav.html',
            link: nav
        };

        function nav(scope, element) {
            // TODO: Implement
        }
    }

}());
