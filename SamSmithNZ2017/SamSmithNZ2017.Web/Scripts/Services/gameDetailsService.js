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
        var baseUrl = 'http://ssnzsteamservice.azurewebsites.net/';

        this.getGameDetails = function (steamId, appId) {
            var url = baseUrl + 'api/GameDetails/GetGameDetails?steamId=' + steamId + '&appId=' + appId;
            return $http.get(url);
        }

    }
})();