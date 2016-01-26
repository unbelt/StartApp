/*********************
* UserList Controller
**********************/
(function () {
    'use strict';

    angular.module('app.controllers')
        .controller('UserListCtrl', UserListCtrl);

    UserListCtrl.$inject = ['userData', 'logger'];

    function UserListCtrl(userData, logger) {
        var vm = this;

        vm.getAllUsers = getAllUsers;

        getAllUsers();

        function getAllUsers() {
            userData.getAllUsers()
                .then(onGetUsersSuccess, onGetUsersFailed);
        }


        // PRIVATE FUNCTIONS

        function onGetUsersSuccess(response) {
            vm.users = response;
            logger.log(response);
        }

        function onGetUsersFailed(error) {
            logger.error(error);
        }
    }

}());
