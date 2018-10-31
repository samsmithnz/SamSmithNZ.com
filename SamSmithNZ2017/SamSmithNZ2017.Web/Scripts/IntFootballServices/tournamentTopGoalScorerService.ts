(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('tournamentTopGoalScorerService', tournamentTopGoalScorerService);
    tournamentTopGoalScorerService.$inject = ['$http'];//, '$q', 'configSettings'];

    function tournamentTopGoalScorerService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballwebservice.azurewebsites.net/';

        this.getTournamentTopGoalScores = function (tournamentCode) {
            let url: string = baseUrl + 'api/TournamentTopGoalScorer/GetTournamentTopGoalScorers?TournamentCode=' + tournamentCode;
            //console.log(url);
            return $http.get(url);
        };

    }
})();