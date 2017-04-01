(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('albumsService', albumsService);
    albumsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function albumsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzguitartabservice.azurewebsites.net/';

        this.getAlbums = function (includeAllItems) {
            var url = baseUrl + 'api/Album/GetAlbums?isAdmin=' + isAdmin;
            console.log(url);
            return $http.get(url);
        };

        this.getAlbum = function (albumCode, isAdmin) {
            var url = baseUrl + 'api/Album/GetAlbum?AlbumCode=' + albumCode + '&isAdmin=' + isAdmin;
            console.log(url);
            return $http.get(url);
        };

        this.saveAlbum = function (albumItem) {
            var url = baseUrl + 'api/Album/SaveAlbum';
            console.log(url);
            return $http.post(url, albumItem);
        };     

    }
})();