(function () {
    'use strict';

    angular.module('app.data')
        .factory('entityData', entityData);

    entityData.$inject = ['data'];

    function entityData(data) {

        var service = {
            getEntity: getEntity,
            getAllEntities: getAllEntities
        };

        return service;

        function getEntity(id) {
            return data.get('entity/get/' + id);
        }

        function getAllEntities() {
            return data.get('entity/getall');
        }
    }

}());
