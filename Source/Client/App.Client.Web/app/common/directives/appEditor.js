/***********************
 * App Editor Directive
 ***********************/
(function () {
    'use strict';

    angular.module('app.directives')
        .directive('appEditor', appEditor);

    function appEditor() {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: editor
        };

        function editor(scope, elm, attr, ngModel) {
            var ckinstance = CKEDITOR.instances.content;

            if (ckinstance) {
                ckinstance.destroy(true);
            }

            var ck = CKEDITOR.replace(elm[0]);

            if (!ngModel) {
                return;
            }

            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });

            function updateModel() {
                scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            }

            ck.on('change', updateModel);
            ck.on('key', updateModel);
            ck.on('dataReady', updateModel);

            ngModel.$render = function (value) {
                ck.setData(ngModel.$viewValue);
            };
        }
    }

}());
