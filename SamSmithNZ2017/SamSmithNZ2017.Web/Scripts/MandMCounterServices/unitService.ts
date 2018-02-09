(function () {
    'use strict';

    angular
        .module('MandMCounterApp')
        .service('unitService', unitService);
    unitService.$inject = ['$http'];

    function unitService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        var baseUrl = 'https://mandmcounterservice.azurewebsites.net/';

        this.getUnitsForVolume = function () {
            let url: string = baseUrl + 'api/units/GetUnitsForVolume';
            //console.log(url);
            return $http.get(url);
        };

        this.getUnitsForContainer = function () {
            let url: string = baseUrl + 'api/units/GetUnitsForContainer';
            console.log(url);
            return $http.get(url);
        };

    }
})();