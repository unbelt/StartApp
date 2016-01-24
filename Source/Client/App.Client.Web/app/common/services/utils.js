/****************
 * Utils Service
 ****************/
(function () {
    'use strict';

    angular.module('app.services')
        .factory('utils', utils);

    utils.$inject = ['logger'];

    function utils(logger) {

        var service = {
            stringFormat: stringFormat
        };

        return service;


        function stringFormat(string, separator, isLowerCase) {

            var formated = string.replace(/[^0-9a-z-]/g, separator);

            if (isLowerCase) {
                return formated.toLowerCase();
            }

            return formated;
        }
    }

}());
