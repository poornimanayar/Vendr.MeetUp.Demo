(function () {
    'use strict';

    function dashboardController(editorService, notificationsService, 
        uSyncExportManager, uSyncExporterService, uSyncDependencyManager, uSyncHub) {

        var vm = this;

        vm.openImportDialog = openImportDialog;
        vm.pickDataTypes = pickDataTypes;

        vm.selection = [];
        vm.includeFiles = true;
        vm.includeMedia = true;
        vm.includeDictionary = true;
        vm.includeLinked = false;
        vm.includeConfig = false;

        vm.create = create;
        vm.createState = 'init';

        vm.remove = remove;
        vm.licenced = true;

        getSettings();
        InitHub();

        uSyncExporterService.isLicenced()
            .then(function (result) {
                vm.licenced = result.data;
            });

        /////////////

        function getSettings() {
            uSyncExporterService.getSettings()
                .then(function (result) {
                    vm.version = result.data.Version;
                    vm.includeFiles = result.data.IncludeTemplates;
                    vm.includeMedia = result.data.IncludeMediaFiles;
                    vm.includeLinked = result.data.IncludeLinkedItems;
                    vm.includeDictionary = result.data.IncludeDictionaryItems;
                });

            uSyncExporterService.getExporters()
                .then(function (result) {
                    vm.exporters = result.data;
                });
        }


        function openImportDialog() {
            uSyncExportManager.openImportDialog({
                entity: { id: -1, name: 'uSync Pack' }
            }, null);
        }

        function create() {

            var items = [];
            vm.createState = 'busy';

            for (let x = 0; x < vm.selection.length; x++) {
                items.push({
                    id: vm.selection[x].id,
                    udi: vm.selection[x].udi,
                    name: vm.selection[x].name,
                    flags: uSyncDependencyManager.getFlags(
                        {
                            includeChildren: vm.selection[x].children,
                            includeAncestors: vm.selection[x].ancestors,
                            includeDependencies: vm.selection[x].dependencies,
                            includeFiles: vm.includeFiles,
                            includeMedia: vm.includeMedia,
                            includeLinked: vm.includeLinked,
                            includeConfig: vm.includeConfig
                        })
                });
            }

            if (vm.includeDictionary) {
                items.push({
                    udi: 'umb://dictionary-item/00000000-0000-0000-0000-000000000000',
                    name: 'dictionary items',
                    flags: uSyncDependencyManager.getFlags({
                        includeChildren: true
                    })
                });

                items.push({
                    udi: 'umb://language/',
                    name: 'Languages',
                    flags: uSyncDependencyManager.getFlags({
                        includeChildren: true
                    })
                });
            }

            uSyncExporterService.exportItems(items, vm.includeFiles, getClientId())
                .then(function (result) {
                    vm.createState = 'success';
                    notificationsService.success('Exported', 'Sync-Pack Created');
                }, function (error) {
                    vm.createState = 'error';
                    console.log(error);
                    notificationsService.error('Export Failed', error.errorMsg);
                });
        }

        function pickDataTypes(treeAlias, section, blockContainers) {

            var contentPicker = {
                section: section,
                title: 'Add ' + treeAlias,
                treeAlias: treeAlias,
                multiPicker: true,
                idType: "int",
                submit: function (model) {
                    vm.selection = vm.selection.concat(prepSelection(model.selection));
                    editorService.close();
                },
                close: function () {
                    editorService.close();
                },
                select: function (node) {
                    node.selected = node.selected === true ? false : true;
                    multiSelectItem(node, this.selection);
                },
                selection: vm.selection
            };

            if (blockContainers) {
                // stops you picking the all-members container
                contentPicker.filter = function (i) {
                    return i.metaData.isContainer;
                };
                contentPicker.filterCssClass = "not-allowed";
            }

            editorService.treePicker(contentPicker);

        }


        function prepSelection(selection) {

            if (selection.length > 0) {
                for (var i = 0; selection.length > i; i++) {
                    selection[i].children = false;
                    selection[i].ancestors = true;
                    selection[i].dependencies = true;

                    switch (selection[i].nodeType) {
                        case 'media':
                        case 'content':
                        case 'container':
                            selection[i].children = true;
                            break;
                        case 'templates':
                            selection[i].includeFiles = true;
                            break;
                    }

                }
            }

            return selection;
        }

        function multiSelectItem(item, selection) {

            var found = false;
            var foundIndex = 0;

            if (selection.length > 0) {
                for (var i = 0; selection.length > i; i++) {
                    var selectedItem = selection[i];
                    if (selectedItem.id === item.id) {
                        found = true;
                        foundIndex = i;
                    }
                }
            }

            if (found) {
                selection.splice(foundIndex, 1);
            }
            else {

                // default import behavior 
                item.children = false;
                item.ancestors = true;
                item.dependencies = true;

                switch (item.nodeType) {
                    case 'media':
                    case 'content':
                    case 'container':
                        item.children = true;
                        break;
                    case 'templates':
                        vm.includeFiles = true;
                        break;
                    case 'macros':
                        // macros are not really there - we do this so we can know
                        // its a macro when we process it. 
                        item.udi = 'umb://macro/00000000-0000-0000-0000-000000000000';
                        break;
                }
                selection.push(item);
            }
        }

        function remove(id) {

            var found = false;
            var foundIndex = 0;

            for (var i = 0; vm.selection.length > i; i++) {
                var selectedItem = vm.selection[i];
                if (selectedItem.id === id) {
                    found = true;
                    foundIndex = i;
                }
            }

            if (found) {
                vm.selection.splice(foundIndex, 1);
            }
        }

        vm.help = {};
        vm.showHelp = showHelp;

        function showHelp(title, viewName) {

            vm.help = {
                title: title,
                subtitle: 'uSync Exporter Help',
                view: '/App_Plugins/uSyncExporter/help/' + viewName + ".html",
                show: true,
                hideSubmitButton: true,
                submit: function (model) {
                    vm.help.show = false;
                    vm.help = {};
                },
                close: function (oldmodel) {
                    vm.help.show = false;
                    vm.help = {};
                }
            };
        }

        vm.calcPercentage = calcPercentage;

        function calcPercentage(status) {
            if (status !== undefined) {
                return (100 * status.Count) / status.Total;
            }
            return 1;
        }



        ////// SignalR things 
        function InitHub() {
            uSyncHub.initHub(function (hub) {

                vm.hub = hub;

                vm.hub.on('update', function (update) {
                    vm.update = update;
                    vm.update.blocks = update.Message.split('||');
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


    }

    angular.module('umbraco')
        .controller('uSyncExportDashboardController', dashboardController);
})();