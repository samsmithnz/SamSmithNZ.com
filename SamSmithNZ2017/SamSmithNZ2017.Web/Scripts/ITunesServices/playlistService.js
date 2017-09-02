(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .service('playlistService', playlistService);
    playlistService.$inject = ['$http']; //, '$q', 'configSettings'];
    function playlistService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzitunesservice.azurewebsites.net/';
        this.getPlaylists = function () {
            var url = baseUrl + 'api/playlist/GetPlaylists';
            //console.log(url);
            return $http.get(url);
        };
        this.getPlaylist = function (playlistCode) {
            var url = baseUrl + 'api/playlist/GetPlaylist?playlistCode=' + playlistCode;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
