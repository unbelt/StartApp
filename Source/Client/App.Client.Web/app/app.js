(function () {
    'use strict';

    angular.module('app', ['ngRoute', 'ngCookies', 'app.controllers', 'app.directives'])
        .config(['$locationProvider', '$routeProvider', config])
        .run(['$rootScope', '$location', run])
        .value('jQuery', jQuery)
        .constant('appSettings', {
            serverPath: 'http://localhost:3000/api/',
            appName: 'StartApp',
        });

    angular.module('templates', []);
    angular.module('app.data', []);
    angular.module('app.services', []);
    angular.module('app.controllers', ['app.data', 'app.services']);
    angular.module('app.directives', []);

    function config($locationProvider, $routeProvider) {
        var CONTROLLER_VIEW_MODEL = 'vm';

        $locationProvider.html5Mode(true).hashPrefix('!');

        $routeProvider
            .when('/', {
                templateUrl: '/app/home/home.html',
                controller: 'HomeCtrl',
                controllerAs: CONTROLLER_VIEW_MODEL,
                title: 'Home'
            })
            .when('/entity/getall', {
                templateUrl: '/app/entity/entities.html',
                controller: 'EntityCtrl',
                controllerAs: CONTROLLER_VIEW_MODEL,
                title: 'All Entity'
            })
            .when('/entity/get/:id', {
                templateUrl: '/app/entity/entity.html',
                controller: 'EntityCtrl',
                controllerAs: CONTROLLER_VIEW_MODEL,
                title: 'Entity'
            })
            .otherwise({
                redirectTo: '404-not-found',
                templateUrl: '/app/common/not-found.html',
                title: 'Page Not Found'
            });
    }

    function run($rootScope, $location) {
        $rootScope.$on('$routeChangeSuccess', function routeChangeSuccess(event, current, previous) {
            // TODO: Run on route success
        });

        $rootScope.$on('$routeChangeStart', function routeChangeSuccess(event, current, previous) {
            // TODO: Run on route start
        });
    }

}());