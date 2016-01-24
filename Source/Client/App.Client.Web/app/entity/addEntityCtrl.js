(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('AddEntityCtrl', AddEntityCtrl);

    AddEntityCtrl.$inject = ['$location', 'entityData', 'logger'];

    function AddEntityCtrl($location, entityData, logger) {
        var vm = this;

        vm.addEntity = addEntity;


        function addEntity(entity) {
            entityData.addEntity(entity)
                .then(onAddSuccess, onAddFailed);
        }


        // PRIVATE METHODS

        function onAddSuccess(response) {
            logger.log(response);
            $location.path('/');
        }

        function onAddFailed(error) {
            logger.error(error.message);
        }
    }

}());