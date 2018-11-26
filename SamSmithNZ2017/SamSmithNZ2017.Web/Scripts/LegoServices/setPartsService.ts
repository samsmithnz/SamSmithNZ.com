(function () {
    'use strict';

    angular
        .module('LegoApp')
        .service('setPartsService', setPartsService);
    setPartsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function setPartsService($http) {//, $q, configSettings) {
        //Read config settings
        var baseUrl = 'https://ssnzlegowebservice.azurewebsites.net/';

        this.getSetParts = function (setNum) {
            let url: string = baseUrl + 'api/LegoParts?setNum=' + setNum;
            //console.log(url);
            return $http.get(url);
        };

    }
})();