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
            getAuthorizationHeaders: getAuthorizationHeaders,
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

        function getAuthorizationHeaders() {
            if (getCurrentUser()) {
                return {
                    'Authorization': 'Bearer ' + getCurrentUser()
                };
            }
        }

        function isAuthenticated() {
            return !!getCurrentUser();
        }
    }

}());
