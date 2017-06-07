(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('tuningsService', tuningsService);
    tuningsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function tuningsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';

        this.getTunings = function () {
            let url: string =  baseUrl + 'api/Tuning/GetTunings';
           //console.log(url);
            return $http.get(url);
        };

    }
})();