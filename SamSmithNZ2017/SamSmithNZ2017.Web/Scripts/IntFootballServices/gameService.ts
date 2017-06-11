(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('gameService', gameService);
    gameService.$inject = ['$http'];//, '$q', 'configSettings'];

    function gameService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballservice.azurewebsites.net/';

        this.getGamesByTeam = function (teamCode) {
            let url: string = baseUrl + 'api/Game/GetGamesByTeam?TeamCode=' + teamCode;
            console.log(url);
            return $http.get(url);
        };

        this.getGamesForGroup = function (teamCode) {
            let url: string = baseUrl + 'api/Game/GetGamesByTeam?TeamCode=' + teamCode;
            console.log(url);
            return $http.get(url);
        };

        this.getGamesForPlayoffs = function (teamCode) {
            let url: string = baseUrl + 'api/Game/GetGamesByTeam?TeamCode=' + teamCode;
            console.log(url);
            return $http.get(url);
        };

        //this.getTeam = function (teamCode) {
        //    let url: string = baseUrl + 'api/Team/GetTournament?TournamentCode=' + teamCode;
        //    //console.log(url);
        //    return $http.get(url);
        //};

    }
})();