(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .service('tabsService', tabsService);
    tabsService.$inject = ['$http']; //, '$q', 'configSettings'];
    function tabsService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';
        this.getTabs = function (albumCode) {
            var url = baseUrl + 'api/Tab/GetTabs?AlbumCode=' + albumCode;
            //console.log(url);
            return $http.get(url);
        };
        this.getTab = function (tabCode) {
            var url = baseUrl + 'api/Tab/GetTab?TabCode=' + tabCode;
            //console.log(url);
            return $http.get(url);
        };
        this.saveTab = function (tabItem) {
            var url = baseUrl + 'api/Tab/SaveTab';
            //console.log(tabItem);
            //console.log(url);
            return $http.post(url, tabItem);
        };
        this.DeleteTab = function (tabCode) {
            var url = baseUrl + 'api/Tab/DeleteTab?TabCode=' + tabCode;
            //console.log(url);
            return $http.get(url);
        };
    }
})();
