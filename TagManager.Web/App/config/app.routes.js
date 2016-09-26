(function () {
   'use strict';

   //Configuration for Angular UI routing.
   angular
    .module('app')
    .config([
       '$stateProvider', '$urlRouterProvider',
       function ($stateProvider, $urlRouterProvider) {
          $urlRouterProvider.otherwise('/gallery');

          $stateProvider
               .state('gallery', {
                  url: '/gallery',
                  templateUrl: 'App/gallery/gallery.html',
                  controller: 'GalleryController as vm'
               })
               .state('media-item', {
                  url: '/media-item/:id',
                  templateUrl: 'App/media-item/media-item.html',
                  controller: 'MediaItemController as vm'
               })
               .state('processing', {
                  url: '/media-item/:id/processing',
                  templateUrl: 'App/processing/processing.html',
                  controller: 'ProcessingController as vm'
               })
          ;
       }
    ]);
})()
