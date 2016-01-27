/*************************
 * Cookie Storage Service
 *************************/
(function () {
    'use strict';

    angular.module('app.services')
        .factory('cookieStorage', cookieStorage);

    cookieStorage.$inject = ['$cookieStore'];

    function cookieStorage($cookieStore) {

        var services = {
            getCookie: getCookie,
            setCookie: setCookie,
            removeCookie: removeCookie
        };

        return services;


        function getCookie(key) {
            return $cookieStore.get(key);
        }

        function setCookie(key, value) {
            $cookieStore.put(key, value);
        }

        function removeCookie(key) {
            $cookieStore.remove(key);
        }
    }

}());
