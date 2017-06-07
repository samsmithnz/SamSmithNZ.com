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
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';

        this.getTabs = function (albumCode) {
            let url: string = baseUrl + 'api/Tab/GetTabs?AlbumCode=' + albumCode;
            //console.log(url);
            return $http.get(url);
        };

        this.getTab = function (tabCode) {
            let url: string = baseUrl + 'api/Tab/GetTab?TabCode=' + tabCode;
            //console.log(url);
            return $http.get(url);
        };

        this.saveTab = function (tabItem) {
            let url: string = baseUrl + 'api/Tab/SaveTab';
            //console.log(tabItem);
            //console.log(url);
            return $http.post(url, tabItem);
        };

        this.DeleteTab = function (tabCode) {
            let url: string = baseUrl + 'api/Tab/DeleteTab?TabCode=' + tabCode;
            //console.log(url);
            return $http.get(url);
        };

    }
})();