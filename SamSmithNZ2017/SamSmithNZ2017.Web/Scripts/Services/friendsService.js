(function () {
    'use strict';

    angular
        .module('SteamApp')
        .service('friendsService', friendsService);
    friendsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function friendsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzsteamservice.azurewebsites.net/';

        this.getFriends = function (steamId) {
            var url = baseUrl + 'api/Friends/GetFriends?steamId=' + steamId;
            return $http.get(url);
        }

    }
})();