/**********************
 * Entity Data Service
 **********************/
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
            editEntity: editEntity,
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

        function editEntity(entity) {
            return data.update('entity/edit', entity);
        }

        function deleteEntity(id) {
            return data.remove('entity/delete/' + id);
        }
    }

}());
