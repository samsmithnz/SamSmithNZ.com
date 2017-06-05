(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .service('topArtistsService', topArtistsService);
    topArtistsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function topArtistsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzitunesservice.azurewebsites.net/';
        this.getTopArtistsByPlaylist = function (playlistCode, showJustSummary) {
            var url = baseUrl + 'api/TopArtists/GetTopArtistsByPlaylist?playlistCode=' + playlistCode + '&showJustSummary=' + showJustSummary;
            console.log(url);
            return $http.get(url);
        };
        this.getTopArtistsSummary = function (showJustSummary) {
            var url = baseUrl + 'api/TopArtists/GetTopArtistsSummary?showJustSummary=' + showJustSummary;
            console.log(url);
            return $http.get(url);
        };
    }
})();
