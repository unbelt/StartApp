/*************************
 * Edit Entity Controller
 *************************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('EditEntityCtrl', EditEntityCtrl);

    EditEntityCtrl.$inject = ['$routeParams', '$location', 'entityData', 'logger'];

    function EditEntityCtrl($routeParams, $location, entityData, logger) {
        var vm = this;

        vm.editEntity = editEntity;

        if (Number($routeParams.id)) {
            entityData.getEntity($routeParams.id)
                .then(onGetSuccess, onGetFailed);
        }

        function editEntity(entity) {
            entityData.editEntity(entity)
                .then(onEditSuccess, onEditFailed);
        }


        // PRIVATE METHODS

        // GET
        function onGetSuccess(response) {
            vm.entity = response;
        }

        function onGetFailed(error) {
            logger.error(error.message);
            $location.path('/404-not-found');
        }

        // EDIT
        function onEditSuccess(response) {
            logger.log(response);
            $location.path('/');
        }

        function onEditFailed(error) {
            logger.error(error.message);
        }
    }

}());
