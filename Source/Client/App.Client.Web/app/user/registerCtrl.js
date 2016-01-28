/*********************
* Register Controller
**********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('RegisterCtrl', RegisterCtrl);

    RegisterCtrl.$inject = ['$location', 'userData', 'identity', 'logger'];

    function RegisterCtrl($location, userData, identity, logger) {
        var vm = this;

        vm.register = register;


        function register(user) {
            userData.register(user)
                .then(onRegisterSuccess, onRegisterFailed);
        }


        // PRIVATE FUNCTIONS

        function onRegisterSuccess(response) {
            identity.setCurrentUser(response);
            logger.log(response);
            $location.path('/');
        }

        function onRegisterFailed(error) {
            logger.error(error.message);
        }
    }

}());
