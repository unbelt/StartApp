(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('HomeCtrl', [homeCtrl]);

    function homeCtrl() {
        var vm = this;

        vm.title = 'Home';

        // TODO: Implement
    }

}());