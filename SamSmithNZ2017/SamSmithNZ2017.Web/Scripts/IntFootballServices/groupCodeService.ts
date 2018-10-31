(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .service('groupCodeService', groupCodeService);
    groupCodeService.$inject = ['$http'];//, '$q', 'configSettings'];

    function groupCodeService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballwebservice.azurewebsites.net/';

        this.getGroupCodes = function (tournamentCode, roundNumber) {
            let url: string = baseUrl + 'api/GroupCode/GetGroupCodes?tournamentCode=' + tournamentCode + '&roundNumber=' + roundNumber;
            //console.log(url);
            return $http.get(url);
        };

    }
})();