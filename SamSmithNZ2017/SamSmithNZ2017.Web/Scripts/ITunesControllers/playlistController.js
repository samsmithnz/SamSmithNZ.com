(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .controller('playlistController', playlistController);
    playlistController.$inject = ['$scope', '$http', 'playlistService', 'trackService', 'topArtistsService', 'movementService', 'moment'];
    function playlistController($scope, $http, playlistService, trackService, topArtistsService, movementService, moment) {
        $scope.tracks = [];
        $scope.topArtists = [];
        $scope.movements = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onPlaylistComplete = function (response) {
            //console.log(response.data);
            var targets = angular.element(document).find('h2');
            if (targets.length > 0) {
                //console.log(moment(response.data.PlaylistDate).format('MMM-YYYY'));
                targets[0].innerText = moment(response.data.PlaylistDate).format('MMM-YYYY') + ' Analysis'; // | date:'d-MMMM';
            }
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
        playlistService.getPlaylist($scope.playlistCode).then(onPlaylistComplete, onError);
        trackService.getTracksByPlaylist($scope.playlistCode, true).then(onTracksComplete, onError);
        topArtistsService.getTopArtistsByPlaylist($scope.playlistCode, true).then(onTopArtistsComplete, onError);
        movementService.getMovementsByPlaylist($scope.playlistCode, true).then(onMovementComplete, onError);
    }
    function formatDate(date) {
        var monthNames = [
            "Jan", "Feb", "Mar", "Apr",
            "May", "Jun", "Jul", "Aug",
            "Sep", "Oct", "Nov", "Dec"
        ];
        var monthIndex = date.getMonth();
        var year = date.getFullYear();
        return monthNames[monthIndex] + '-' + year;
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
