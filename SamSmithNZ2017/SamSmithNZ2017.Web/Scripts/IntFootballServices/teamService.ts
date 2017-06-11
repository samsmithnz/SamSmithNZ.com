(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('teamService', teamService);
    teamService.$inject = ['$http'];//, '$q', 'configSettings'];

    function teamService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballservice.azurewebsites.net/';

        this.getTeams = function () {
            let url: string = baseUrl + 'api/Team/GetTeams';
            //console.log(url);
            return $http.get(url);
        };

        this.getTeam = function (teamCode) {
            let url: string = baseUrl + 'api/Team/GetTeam?TeamCode=' + teamCode;
            //console.log(url);
            return $http.get(url);
        };

    }
})();