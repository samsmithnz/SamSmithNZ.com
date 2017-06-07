(function () {
    'use strict';

    angular
        .module('FooFightersApp')
        .service('songService', songService);
    songService.$inject = ['$http'];//, '$q', 'configSettings'];

    function songService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofightersservice.azurewebsites.net/';

        this.getSongs = function () {
            let url: string = baseUrl + 'api/Song/GetSongs';
            //console.log(url);
            return $http.get(url);
        };

        this.getSongsByAlbum = function (albumCode) {
            let url: string = baseUrl + 'api/Song/GetSongsByAlbum?AlbumCode=' + albumCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getSongsByShow = function (showCode) {
            let url: string = baseUrl + 'api/Song/GetSongsByShow?ShowCode=' + showCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getSong = function (songCode) {
            let url: string = baseUrl + 'api/Song/GetSong?SongCode=' + songCode;
            //console.log(url);
            return $http.get(url);
        };


    }
})();