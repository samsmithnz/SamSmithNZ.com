(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .service('ratingsService', ratingsService);
    ratingsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function ratingsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzguitartabservice.azurewebsites.net/';
        this.getRatings = function () {
            var url = baseUrl + 'api/Rating/GetRatings';
            console.log(url);
            return $http.get(url);
        };
    }
})();
