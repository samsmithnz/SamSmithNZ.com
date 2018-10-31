(function () {
    'use strict';

    angular
        .module('ITunesApp')
        .service('topArtistsService', topArtistsService);
    topArtistsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function topArtistsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzituneswebservice.azurewebsites.net/';

        this.getTopArtistsByPlaylist = function (playlistCode, showJustSummary) {
            let url: string = baseUrl + 'api/TopArtists/GetTopArtistsByPlaylist?playlistCode=' + playlistCode + '&showJustSummary=' + showJustSummary;
            //console.log(url);
            return $http.get(url);
        };

        this.getTopArtistsSummary = function (showJustSummary) {
            let url: string = baseUrl + 'api/TopArtists/GetTopArtistsSummary?showJustSummary=' + showJustSummary;
            //console.log(url);
            return $http.get(url);
        };

    }
})();