(function () {
    'use strict';

    angular.module('app.data')
        .factory('entityData', ['data', entityData]);

    function entityData(data) {

        return {
            getEntity: getEntity,
            getAllEntities: getAllEntities
        };

        function getEntity(id) {
            return data.get('entity/get/' + id);
        }

        function getAllEntities() {
            return data.get('entity/getall');
        }
    }

}());