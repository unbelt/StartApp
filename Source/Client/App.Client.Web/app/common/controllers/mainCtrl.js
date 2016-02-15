/*****************
* Main Controller
******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('MainCtrl', MainCtrl);

    MainCtrl.$inject = ['$location', '$window', 'identity', 'logger'];

    function MainCtrl($location, $window, identity, logger) {
        var vm = this;

        vm.isAuthenticated = identity.isAuthenticated;
        vm.currentUser = identity.getCurrentUser;
        vm.logout = logout;


        function logout() {
            identity.removeCurrentUser();
            logger.info('You have logged out successfully!');
            $window.history.back();
        }
    }

}());
