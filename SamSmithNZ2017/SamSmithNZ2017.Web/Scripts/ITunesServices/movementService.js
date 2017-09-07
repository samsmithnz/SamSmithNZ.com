(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .service('movementService', movementService);
    movementService.$inject = ['$http']; //, '$q', 'configSettings'];
    function movementService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzitunesservice.azurewebsites.net/';
        this.getMovementsByPlaylist = function (playlistCode, showJustSummary) {
            var url = baseUrl + 'api/Movement/GetMovementsByPlaylist?playlistCode=' + playlistCode + '&showJustSummary=' + showJustSummary;
            //console.log(url);
            return $http.get(url);
        };
        this.getMovementsSummary = function (showJustSummary) {
            var url = baseUrl + 'api/Movement/GetMovementsSummary?showJustSummary=' + showJustSummary;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=movementService.js.map