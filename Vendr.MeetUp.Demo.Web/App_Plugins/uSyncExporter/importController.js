(function () {
    'use strict';

    function importController($scope,
        navigationService,
        notificationsService, uSyncExporterService,
        Upload,
        uSyncHub) {

        var vm = this;

        vm.buttonState = 'init';

        vm.importGroup = {
            defaultButton: {
                labelKey: 'usync_import',
                handler: function () {
                    vm.importPack(vm.importId, false);
                }
            },
            subButtons: [{
                labelKey: 'usync_importforce',
                handler: function () {
                    vm.importPack(vm.importId, true);
                }
            }]
        };


        vm.close = close;
        vm.handleFiles = handleFiles;
        vm.upload = upload;
        vm.importPack = importPack;

        vm.countChanges = countChanges;

        vm.file = null;
        vm.report = [];

        vm.imported = false;
        vm.running = false;
        vm.uploaded = false;

        vm.update = {
            Message: 'Importing',
            Count: 1,
            Total: 1
        };

        // signalR
        InitHub();
        vm.calcPercentage = calcPercentage;
        vm.getTypeName = getTypeName;

        /////////////////////////////////

        function close() {
            if ($scope.model.close) {
                $scope.model.close();
            }
            else {
                navigationService.hideDialog();
            }
        }

        function countChanges(changes) {
            var count = 0;
            angular.forEach(changes, function (val, key) {
                if (val.Change !== 'NoChange') {
                    count++;
                }
            });

            return count;
        }

        ///// file managent 

        function handleFiles(files, event) {
            if (files && files.length > 0) {
                vm.file = files[0];
                // vm.upload(files[0]);
            }
        }

        function upload(file) {
            vm.buttonState = 'busy';
            vm.running = true;
            vm.uploaded = false;

            Upload.upload({
                url: Umbraco.Sys.ServerVariables.uSync.exporterService + 'uploadFile',
                fields: {
                    clientId: getClientId()
                },
                file: file
            }).success(function (data, status, headers, config) {
                vm.buttonState = 'success';

                vm.running = false;
                vm.uploaded = true;

                vm.report = data.Actions;
                vm.importId = data.ImportId;
            }).error(function (evt, status, headers, config) {
                vm.running = false;
                vm.buttonState = 'error';
            });
        }

        function importPack(id, force) {
            vm.buttonState = 'busy';
            vm.running = true;
            uSyncExporterService.importPack(id, force, getClientId())
                .then(function (result) {
                    vm.report = result.data.Actions;
                    vm.imported = true;
                    vm.running = false;
                    vm.buttonState = 'success';
                    notificationsService.success('imported', 'Import complete');
                }, function (error) {
                    vm.running = false;
                    vm.buttonState = 'error';
                    notificationsService.error('error', error.data.ExceptionMessage);
                });
        }

        ////// SignalR things 
            
        vm.calcPercentage = calcPercentage;

        function InitHub() {
            uSyncHub.initHub(function (hub) {

                vm.hub = hub;

                vm.hub.on('update', function (update) {
                    vm.update = update;
                });

                vm.hub.on('add', function (data) {
                    vm.status = data;
                });


                vm.hub.start();
            });
        }

        function getClientId() {
            if ($.connection !== undefined && $.connection.hub !== undefined) {
                return $.connection.hub.id;
            }
            return "";
        }

        function calcPercentage(status) {
            if (status !== undefined) {
                return (100 * status.Count) / status.Total;
            }
            return 1;
        }

        function getTypeName(typeName) {
            var umbType = typeName.substring(0, typeName.indexOf(','));
            return umbType.substring(umbType.lastIndexOf('.') + 1);
        }


    }

    angular.module('umbraco')
        .controller('uSyncExporterImportController', importController);
})();