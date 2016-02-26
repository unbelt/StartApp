/************************************
 * HTTP Response Interceptor Service
 ************************************/
(function () {
    'use strict';

    angular.module('app')
        .factory('httpResponseInterceptor', httpResponseInterceptor);

    httpResponseInterceptor.$inject = ['$q', '$location', 'logger'];

    function httpResponseInterceptor($q, $location, logger) {

        var service = {
            'response': response,
            'responseError': responseError
        };

        return service;


        function response(response) {
            if (response.data.success !== undefined) {
                if (response.data.success === true) {
                    response.data = response.data.data;
                } else if (response.data.success === false) {
                    if (response.data.errorMessage) {
                        $location.path('/');
                    } else {
                        logger.error(response.data.errorMessage);
                    }

                    return $q.reject(response);
                }
            }

            return response;
        }

        function responseError(rejection) {
            if (rejection.data && rejection.data.error_description) {
                logger.error(rejection.data.error_description);
            } else {
                logger.error('Check your internet connection.', null, 'Error loading the data!');
            }

            return $q.reject(rejection);
        }
    }

}());
