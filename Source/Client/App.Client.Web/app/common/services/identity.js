/*******************
 * Identity Service
 *******************/
(function () {
    'use strict';

    angular.module('app.services')
        .factory('identity', identity);

    identity.$inject = ['cookieStorage'];

    function identity(cookieStorage) {

        var service = {
            setCurrentUser: setCurrentUser,
            getCurrentUser: getCurrentUser,
            removeCurrentUser: removeCurrentUser,
            getAuthorization: getAuthorization,
            isAuthenticated: isAuthenticated
        };

        return service;


        function setCurrentUser(user) {
            cookieStorage.setCookie('currentUser', user);
        }

        function getCurrentUser() {
            return cookieStorage.getCookie('currentUser');
        }

        function removeCurrentUser() {
            cookieStorage.removeCookie('currentUser');
        }

        function getAuthorization() {
            var currentUser = getCurrentUser();

            if (currentUser) {
                return currentUser.token_type + ' ' + currentUser.access_token;
            }
        }

        function isAuthenticated() {
            return !!getCurrentUser();
        }
    }

}());
