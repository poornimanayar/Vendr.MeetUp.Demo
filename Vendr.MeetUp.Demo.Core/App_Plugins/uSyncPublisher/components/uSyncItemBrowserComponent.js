(function () {

    'use strict';

    var uSyncItemBrowserComponent = {
        templateUrl: Umbraco.Sys.ServerVariables.application.applicationPath + 'App_Plugins/uSyncPublisher/Components/uSyncItemBrowser.html',
        bindings: {
            items: '=',
            selection: '=',
            loadFolder: '&',
            loading: '=',
            serverName: '=',
            type: '@'
        },
        controllerAs: 'vm',
        controller: uSyncBrowserController
    };

    function uSyncBrowserController($scope) {

        var vm = this; 
        
        vm.onClickItem = onClickItem;
        vm.onClickItemName = onClickItemName;
        vm.onBreadcrumbItem = onBreadcrumbItem;

        vm.rootKey = '00000000-0000-0000-0000-000000000000';
        vm.breadcrumb = [{ key: vm.rootKey, name: 'Media' }]

        vm.current = vm.rootKey;

        ///////
        function onBreadcrumbItem(item, $index) {
            if ($index < vm.breadcrumb.length - 1) {

                // vm.clearSelection();

                var pos = $index + 1;
                vm.breadcrumb.splice(pos, vm.breadcrumb.length - pos);

                vm.loadFolder({ key: vm.breadcrumb[vm.breadcrumb.length - 1].key });
            }
        }

        function onClickItem(item, $event) {

            $event.preventDefault();
            $event.stopPropagation();

            item.selected = !item.selected;

            var alreadySelected = false; 
            // selection code.
            for (let i = 0; i < vm.selection.length; i++) {
                if (vm.selection[i].udi == item.udi) {
                    if (item.selected) {
                        alreadySelected = true;
                    }
                    else {
                        vm.selection.splice(i, 1);
                    }
                }
            }

            if (item.selected && !alreadySelected) {
                vm.selection.push(item);
            }
        }

        function onClickItemName(item, $event) {

            $event.preventDefault();
            $event.stopPropagation();

            if (item.isFolder && item.hasChildren && vm.loadFolder) {

                vm.breadcrumb.push({ key: item.key, name: item.name });
                vm.loadFolder({ key: item.key });

            }
            else {
                onClickItem(item, $event);
            }
        }

        //// thumbnail making.

        function activate() {
            for (var i = 0; vm.items.nodes.length > i; i++) {
                setOriginalSize(vm.items.nodes[i]);
            }
        }

        var itemDefaultHeight = 200;
        var itemDefaultWidth = 200;
        var itemMaxWidth = 200;
        var itemMaxHeight = 200;

        function setOriginalSize(item) {

            if (item.height !== undefined && item.width !== undefined) {
                //set to a square by default
                item.width = itemDefaultWidth;
                item.height = itemDefaultHeight;
                item.aspectRatio = 1;

                item.aspectRatio = item.width / item.height;

                // set max width and height
                // landscape
                if (item.aspectRatio >= 1) {
                    if (item.width > itemMaxWidth) {
                        item.width = itemMaxWidth;
                        item.height = itemMaxWidth / item.aspectRatio;
                    }
                    // portrait
                } else {
                    if (item.height > itemMaxHeight) {
                        item.height = itemMaxHeight;
                        item.width = itemMaxHeight * item.aspectRatio;
                    }
                }
            }

        }

        var unbindItemsWatcher = $scope.$watch('vm.items', function (newValue, oldValue) {
            if (newValue !== undefined && _.isArray(newValue.nodes))
            {
                if (newValue.key === vm.rootKey) {
                    vm.breadcrumb = [vm.breadcrumb[0]];
                };

                activate();
            }
        });

        var unbindServerWatcher = $scope.$watch('vm.serverName', function (name) {
            if (name !== undefined) {
                vm.breadcrumb[0].name = vm.type + ' [' + name + ']';

            }
        })

        
        $scope.$on('$destroy', function () {
            unbindItemsWatcher();
            unbindServerWatcher();
        });


    }

    angular.module('umbraco')
        .component('usyncItemBrowser', uSyncItemBrowserComponent);

})();