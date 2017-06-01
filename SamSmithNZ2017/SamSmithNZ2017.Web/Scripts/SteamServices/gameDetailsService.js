(function () {
    'use strict';
    angular
        .module('SteamApp')
        .service('gameDetailsService', gameDetailsService);
    gameDetailsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function gameDetailsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzsteamservice.azurewebsites.net/';
        this.getGameDetails = function (steamId, appId) {
            var url = baseUrl + 'api/GameDetails/GetGameDetails?steamId=' + steamId + '&appId=' + appId;
            console.log(url);
            return $http.get(url);
        };
        this.getGameWithFriendDetails = function (steamId, appId, friendSteamId) {
            var url = baseUrl + 'api/GameDetails/GetGameWithFriendDetails?steamId=' + steamId + '&appId=' + appId + '&friendSteamId=' + friendSteamId;
            console.log(url);
            return $http.get(url);
        };
    }
})();
