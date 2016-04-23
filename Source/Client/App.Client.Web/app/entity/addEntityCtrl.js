/************************
 * Add Entity Controller
 ************************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('AddEntityCtrl', AddEntityCtrl);

    AddEntityCtrl.$inject = ['$location', 'entityData', 'identity', 'logger'];

    function AddEntityCtrl($location, entityData, identity, logger) {
        var vm = this;

        vm.addEntity = addEntity;

        function addEntity(entity) {
            var currentUser = identity.getCurrentUser();

            entity.userId = currentUser.userName;

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