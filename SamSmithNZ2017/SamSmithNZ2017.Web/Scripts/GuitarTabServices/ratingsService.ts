(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('ratingsService', ratingsService);
    ratingsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function ratingsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitarservice.azurewebsites.net/';

        this.getRatings = function () {
            let url: string = baseUrl + 'api/Rating/GetRatings';
            //console.log(url);
            return $http.get(url);
        };

    }
})();