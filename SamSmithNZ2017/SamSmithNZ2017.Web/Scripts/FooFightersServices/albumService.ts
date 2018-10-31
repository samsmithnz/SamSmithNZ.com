(function () {
    'use strict';

    angular
        .module('FooFightersApp')
        .service('albumService', albumService);
    albumService.$inject = ['$http'];//, '$q', 'configSettings'];

    function albumService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofighterswebservice.azurewebsites.net/';

        this.getAlbums = function () {
            let url: string = baseUrl + 'api/Album/GetAlbums';
            //console.log(url);
            return $http.get(url);
        };

        this.getAlbum = function (albumCode) {
            let url: string = baseUrl + 'api/Album/GetAlbum?AlbumCode=' + albumCode;
            //console.log(url);
            return $http.get(url);
        };


    }
})();