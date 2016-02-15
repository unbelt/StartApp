/*******************
* Login Controller
*******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$location', '$window', 'userData', 'identity', 'logger'];

    function LoginCtrl($location, $window, userData, identity, logger) {
        var vm = this;

        vm.login = login;


        function login(user) {
            userData.login(user)
                .then(onLoginSuccess, onLoginFailed);
        }

        
        // PRIVATE FUNCTIONS

        function onLoginSuccess(response) {
            identity.setCurrentUser(response);
            logger.log(response);
            $window.history.back();
        }

        function onLoginFailed(error) {
            logger.error(error.message);
        }
    }

}());
