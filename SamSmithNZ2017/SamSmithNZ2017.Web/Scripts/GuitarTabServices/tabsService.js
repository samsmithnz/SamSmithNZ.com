(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .service('tabsService', tabsService);
    tabsService.$inject = ['$http'];//, '$q', 'configSettings'];

    function tabsService($http) {//, $q, configSettings) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'http://ssnzguitartabservice.azurewebsites.net/';

        this.getTabs = function (albumCode) {
            var url = baseUrl + 'api/Tab/GetTabs?AlbumCode=' + albumCode;
            console.log(url);
            return $http.get(url);
        };

        this.getTab = function (tabCode) {
            var url = baseUrl + 'api/Tab/GetTab?AlbumCode=' + tabCode;
            console.log(url);
            return $http.get(url);
        };

        this.saveTab = function (tabItem) {
            var url = baseUrl + 'api/Album/SaveTab';
            console.log(url);
            return $http.post(url, tabItem);
        };

        this.DeleteTab = function (tabCode) {
            var url = baseUrl + 'api/Album/DeleteTab?AlbumCode=' + tabCode;
            console.log(url);
            return $http.get(url);
        };  

    }
})();