/******************
* User Controller
******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('ProfileCtrl', ProfileCtrl);

    ProfileCtrl.$inject = ['$routeParams', '$location', 'userData', 'identity', 'logger'];

    function ProfileCtrl($routeParams, $location, userData, identity, logger) {
        var vm = this;

        vm.getUser = getUser;


        getUser();

        function getUser() {
            userData.getUserByUserName($routeParams.id)
                .then(onGetUserSuccess, onGetUserFailed);
        }


        // PRIVATE FUNCTIONS

        function onGetUserSuccess(response) {
            vm.user = response;
            logger.log(response);
        }

        function onGetUserFailed(error) {
            logger.error(error.message);
            $location.path('user/list');
        }
    }

}());
