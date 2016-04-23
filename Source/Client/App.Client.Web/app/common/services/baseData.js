/********************
 * Base Data Service
 ********************/
(function () {
    'use strict';

    angular.module('app.data')
        .factory('data', baseData);

    baseData.$inject = ['$http', '$q', 'identity', 'appSettings'];

    function baseData($http, $q, identity, appSettings) {

        var service = {
            get: get,
            post: post,
            update: update,
            remove: remove
        },

        headers = {
            'Content-Type': 'application/json',
        };

        return service;

        function get(path) {
            var deferred = $q.defer(),
                url = appSettings.serverPath + path;

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
            var deferred = $q.defer(),
                url = appSettings.serverPath + path;

            headers.Authorization = identity.getAuthorization();

            $http.post(url, data, headers)
                .success(function onSuccess(data) {
                    deferred.resolve(data);
                })
                .error(function onError(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        function update(path, data) {
            var deferred = $q.defer(),
                url = appSettings.serverPath + path;

            $http.patch(url, data, headers)
                .success(function onSuccess(data) {
                    deferred.resolve(data);
                })
                .error(function onError(error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        function remove(path) {
            var deferred = $q.defer(),
                url = appSettings.serverPath + path;

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
