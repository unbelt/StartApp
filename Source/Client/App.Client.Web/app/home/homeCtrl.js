/******************
* Home Controller
******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = ['$rootScope', 'entityData', 'logger'];

    function HomeCtrl($rootScope, entityData, logger) {
        var vm = this;

        vm.title = 'Entity Home';
        vm.getAllEntities = getAllEntities;

        getAllEntities();

        function getAllEntities() {
            $rootScope.loading = true;

            entityData.getAllEntities()
                .then(getEntitiesComplete, getEntitiesFailed)
                .finally(onComplete);

            function getEntitiesComplete(response) {
                vm.entities = response;
            }

            function getEntitiesFailed(response) {
                logger.error(response);
            }

            function onComplete() {
                $rootScope.loading = false;
            }
        }
    }

}());
