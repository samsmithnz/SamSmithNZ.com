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
        var baseUrl = 'http://ssnzfoofightersservice.azurewebsites.net/';

        this.getShows = function (yearCode) {
            var url = baseUrl + 'api/Show/GetShowsByYear?YearCode=' + yearCode;
            console.log(url);
            return $http.get(url);
        };

        this.getShowsBySong = function (songCode) {
            var url = baseUrl + 'api/Show/GetShowsBySong?SongCode=' + songCode;
            console.log(url);
            return $http.get(url);
        };

        this.getShow = function (showCode) {
            var url = baseUrl + 'api/Show/GetShow?ShowCode=' + showCode;
            console.log(url);
            return $http.get(url);
        };
   

    }
})();