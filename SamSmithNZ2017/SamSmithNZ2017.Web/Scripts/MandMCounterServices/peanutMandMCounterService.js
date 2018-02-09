(function () {
    'use strict';
    angular
        .module('MandMCounterApp')
        .service('peanutMandMCounterService', peanutMandMCounterService);
    peanutMandMCounterService.$inject = ['$http'];
    function peanutMandMCounterService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        var baseUrl = 'https://mandmcounterservice.azurewebsites.net/';
        this.getDataForUnit = function (unit, quantity) {
            var url = baseUrl + 'api/PeanutMandMCounter/GetDataForUnit?unit=' + unit + '&quantity=' + quantity;
            //console.log(url);
            return $http.get(url);
        };
        this.getDataForRectangle = function (unit, height, width, length) {
            var url = baseUrl + 'api/PeanutMandMCounter/GetDataForRectangle?unit=' + unit + '&height=' + height + '&width=' + width + '&length=' + length;
            console.log(url);
            return $http.get(url);
        };
        this.getDataForCylinder = function (unit, height, radius) {
            var url = baseUrl + 'api/PeanutMandMCounter/GetDataForCylinder?unit=' + unit + '&height=' + height + '&radius=' + radius;
            console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=peanutMandMCounterService.js.map