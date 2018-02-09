(function () {
    'use strict';
    angular
        .module('MandMCounterApp')
        .service('gameService', gameService);
    gameService.$inject = ['$http']; //, '$q', 'configSettings'];
    function gameService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        var baseUrl = 'https://mandmcounterservice.azurewebsites.net/';
        this.getDataForUnit = function (unit, quantity) {
            var url = baseUrl + 'api/MandMCounter/GetDataForUnit?unit=' + unit + '&quantity=' + quantity;
            console.log(url);
            return $http.get(url);
        };
        //this.getGamesForGroup = function (tournamentCode, roundNumber, roundCode) {
        //    let url: string = baseUrl + 'api/Game/GetGames?tournamentCode=' + tournamentCode + '&roundNumber=' + roundNumber + '&roundCode=' + roundCode;
        //    console.log(url);
        //    return $http.get(url);
        //};
        //this.getGamesForPlayoffs = function (tournamentCode, roundNumber) {
        //    let url: string = baseUrl + 'api/Game/GetPlayoffGames?tournamentCode=' + tournamentCode + '&roundNumber=' + roundNumber;
        //    console.log(url);
        //    return $http.get(url);
        //};
        //this.getTeam = function (teamCode) {
        //    let url: string = baseUrl + 'api/Team/GetTournament?TournamentCode=' + teamCode;
        //    //console.log(url);
        //    return $http.get(url);
        //};
    }
})();
//# sourceMappingURL=gameService.js.map