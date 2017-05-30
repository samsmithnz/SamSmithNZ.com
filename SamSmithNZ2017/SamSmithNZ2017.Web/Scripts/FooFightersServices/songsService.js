(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .service('songsService', songsService);
    songsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function songsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzfoofightersservice.azurewebsites.net/';
        this.getSongs = function () {
            var url = baseUrl + 'api/Song/GetSongs';
            console.log(url);
            return $http.get(url);
        };
        this.getSongsByAlbum = function (albumCode) {
            var url = baseUrl + 'api/Song/GetSongsByAlbum?AlbumCode=' + albumCode;
            console.log(url);
            return $http.get(url);
        };
        this.getSongsByShow = function (showCode) {
            var url = baseUrl + 'api/Song/GetSongsByShow?ShowCode=' + showCode;
            console.log(url);
            return $http.get(url);
        };
        this.getSong = function (songCode) {
            var url = baseUrl + 'api/Song/GetSong?SongCode=' + songCode;
            console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=songsService.js.map