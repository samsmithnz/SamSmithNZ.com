(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .service('yearService', yearService);
    yearService.$inject = ['$http']; //, '$q', 'configSettings'];
    function yearService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofightersservice.azurewebsites.net/';
        this.getYears = function () {
            var url = baseUrl + 'api/Year/GetYears';
            console.log(url);
            return $http.get(url);
        };
    }
})();
