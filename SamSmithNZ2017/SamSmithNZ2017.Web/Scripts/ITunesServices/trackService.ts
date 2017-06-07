(function () {
    'use strict';

    angular
        .module('ITunesApp')
        .service('trackService', trackService);
    trackService.$inject = ['$http'];//, '$q', 'configSettings'];

    function trackService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzitunesservice.azurewebsites.net/';

        this.getTracksByPlaylist = function (playlistCode: string, showJustSummary: string) {
            let url: string = baseUrl + 'api/track/GetTracks?playlistCode=' + playlistCode + '&showJustSummary=' + showJustSummary;
            //console.log(url);
            return $http.get(url);
        };

    }
})();