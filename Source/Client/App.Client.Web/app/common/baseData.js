(function () {
    'use strict';

    angular.module('app.data')
        .factory('data', ['$http', '$q', 'appSettings', baseData])

    function baseData($http, $q, appSettings) {

        return {
            get: get,
            post: post
        };

        var headers = {
            'Content-Type': 'application/json'
        };

        function get(url) {
            var deferred = $q.defer();

            var url = appSettings.serverPath + url;

            $http.get(url)
                .success(function onSuccess(data) {
                    deferred.resolve(data);
                })
                .error(function onError(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        function post(url, data) {
            var deferred = $q.defer();

            var url = appSettings.serverPath + url;

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