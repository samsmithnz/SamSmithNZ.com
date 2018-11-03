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
        var baseUrl = 'https://ssnzsteam2019webservice.azurewebsites.net/';

        this.getFriends = function (steamId) {
            let url: string = baseUrl + 'api/Friends/GetFriends?steamId=' + steamId;
            //console.log(url);
            return $http.get(url);
        };

        this.getFriendsWithSameGame = function (steamId, appId) {
            let url: string = baseUrl + 'api/Friends/GetFriendsWithSameGame?steamId=' + steamId + '&appId=' + appId;
            //console.log(url);
            return $http.get(url);
        };

    }
})();