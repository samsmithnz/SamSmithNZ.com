(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('albumService', albumService);
    albumService.$inject = ['$http'];//, '$q', 'configSettings'];

    function albumService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitarservice.azurewebsites.net/';

        this.getAlbums = function (isAdmin) {
            let url: string = baseUrl + 'api/Album/GetAlbums?isAdmin=' + isAdmin;
            //console.log(url);
            return $http.get(url);
        };

        this.getAlbum = function (albumCode, isAdmin) {
            let url: string = baseUrl + 'api/Album/GetAlbum?AlbumCode=' + albumCode + '&isAdmin=' + isAdmin;
            //console.log(url);
            return $http.get(url);
        };

        this.saveAlbum = function (albumItem) {
            let url: string = baseUrl + 'api/Album/SaveAlbum';
            //console.log(url);
            return $http.post(url, albumItem);
        };

    }
})();