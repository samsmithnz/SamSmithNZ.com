(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('searchService', searchService);
    searchService.$inject = ['$http'];//, '$q', 'configSettings'];

    function searchService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitarservice.azurewebsites.net/';

        this.getSearchResults = function (searchText) {
            let url: string = baseUrl + 'api/Search/GetSearchResults?SearchText=' + searchText;
            //console.log(url);
            return $http.get(url);
        };

    }
})();