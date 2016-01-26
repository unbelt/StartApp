/******************
* UserData Service
*******************/
(function () {
    'use strict';

    angular.module('app.services')
        .factory('userData', userData);

    userData.$inject = ['data'];

    function userData(data) {

        var service = {
            getAllUsers: getAllUsers,
            getUserByUserName: getUserByUserName,
            login: login,
            register: register
        };

        return service;


        function getAllUsers() {
            return data.get('account/getall');
        }

        function getUserByUserName(username) {
            return data.get('account/get/' + username);
        }

        function login(user) {
            return data.post('account/login', user);
        }

        function register(user) {
            return data.post('account/register', user);
        }
    }

}());
