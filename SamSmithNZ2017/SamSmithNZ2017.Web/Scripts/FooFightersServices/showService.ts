(function () {
    'use strict';

    angular
        .module('FooFightersApp')
        .service('showService', showService);
    showService.$inject = ['$http'];//, '$q', 'configSettings'];

    function showService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofighterswebservice.azurewebsites.net/';

        this.getShows = function (yearCode) {
            let url: string = baseUrl + 'api/Show/GetShowsByYear?YearCode=' + yearCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getShowsBySong = function (songCode) {
            let url: string = baseUrl + 'api/Show/GetShowsBySong?SongCode=' + songCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getShow = function (showCode) {
            let url: string = baseUrl + 'api/Show/GetShow?ShowCode=' + showCode;
            //console.log(url);
            return $http.get(url);
        };


    }
})();