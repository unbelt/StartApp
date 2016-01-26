/******************
* User Controller
******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('UserCtrl', UserCtrl);

    UserCtrl.$inject = ['$routeParams', 'userData', 'logger'];

    function UserCtrl($routeParams, userData, logger) {
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
            logger.error(error);
        }
    }

}());
