(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('albumController', albumController);
    albumController.$inject = ['$scope', '$http', 'albumService', 'songService'];
    function albumController($scope, $http, albumService, songService) {
        $scope.album = null;
        $scope.songs = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetAlbumsEventComplete = function (response) {
            //console.log(response.data);
            $scope.album = response.data;
        };
        var onGetSongsEventComplete = function (response) {
            //console.log(response.data);
            $scope.songs = response.data;
        };
        $scope.albumCode = getUrlParameter('AlbumCode');
        var albumKey = Number(getUrlParameter('AlbumKey'));
        if (albumKey > 0) {
            console.log("updating album key to album code");
            $scope.albumCode = albumKey;
        }
        albumService.getAlbum($scope.albumCode).then(onGetAlbumsEventComplete, onError);
        songService.getSongsByAlbum($scope.albumCode).then(onGetSongsEventComplete, onError);
    }
    function getUrlParameter(param) {
        var sPageURL = (window.location.search.substring(1));
        var sURLVariables = sPageURL.split(/[&||?]/);
        var res;
        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i], sParameterName = (paramName || '').split('=');
            //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
            if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
                res = sParameterName[1];
                //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
            }
        }
        return res;
    }
})();
//# sourceMappingURL=albumController.js.map