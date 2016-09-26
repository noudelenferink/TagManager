(function () {
    'use strict';

    angular
     .module('app')
     .controller('MediaItemController', MediaItemController);

    MediaItemController.$inject = ['$scope', '$state', '$sce', 'abp.services.app.mediaItem'];

    function MediaItemController($scope, $state, $sce, mediaItemService) {
        var vm = this;
        vm.title = 'Item';
        vm.mediaItemId = $state.params.id;
        
        activate();

        ///////////////////
        function activate() {
            vm.processingAllowed = false;
            vm.itemTypes = [
                { id: 0, name: 'Image' },
                {id: 1, name: 'Video'},
                { id: 2, name: 'Audio' }
            ]
            loadMediaItem();
        }

        function loadMediaItem() {
            mediaItemService.getMediaItem({ id: vm.mediaItemId }).then(function (response) {
                vm.mediaItem = response.data;
                vm.processingAllowed = vm.mediaItem.statusId === 0;
                setVideoConfig();

                if (vm.mediaItem.statusId === 2) {
                    vm.processingFinished = true;
                    mediaItemService.getRelevantMediaItems({ id: vm.mediaItemId }).then(function (response) {
                        vm.relevantItems = response.data;
                    });
                }
            })
        }

        function setVideoConfig() {
            vm.videoConfig = {
                sources: [
                   { src: $sce.trustAsResourceUrl("/Media/" + vm.mediaItem.fileName), type: "video/webm" }
                ],
                theme: "bower_components/videogular-themes-default/videogular.css"
            };
        }
    }
})()