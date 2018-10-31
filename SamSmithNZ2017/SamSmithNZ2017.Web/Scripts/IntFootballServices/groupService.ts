(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('groupService', groupService);
    groupService.$inject = ['$http'];//, '$q', 'configSettings'];

    function groupService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballwebservice.azurewebsites.net/';

        this.getGroups = function (tournamentCode, roundNumber, roundCode) {
            let url: string = baseUrl + 'api/Group/GetGroups?tournamentCode=' + tournamentCode + '&roundNumber=' + roundNumber + '&roundCode=' + roundCode;
            //console.log(url);
            return $http.get(url);
        };

    }
})();