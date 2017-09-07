(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .service('albumService', albumService);
    albumService.$inject = ['$http']; //, '$q', 'configSettings'];
    function albumService($http) {
        //Read config settings
        //var baseUrl = configSettings.webApiBaseUrl;
        //var baseUrl = 'http://localhost:12730/';
        var baseUrl = 'https://ssnzguitartabservice.azurewebsites.net/';
        this.getAlbums = function (isAdmin) {
            var url = baseUrl + 'api/Album/GetAlbums?isAdmin=' + isAdmin;
            //console.log(url);
            return $http.get(url);
        };
        this.getAlbum = function (albumCode, isAdmin) {
            var url = baseUrl + 'api/Album/GetAlbum?AlbumCode=' + albumCode + '&isAdmin=' + isAdmin;
            //console.log(url);
            return $http.get(url);
        };
        this.saveAlbum = function (albumItem) {
            var url = baseUrl + 'api/Album/SaveAlbum';
            //console.log(url);
            return $http.post(url, albumItem);
        };
    }
})();
//# sourceMappingURL=albumsService.js.map