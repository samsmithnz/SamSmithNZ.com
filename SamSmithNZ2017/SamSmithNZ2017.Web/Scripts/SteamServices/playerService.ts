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
        var baseUrl = 'https://ssnzsteam2019webservice.azurewebsites.net/';

        this.getPlayer = function (steamId) {
            let url: string =  baseUrl + 'api/Player/GetPlayer?steamId=' + steamId;
            //console.log(url);
            return $http.get(url);
        };

    }
})();