/********************
 * Entity Controller
 ********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EntityCtrl', EntityCtrl);

    EntityCtrl.$inject = ['$routeParams', '$location', 'entityData', 'logger'];

    function EntityCtrl($routeParams, $location, entityData, logger) {
        var vm = this;

        vm.addEntity = addEntity;
        vm.updateEntity = updateEntity;
        vm.deleteEntity = deleteEntity;


        if (Number($routeParams.id)) {
            entityData.getEntity($routeParams.id)
                .then(onGetSuccess, onFail);
        }

        function addEntity(entity) {
            entityData.addEntity(entity)
                .then(onAddSuccess, onFail);
        }

        function updateEntity(entity) {
            // TODO: Implement
        }

        function deleteEntity() {
            // TODO: Implement
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
        function onDeleteSuccess(response) {
            $location.path('/');
        }

        function onFail(error) {
            logger.error(error);
        }
    }

}());
