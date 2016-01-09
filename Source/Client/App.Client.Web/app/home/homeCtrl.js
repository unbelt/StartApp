/******************
 * Home Controller
 ******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = ['entityData'];

    function HomeCtrl(entityData) {
        var vm = this;

        vm.title = 'Entity Home';
        vm.getAllEntities = getAllEntities;

        getAllEntities();

        function getAllEntities() {
            entityData.getAllEntities()
                .then(getEntitiesComplete)
                .catch(getEntitiesFailed);

            function getEntitiesComplete(response) {
                vm.entities = response;
            }

            function getEntitiesFailed(response) {
                logger.error(response);
            }
        }
    }

}());
