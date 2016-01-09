/********************
 * Entity Controller
 ********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EntityCtrl', EntityCtrl);

    EntityCtrl.$inject = ['$routeParams', 'entityData', 'logger'];

    function EntityCtrl($routeParams, entityData) {
        var vm = this;

        if ($routeParams.id) {
            entityData.getEntity($routeParams.id)
                .then(function (response) {
                    vm.entity = response;
                }, function (error) {
                    console.log(error);
                });
        }
    }

}());
