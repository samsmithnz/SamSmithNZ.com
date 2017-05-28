(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'playlistService', 'topArtistsService', 'movementService'];
    function indexController($scope, $http, playlistService, topArtistsService, movementService) {
        $scope.playlists = [];
        $scope.topArtists = [];
        $scope.movements = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onPlaylistComplete = function (response) {
            $scope.playlists = response.data;
        };
        var onTopArtistsComplete = function (response) {
            $scope.topArtists = response.data;
        };
        var onMovementComplete = function (response) {
            $scope.movements = response.data;
        };
        playlistService.getPlaylists().then(onPlaylistComplete, onError);
        topArtistsService.getTopArtistsSummary(true).then(onTopArtistsComplete, onError);
        movementService.getMovementsSummary(true).then(onMovementComplete, onError);
    }
    //function getUrlParameter(param: string) {
    //    var sPageURL: string = (window.location.search.substring(1));
    //    var sURLVariables: string[] = sPageURL.split(/[&||?]/);
    //    var res: string;
    //    for (var i = 0; i < sURLVariables.length; i += 1) {
    //        var paramName = sURLVariables[i], sParameterName = (paramName || '').split('=');
    //        //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
    //        if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
    //            res = sParameterName[1];
    //            //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
    //        }
    //    }
    //    return res;
    //}
})();
//# sourceMappingURL=indexController - Copy.js.map