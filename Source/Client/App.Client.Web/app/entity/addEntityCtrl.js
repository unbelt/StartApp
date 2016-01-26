(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('AddEntityCtrl', AddEntityCtrl);

    AddEntityCtrl.$inject = ['$location', 'entityData', 'logger'];

    function AddEntityCtrl($location, entityData, logger) {
        var vm = this;

        vm.addEntity = addEntity;


        function addEntity(entity) {
            entity.userId = '9274425f-805b-46d7-adb5-e8a03ec6729d'; // TODO: For testing porpoise

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