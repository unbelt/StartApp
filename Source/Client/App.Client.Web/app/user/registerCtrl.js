/*********************
* Register Controller
**********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('RegisterCtrl', RegisterCtrl);

    RegisterCtrl.$inject = ['$location', 'userData', 'logger'];

    function RegisterCtrl($location, userData, logger) {
        var vm = this;

        vm.register = register;


        function register(user) {
            userData.register(user)
                .then(onRegisterSuccess, onRegisterFailed);
        }


        // PRIVATE FUNCTIONS

        function onRegisterSuccess(response) {
            // TODO: Implement
            $location.path('/');
            logger.log(response);
        }

        function onRegisterFailed(error) {
            logger.error(error.message);
        }
    }

}());
