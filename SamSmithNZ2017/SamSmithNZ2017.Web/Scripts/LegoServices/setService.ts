(function () {
    'use strict';

    angular
        .module('LegoApp')
        .service('setService', setService);
    setService.$inject = ['$http'];//, '$q', 'configSettings'];

    function setService($http) {//, $q, configSettings) {
        //Read config settings
        var baseUrl = 'https://ssnzlegowebservice.azurewebsites.net/';

        this.getOwnedSets = function () {
            let url: string = baseUrl + 'api/LegoOwnedSets/GetOwnedSets';
            //console.log(url);
            return $http.get(url);
        };

        this.getSet = function (setNum) {
            let url: string = baseUrl + 'api/LegoOwnedSets/GetSet?setNum=' + setNum;
            //console.log(url);
            return $http.get(url);
        }
    }
})();