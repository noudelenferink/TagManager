(function () {
    'use strict';

    angular
     .module('app')
     .controller('ProcessingController', ProcessingController);

    ProcessingController.$inject = [
       '$scope',
       '$state',
       '$filter',
       '$sessionStorage',
       'abp.services.app.mediaItem'];

    function ProcessingController(
       $scope,
       $state,
       $filter,
       $sessionStorage,
       mediaItemService) {

        var vm = this;
        vm.title = 'Processing';
        vm.openMenu = openMenu;
        vm.setSuggestion = setSuggestion;
        vm.restoreTag = restoreTag;
        vm.processTags = processTags;
        vm.setValueAsMediaTag = setValueAsMediaTag;
        vm.anyMediaTags = anyMediaTags;
        vm.clearMediaTag = clearMediaTag;
        vm.saveMediaTags = saveMediaTags;

        activate();

        function activate() {
            vm.mediaItemId = $state.params.id;
            vm.mediaTagTypes = [
                { id: 0, name: 'None' },
                { id: 1, name: 'Actor' },
                { id: 2, name: 'Place' },
                { id: 3, name: 'Time' }
            ];

            getPreprocessedTags();

        }

        function getPreprocessedTags() {
            abp.ui.setBusy();
            mediaItemService.getMediaItem({ id: vm.mediaItemId }).then(function (response) {
                vm.mediaItem = response.data;
            });

            mediaItemService.getPreprocessedTags({ id: vm.mediaItemId }).then(function (response) {
                vm.preprocessedTags = response.data;
                vm.preprocessedTags.forEach(function (t) {
                    t.originalTagValue = t.tagValue;
                    t.ignored = false;
                })
            }).finally(function () {
                abp.ui.clearBusy();
            });;
        }

        function processTags() {
            abp.ui.setBusy();
            var tagList = $filter('where')(vm.preprocessedTags, { 'ignored': false });
            var tagValues = tagList.map(function (d) {
                return d.tagValue
            });

            mediaItemService.processTags({ tags: tagValues }).then(function (response) {
                vm.processedTagGroups = response.data;
            }).finally(function () {
                abp.ui.clearBusy();
            });
        }

        function setValueAsMediaTag(tagGroup, tag) {
            if (tag.mediaTag) {
                tagGroup.mediaTag = tag.mediaTag;
            } else {
                tagGroup.mediaTag = {
                    value: tag.value,
                    type: 0
                };
            }
        }

        function setSuggestion(tag, suggestion) {
            tag.value = suggestion;
        }

        function anyMediaTags() {
            var result = false;
            if (!vm.processedTagGroups) {
                return result;
            }

            vm.processedTagGroups.forEach(function (group) {
                if (group.mediaTag) {
                    result = true;
                };
            });

            return result;
        }

        function openMenu($mdOpenMenu, ev) {
            $mdOpenMenu(ev);
        };

        function restoreTag(tag) {
            tag.tagValue = tag.originalTagValue;
        }

        function clearMediaTag(tagGroup) {
            delete tagGroup.mediaTag;
        }

        function saveMediaTags() {
            abp.ui.setBusy();
            var mediaTagsToSave = vm.processedTagGroups.filter(function (group) {
                return group.mediaTag;
            }).map(function (group) {
                return group.mediaTag;
            });

            mediaItemService.saveMediaTags({ mediaItemId: vm.mediaItemId, mediaTags: mediaTagsToSave }).then(function (response) {
                abp.notify.success('Successfully saved the specified media tags');
            }).finally(function () {
                abp.ui.clearBusy();
            })
        }
    }
})();