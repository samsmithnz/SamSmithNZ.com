(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('tournamentTeamService', tournamentTeamService);
    tournamentTeamService.$inject = ['$http'];//, '$q', 'configSettings'];

    function tournamentTeamService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballservice.azurewebsites.net/';

        this.getTournamentQualifyingTeams = function (tournamentCode) {
            let url: string = baseUrl + 'api/TournamentTeam/GetTournamentQualifyingTeams?TournamentCode=' + tournamentCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getTournamentPlacingTeams = function (tournamentCode) {
            let url: string = baseUrl + 'api/TournamentTeam/GetTournamentPlacingTeams?TournamentCode=' + tournamentCode;
            //console.log(url);
            return $http.get(url);
        };

    }
})();