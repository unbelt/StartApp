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
            log: $log.log
        };

        return service;

        function success(message, data, title) {
            toastr.success(message, title);
            $log.success('Success: ' + message, data);
        }

        function error(message, data, title) {
            toastr.error(message, title);
            $log.error('Error: ' + message, data);
        }

        function warning(message, data, title) {
            toastr.warning(message, title);
            $log.warning('Warning: ' + message, data);
        }

        function info(message, data, title) {
            toastr.info(message, title);
            $log.info('Info: ' + message, data);
        }
    }

}());
