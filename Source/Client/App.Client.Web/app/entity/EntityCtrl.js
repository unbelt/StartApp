/********************
 * Entity Controller
 ********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EntityCtrl', EntityCtrl);

    EntityCtrl.$inject = ['$routeParams', '$location', 'entityData', 'utils', 'logger'];

    function EntityCtrl($routeParams, $location, entityData, utils, logger) {
        var vm = this;

        vm.getEntity = getEntity;
        vm.addEntity = addEntity;
        vm.updateEntity = updateEntity;
        vm.deleteEntity = deleteEntity;

        if (Number($routeParams.id)) {
            entityData.getEntity($routeParams.id)
                .then(onGetSuccess, onFail);
        }

        function getEntity(id) {
            entityData.getEntity(id)
                .then(onGetSuccess, onFail);
        }

        function addEntity(entity) {
            entityData.addEntity(entity)
                .then(onAddSuccess, onFail);
        }

        function updateEntity(entity) {
            // TODO: Implement
        }

        function deleteEntity(id) {
            entityData.deleteEntity(id)
                .then(onDeleteSuccess, onFail);
        }


        // PRIVATE METHODS

        // GET
        function onGetSuccess(response) {
            vm.entity = response;
        }

        // ADD
        function onAddSuccess(response) {
            $location.path('/');
        }

        // UPDATE
        function onUpdateSuccess(response) {
            $location.path('/');
        }

        // DELETE
        function onDeleteSuccess() {
            $location.path('/');
        }

        function onFail(error) {
            logger.error(error.message);
            $location.path('/404-not-found');
        }
    }

}());
