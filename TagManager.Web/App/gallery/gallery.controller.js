(function () {
   'use strict';

   angular
    .module('app')
    .controller('GalleryController', GalleryController);

   GalleryController.$inject = ['$scope', '$state', '$sessionStorage', 'abp.services.app.mediaItem'];

   function GalleryController($scope, $state, $sessionStorage, mediaItemService) {
      var vm = this;
      vm.$storage = $sessionStorage;
      vm.title = 'Gallery';
      vm.previousPage = previousPage;
      vm.nextPage = nextPage;
      vm.isFirstPage = isFirstPage;
      vm.isLastPage = isLastPage;
      vm.toFirstPage = toFirstPage;
      vm.getStatusIcon = getStatusIcon;


      activate();

      $scope.$watch('vm.currentPage', function (newVal, oldVal) {
         vm.$storage.galleryPage = newVal;
         loadMediaItems();
      });

      /////////////////////////

      function activate() {
         if (vm.$storage.galleryPage) {
            vm.currentPage = vm.$storage.galleryPage;
         } else {
            vm.currentPage = 1;
         }
      }

      function loadMediaItems() {
         mediaItemService.getMediaItemList({ page: vm.currentPage }).then(function (response) {
            vm.mediaItems = response.data.items;
         });
      }

      function getStatusIcon(statusId) {
         switch (statusId) {
            case 0:
               return 'mdi-shield-outline';
            case 1:
               return 'mdi-shield';
            case 2:
               return 'mdi-marker-check';
         }
      }

      function isFirstPage() {
         return vm.currentPage == 1;
      }

      function isLastPage() {
         if (!vm.mediaItems) {
            return true;
         } else {
            return vm.mediaItems.TotalCount / vm.mediaItems.Items > vm.currentPage;
         }
      }

      function toFirstPage() {
         vm.currentPage = 1;
      }

      function previousPage() {
         if (!isFirstPage()) {
            vm.currentPage--;
         }
      }

      function nextPage() {
         if (!isLastPage()) {
            vm.currentPage++;
         }
      }
   }

})()
