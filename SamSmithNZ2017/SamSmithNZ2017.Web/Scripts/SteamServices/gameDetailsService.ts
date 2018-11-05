(function () {
    'use strict';

    angular
        .module('SteamApp')
        .service('gameDetailsService', gameDetailsService);
    gameDetailsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function gameDetailsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzsteam2019webservice.azurewebsites.net/';

        this.getGameDetails = function (steamId, appId) {
            let url: string =  baseUrl + 'api/GameDetails/GetGameDetails?steamId=' + steamId + '&appId=' + appId;
            //console.log(url);
            return $http.get(url);
        };

        //this.getGameWithFriendDetails = function (steamId, appId, friendSteamId) {
        //    let url: string =  baseUrl + 'api/GameDetails/GetGameWithFriendDetails?steamId=' + steamId + '&appId=' + appId + '&friendSteamId=' + friendSteamId;
        //    //console.log(url);
        //    return $http.get(url);
        //};

    }
})();