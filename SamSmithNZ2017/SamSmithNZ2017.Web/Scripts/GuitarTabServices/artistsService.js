(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .service('artistsService', artistsService);
    artistsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function artistsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';
        this.getArtists = function (includeAllItems) {
            var url = baseUrl + 'api/Artist/GetArtists?includeAllItems=' + includeAllItems;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=artistsService.js.map