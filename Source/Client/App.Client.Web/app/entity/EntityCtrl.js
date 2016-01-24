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
        vm.deleteEntity = deleteEntity;

        if (Number($routeParams.id)) {
            getEntity($routeParams.id);
        }

        function getEntity(id) {
            entityData.getEntity(id)
                .then(onGetSuccess, onGetFail);
        }

        function deleteEntity(id) {
            entityData.deleteEntity(id)
                .then(onDeleteSuccess, onDeleteFailed);
        }


        // PRIVATE METHODS

        // GET
        function onGetSuccess(response) {
            logger.log(response);
            vm.entity = response;
        }

        function onGetFail(error) {
            logger.error(error.message);
            $location.path('/404-not-found');
        }


        // DELETE
        function onDeleteSuccess() {
            $location.path('/');
        }

        function onDeleteFailed(error) {
            logger.error(error.message);
        }
    }

}());
