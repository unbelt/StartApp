/*****************
* Main Controller
******************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('MainCtrl', MainCtrl);

    MainCtrl.$inject = ['$location', 'identity'];

    function MainCtrl($location, identity) {
        var vm = this;

        vm.isAuthenticated = identity.isAuthenticated;
        vm.currentUser = identity.getCurrentUser;
        vm.logout = logout;


        function logout() {
            identity.removeCurrentUser();
            $location.path('/');
        }
    }

}());
