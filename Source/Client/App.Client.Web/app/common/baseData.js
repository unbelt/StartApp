(function () {
    'use strict';

    angular.module('app.data')
        .factory('data', ['$http', '$q', 'appSettings', baseData]);

    function baseData($http, $q, appSettings) {

        return {
            get: get,
            post: post
        };

        function get(path) {
            var deferred = $q.defer();

            var url = appSettings.serverPath + path;

            $http.get(url)
                .success(function onSuccess(data) {
                    deferred.resolve(data);
                })
                .error(function onError(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        function post(path, data) {
            var deferred = $q.defer();

            var url = appSettings.serverPath + path;

            var headers = {
                'Content-Type': 'application/json'
            };

            $http.post(url, data, headers)
                .success(function onSuccess(data) {
                    deferred.resolve(data);
                })
                .error(function onError(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }
    }

}());