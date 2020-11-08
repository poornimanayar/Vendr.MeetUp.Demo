(function () {
    'use strict';

    function browserDashboard($scope, appState, uSyncPublishService, uSyncPublishDialogManager) {

        var vm = this;

        vm.section = appState.getSectionState('currentSection');

        vm.servers = [];
        vm.server = {};
        vm.picked = false;

        vm.openDialog = openDialog;
        vm.getFolders = getFolders;
        vm.clearSelection = clearSelection;
        vm.onSelected = onSelected;

        vm.selectionLabel = 'Pull selection';

        vm.selectLocal = selectLocal;
        vm.local = false;

        vm.rootKey = '00000000-0000-0000-0000-000000000000';


        vm.mode = 'pull';

        vm.items = {};

        uSyncPublishService.getServers(vm.mode)
            .then(function (result) {
                vm.servers = result.data;
                checkServers(vm.servers);
            });


        function checkServers(servers) {
            servers.forEach(function (server) {
                uSyncPublishService.checkServer(server.Alias)
                    .then(function (result) {
                        server.status = result.data;
                    });
            });
        }

        //////////////
        function openDialog() {

            if (vm.section === 'content') {
                if (vm.local) {
                    uSyncPublishDialogManager.openPublisherDialog({
                        application: vm.section,
                        items: vm.selected
                    }, function (result) {
                        if (result) {
                            clearSelection()
                        }
                    });
                }
                else {
                    uSyncPublishDialogManager.openPublisherPullDialog({
                        application: vm.section,
                        serverAlias: vm.server.Alias,
                        items: vm.selected
                    }, function (result) {
                        if (result) {
                            clearSelection();
                        }
                    });
                }
            }
            else {
                if (vm.local) {
                    uSyncPublishDialogManager.openPublisherMediaPush({
                        application: vm.section,
                        items: vm.selected
                    }, function (result) {
                        if (result) {
                            clearSelection()
                        }
                    });
                }
                else {
                    uSyncPublishDialogManager.openPublisherMediaPull({
                        application: vm.section,
                        serverAlias: vm.server.Alias,
                        items: vm.selected
                    }, function (result) {
                        if (result) {
                            clearSelection();
                        }
                    });
                }

            }
        }

        function selectLocal() {
            vm.local = true;
            vm.selectionLabel = 'Push selection';
            vm.picked = false;
            vm.server = { name: 'local' };
            vm.servers.forEach(function (server) {
                server.selected = false;
            });
            vm.items = {};
            clearSelection();
            getFolders(vm.rootKey);
        }


        //////////////

        function onSelected(server) {
            vm.picked = true;
            vm.local = false;
            vm.selectionLabel = 'Pull selection';
            vm.server = server;
            vm.loading = true;
            getFolders(vm.rootKey);
        }

        function getFolders(key) {
            vm.loading = true;
            clearSelection();

            if (vm.local) {
                if (vm.section === 'content') {
                    uSyncPublishService.getLocalContentFolders(key)
                        .then(function (result) {
                            vm.loading = false;
                            vm.items = result.data;
                        });
                }
                else {
                    uSyncPublishService.getLocalMediaFolders(key)
                        .then(function (result) {
                            vm.loading = false;
                            vm.items = result.data;
                        });
                }
            }
            else {

                if (vm.section === 'content') {
                    uSyncPublishService.getContentFolders(key, vm.server.Alias)
                        .then(function (result) {
                            vm.loading = false;
                            vm.items = result.data;
                        });
                }
                else {
                    uSyncPublishService.getMediaFolders(key, vm.server.Alias)
                        .then(function (result) {
                            vm.loading = false;
                            vm.items = result.data;
                        });

                }
            }
        }
         
        function clearSelection() {
            vm.selected = [];

            clearsSelectedItems(vm.items.folders);
            clearsSelectedItems(vm.items.nodes);
        }

        function clearsSelectedItems(items) {
            if (items !== undefined && items !== null) {
                for (let i = 0; i < items.length; i++) {
                    items[i].selected = false;
                }
            }
        }

        vm.selected = [];

    }

    angular.module('umbraco')
        .controller('uSyncPublisherBrowserDashboardController', browserDashboard);

})();