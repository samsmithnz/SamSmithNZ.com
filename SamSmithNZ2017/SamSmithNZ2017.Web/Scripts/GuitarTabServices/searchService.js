(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .service('searchService', searchService);
    searchService.$inject = ['$http']; //, '$q', 'configSettings'];
    function searchService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';
        this.getSearchResults = function (searchText) {
            var url = baseUrl + 'api/Search/GetSearchResults?SearchText=' + searchText;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
//# sourceMappingURL=searchService.js.map