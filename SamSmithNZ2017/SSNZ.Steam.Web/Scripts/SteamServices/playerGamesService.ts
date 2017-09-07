(function () {
    'use strict';

    angular
        .module('SteamApp')
        .service('playerGamesService', playerGamesService);
    playerGamesService.$inject = ['$http'];//, '$q', 'configSettings'];

    function playerGamesService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzsteamservice.azurewebsites.net/';

        this.getPlayerGames = function (steamId) {
            let url: string =  baseUrl + 'api/PlayerGames/GetPlayer?steamId=' + steamId; //todo: refactor to call API function "GetPlayerGames"
           //console.log(url);
            return $http.get(url);
        };

    }
})();