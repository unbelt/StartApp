(function () {
    'use strict';

    angular.module('app.data')
        .factory('entityData', entityData);

    entityData.$inject = ['data'];

    function entityData(data) {

        var service = {
            getEntity: getEntity,
            getAllEntities: getAllEntities,
            addEntity: addEntity,
            updateEntity: updateEntity,
            deleteEntity: deleteEntity
        };

        return service;

        function getEntity(id) {
            return data.get('entity/get/' + id);
        }

        function getAllEntities() {
            return data.get('entity/getall');
        }

        function addEntity(entity) {
            return data.post('entity/post', entity);
        }

        function updateEntity(entity) {
            // TOOD: Implement
        }

        function deleteEntity(id) {
            // TODO: Implement
        }
    }

}());
