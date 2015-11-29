(function () {
    'use strict';

    angular.module('app.data')
        .factory('entityData', ['data', entityData])

    function entityData(data) {

        return {
            getEntity: getEntity
        };

        function getEntity(id) {
            return data.get('entity/' + id);
        }
    }

}());