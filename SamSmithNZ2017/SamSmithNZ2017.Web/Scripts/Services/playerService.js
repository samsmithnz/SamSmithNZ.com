(function () {
    'use strict';

    angular
        .module('SteamApp')
        .service('playerService', playerService);
    playerService.$inject = ['$http'];//, '$q', 'configSettings'];

    function playerService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzsteamservice.azurewebsites.net/';

        this.getPlayer = function (steamId) {
            var url = baseUrl + 'api/Player/GetPlayer?steamId=' + steamId;
            console.log(url);
            return $http.get(url);
        }

    }
})();