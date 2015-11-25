(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('HomeCtrl', ['$scope', homeCtrl]);

    function homeCtrl($scope) {
        var vm = this;

        $scope.title = 'Home';

        // TODO: Implement
    }

}());