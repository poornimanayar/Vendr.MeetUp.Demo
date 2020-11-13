(function () {
    'use strict';

    function dialogManager($rootScope, $timeout, editorService, navigationService) {

        return {
            openPublisherDialog: openPublisherDialog,
            openPublisherPullDialog: openPublisherPullDialog,
            openPublisherMediaPush: openPublisherMediaPush,
            openPublisherMediaPull: openPublisherMediaPull,

            openSettingsPush: openSettingsPush,

            openSyncDialog: openSyncDialog,

            openServerDialog: openServerDialog,
            openNewServerDialog: openNewServerDialog
        };

        function openPublisherDialog(options, cb) {
            options.application = 'content';
            openSyncDialog('Publish Content', 'publisher', options, cb, 'push');
        }

        function openPublisherPullDialog(options, cb) {
            options.application = 'content';
            openSyncDialog('Pull Content', 'publisher', options, cb, 'pull');
        }

        function openPublisherMediaPush(options, cb) {
            options.application = 'media';
            openSyncDialog('Publish Media', 'publisher', options, cb, 'push');
        }

        function openPublisherMediaPull(options, cb) {
            options.application = 'media';
            openSyncDialog('Pull Media', 'publisher', options, cb, 'pull');
        }

        function openSettingsPush(options, cb) {

            var emptyGuid = '00000000-0000-0000-0000-000000000000';
            var items = [
                {
                    uid: 'umb://document-type/' + emptyGuid, name: 'ContentType'
                },
                {
                    udi: 'umb://data-type/' + emptyGuid, name: 'DataType'
                },
                {
                    udi: 'umb://media-type/' + emptyGuid, name: 'MediaType'
                }];

            var deployOptions = {
                entity: {
                    id: '-1',
                    items: items
                },
                serverAlias: options.entity.id,
                application: 'settings'
            };

            openSyncDialog('Deploy Settings', 'publisher', deployOptions, cb, "SettingsPush", '');
        }

        function openSyncDialog(dialogTitle, dialogView, options, cb, mode, size = 'small') {

            if (options.entity !== undefined) {
                options.items = [options.entity];
            }

            editorService.open({
                options: options,
                mode: mode,
                title: dialogTitle,
                size: size,
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'dialogs/' + dialogView + '.html',
                submit: function (done) {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(true);
                    }
                },
                close: function () {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });

            // wrap in a timeout, get rid of the 'bounce' 
            $timeout(function () {
                navigationService.hideDialog();
            });
        }


        function openNewServerDialog(entity, cb) {
            editorService.open({
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'dialogs/addServer.html',
                title: 'Add Server',
                size: 'small',
                submit: function (model) {
                    editorService.close();
                    navigationService.hideNavigation();
                    openServerDialog(model.Alias);
                    if (cb !== undefined) {
                        cb(true);
                    }
                },
                close: function () {
                    editorService.close();
                    navigationService.hideNavigation();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });
        }

        function openServerDialog(alias, cb) {
            editorService.open({
                serverAlias: alias,
                title: 'Server View',
                view: Umbraco.Sys.ServerVariables.uSyncPublisher.pluginPath + 'backoffice/uSyncPublisher/server.html',
                submit: function (done) {
                    $rootScope.$broadcast('usync-publisher-settings-reload');
                    navigationService.hideNavigation();
                    editorService.close();
                    if (cb !== undefined) {
                        cb(true);
                    }
                },
                close: function () {
                    $rootScope.$broadcast('usync-publisher-settings-reload');
                    navigationService.hideNavigation();
                    editorService.close();
                    if (cb !== undefined) {
                        cb(false);
                    }
                }
            });
        }

    }

    angular.module('umbraco')
        .factory('uSyncPublishDialogManager', dialogManager);
})();