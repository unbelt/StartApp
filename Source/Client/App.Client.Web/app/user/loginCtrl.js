/*******************
* Login Controller
*******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$location', 'userData', 'logger'];

    function LoginCtrl($location, userData, logger) {
        var vm = this;

        vm.login = login;


        function login(user) {
            userData.login(user)
                .then(onLoginSuccess, onLoginFailed);
        }

        
        // PRIVATE FUNCTIONS

        function onLoginSuccess(response) {
            // TODO: Implement
            $location.path('/');
            logger.log(response);
        }

        function onLoginFailed(error) {
            logger.error(error.message);
        }
    }

}());
