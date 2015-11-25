(function () {
    'use strict';

    angular.module('app', [])
        .config([config])
        .run([run])
        .value('jQuery', jQuery)
        .constant('appSettings', {
            appName: 'StartApp',
        });

    function config() {

    }

    function run() {

    }

}());