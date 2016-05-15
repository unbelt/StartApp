/***************
 * Usafe Filter
 ***************/
(function () {
    'use strict';

    angular.module('app.filters')
        .filter('unsafe', unsafe);

    unsafe.$inject = ['$sce'];

    function unsafe($sce) {
        return $sce.trustAsHtml;
    }

}());
