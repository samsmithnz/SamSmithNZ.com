(function () {
    'use strict';
    angular
        .module('IntFootballApp')
        .service('tournamentService', tournamentService);
    tournamentService.$inject = ['$http']; //, '$q', 'configSettings'];
    function tournamentService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballservice.azurewebsites.net/';
        this.getTournaments = function () {
            var url = baseUrl + 'api/Tournament/GetTournaments';
            //console.log(url);
            return $http.get(url);
        };
        this.getTournament = function (tournamentCode) {
            var url = baseUrl + 'api/Tournament/GetTournament?TournamentCode=' + tournamentCode;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=tournamentService.js.map