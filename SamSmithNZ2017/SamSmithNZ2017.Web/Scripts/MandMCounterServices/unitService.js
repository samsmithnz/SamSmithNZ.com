(function () {
    'use strict';
    angular
        .module('MandMCounterApp')
        .service('unitService', unitService);
    unitService.$inject = ['$http'];
    function unitService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        var baseUrl = 'https://mandmcounterservice.azurewebsites.net/';
        this.getUnitsForVolume = function () {
            var url = baseUrl + 'api/units/GetUnitsForVolume';
            //console.log(url);
            return $http.get(url);
        };
        this.getUnitsForContainer = function () {
            var url = baseUrl + 'api/units/GetUnitsForContainer';
            console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=unitService.js.map