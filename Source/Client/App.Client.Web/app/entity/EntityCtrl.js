(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EntityCtrl', ['$routeParams', 'entityData', EntityCtrl]);

    function EntityCtrl($routeParams, entityData) {
        var vm = this;

        if ($routeParams.id) {
            entityData.getEntity($routeParams.id)
                .then(function (response) {
                    vm.entity = response;
                }, function (error) {
                    console.log(error);
                });
        } else { // GET All
            entityData.getAllEntities()
                .then(function (response) {
                    vm.entities = response;
                }, function (error) {
                    console.log(error);
                });
        }
    }

}());