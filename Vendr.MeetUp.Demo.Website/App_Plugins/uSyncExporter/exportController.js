(function () {
    'use strict';

    function exportController($scope,
        navigationService,
        uSyncExporterService) {

        var vm = this;
        vm.hasError = false;

        vm.node = $scope.currentNode;

        vm.exportItem = exportItem;
        vm.exportContainer = exportContainer;

        vm.hasChildren = $scope.currentNode.hasChildren;
        vm.isDocType = $scope.currentNode.nodeType === 'documentTypes';
        vm.isMedia = $scope.currentNode.nodeType === 'media';

        vm.options = {
            includeChildren: false,
            includeAncestors: true,
            includeDependencies: true,
            includeFiles: false,
            includeMedia: false
        };

        vm.close = close;

        function exportItem() {
            vm.hasError = false;
            vm.buttonState = 'busy';

            uSyncExporterService.exportItem(vm.node.udi, vm.node.name, vm.options)
                .then(function () {
                    vm.buttonState = 'success';
                }, function (error) {
                    vm.buttonState = 'error';
                    vm.hasError = true;
                    vm.errorMessage = error.data.ExceptionMessage;
                });
        }

        function exportContainer(itemType) {
            vm.buttonState = 'busy';
            uSyncExporterService.exportContainer(vm.node.id, itemType, vm.node.name, vm.options)
                .then(function () {
                    vm.buttonState = 'success';
                }, function (error) {
                        vm.buttonState = 'error';
                        vm.hasError = true;
                        vm.errorMessage = error.data.ExceptionMessage;
                });
        }

        function close() {
            if ($scope.model && $scope.model.close) {
                $scope.model.close();
            }
            else {
                navigationService.hideDialog();
            }
        }
    }

    angular.module('umbraco')
        .controller('usyncExporterExportController', exportController);

})();