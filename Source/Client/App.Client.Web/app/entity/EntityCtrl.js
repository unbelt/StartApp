(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EntityCtrl', ['$routeParams', 'entityData', entityCtrl])

    function entityCtrl($routeParams, entityData) {
        var vm = this;

        if ($routeParams.id) {
            entityData.getEntity($routeParams.id)
            .then(function (response) {
                console.log(response);
                //vm.entity = response;
            });
        } else {
            // TODO: Get all
        }
    }

}());