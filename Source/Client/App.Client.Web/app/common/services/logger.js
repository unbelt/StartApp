/*****************
 * Logger Service
 *****************/
(function () {
    'use strict';

    angular.module('app.services')
        .factory('logger', logger);

    logger.$inject = ['$log', 'toastr'];

    function logger($log, toastr) {

        var service = {
            success: success,
            error: error,
            warning: warning,
            info: info,
            log: $log.log,
            print: print
        };

        return service;

        function success(message, data, title) {
            print(message, data, title, 'success', 'log');
        }

        function error(message, data, title) {
            print(message, data, title, 'error', 'error');
        }

        function warning(message, data, title) {
            print(message, data, title, 'warning', 'warn');
        }

        function info(message, data, title) {
            print(message, data, title, 'info', 'info');
        }

        function print(message, data, title, toastrType, logType) {
            if (message) {

                if (toastrType) {
                    toastr[toastrType](message, title);
                }

                if (logType) {
                    $log[logType](logType.toUpperCase() + ': ' + (title || message), data || '');
                }
            }
        }
    }

}());
