(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .controller('playlistController', playlistController);
    playlistController.$inject = ['$scope', '$http', 'trackService', 'topArtistsService', 'movementService'];
    function playlistController($scope, $http, trackService, topArtistsService, movementService) {
        $scope.tracks = [];
        $scope.topArtists = [];
        $scope.movements = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onTracksComplete = function (response) {
            $scope.tracks = response.data;
        };
        var onTopArtistsComplete = function (response) {
            $scope.topArtists = response.data;
        };
        var onMovementComplete = function (response) {
            $scope.movements = response.data;
        };
        $scope.playlistCode = getUrlParameter("playlistcode");
        console.log('playlistCode:' + $scope.playlistCode);
        trackService.getTracksByPlaylist($scope.playlistCode, true).then(onTracksComplete, onError);
        topArtistsService.getTopArtistsByPlaylist($scope.playlistCode, true).then(onTopArtistsComplete, onError);
        movementService.getMovementsByPlaylist($scope.playlistCode, true).then(onMovementComplete, onError);
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
//# sourceMappingURL=playlistController.js.map