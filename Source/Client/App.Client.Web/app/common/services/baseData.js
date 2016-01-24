/********************
 * Base Data Service
 ********************/
(function () {
    'use strict';

    angular.module('app.data')
        .factory('data', baseData);

    baseData.$inject = ['$http', '$q', 'appSettings'];

    function baseData($http, $q, appSettings) {

        var service = {
            get: get,
            post: post,
            update: update,
            remove: remove
        };

        return service;

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

        function update(url, data) {
            // TODO: Implement
        }

        function remove(path) {
            var deferred = $q.defer();

            var url = appSettings.serverPath + path;

            $http.delete(url)
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
