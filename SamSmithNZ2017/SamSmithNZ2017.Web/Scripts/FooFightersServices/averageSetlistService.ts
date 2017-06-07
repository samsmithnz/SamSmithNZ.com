(function () {
    'use strict';

    angular
        .module('FooFightersApp')
        .service('averageSetlistService', averageSetlistService);
    averageSetlistService.$inject = ['$http'];//, '$q', 'configSettings'];

    function averageSetlistService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofightersservice.azurewebsites.net/';

        this.getAverageSetlist = function (yearCode, minimumSongCount, showAllSongs) {
            let url: string = baseUrl + 'api/AverageSetlist/GetAverageSetlist?YearCode=' + yearCode + '&minimumSongCount=' + minimumSongCount + '&showAllSongs=' + showAllSongs;
            console.log(url);
            return $http.get(url);
        };


    }
})();