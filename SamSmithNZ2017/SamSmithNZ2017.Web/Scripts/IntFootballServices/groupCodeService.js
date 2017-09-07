(function () {
    'use strict';
    angular
        .module('IntFootballApp')
        .service('groupCodeService', groupCodeService);
    groupCodeService.$inject = ['$http']; //, '$q', 'configSettings'];
    function groupCodeService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzintfootballservice.azurewebsites.net/';
        this.getGroupCodes = function (tournamentCode, roundNumber) {
            var url = baseUrl + 'api/GroupCode/GetGroupCodes?tournamentCode=' + tournamentCode + '&roundNumber=' + roundNumber;
            console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=groupCodeService.js.map