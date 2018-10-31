(function () {
    'use strict';

    angular
        .module('FooFightersApp')
        .service('yearService', yearService);
    yearService.$inject = ['$http'];//, '$q', 'configSettings'];

    function yearService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzfoofighterswebservice.azurewebsites.net/';

        this.getYears = function () {
            let url: string = baseUrl + 'api/Year/GetYears';
            //console.log(url);
            return $http.get(url);
        };

    }
})();