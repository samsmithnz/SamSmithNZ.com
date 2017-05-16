(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'albumsService'];
    function indexController($scope, $http, albumsService) {
        $scope.albums = [];
        $scope.currentArtist = '';
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetAlbumsEventComplete = function (response) {
            var currentArtist = '';
            var tempAlbums = [];
            //console.log('Original album list count: ' + response.data.length);
            for (var i = 0; i <= response.data.length - 1; i++) {
                if (response.data[i].ArtistNameTrimed != currentArtist) {
                    currentArtist = response.data[i].ArtistNameTrimed;
                    var album = angular.copy(response.data[i]);
                    album.AlbumCode = 0;
                    album.ArtistName = response.data[i].ArtistName;
                    album.ArtistNameTrimed = response.data[i].ArtistNameTrimed;
                    album.IsLeadArtist = true;
                    album.AlbumName = '' + i;
                    album.AlbumYear = 0;
                    //album.IsBassTab = true;
                    //album.IsNewAlbum = true;
                    //album.IsMiscCollectionAlbum = true;
                    //album.IncludeInIndex = true;
                    //album.IncludeOnWebsite = true;
                    album.AverageRating = 0;
                    //console.log('New album: ' + album.ArtistNameTrimed + ':' + album.AlbumName + ':' + album.IsLeadArtist);
                    tempAlbums.push(album);
                }
                tempAlbums.push(response.data[i]);
                //console.log('Album: ' + response.data[i].ArtistNameTrimed + ':' + response.data[i].AlbumName + ':' + response.data[i].IsLeadArtist);
            }
            //console.log('Final album list count: ' + $scope.albums.length);
            //console.log(tempAlbums);
            $scope.albums = tempAlbums;
        };
        //console.log("Steam Ids: Hidden: " + $('#txtSteamId').val() + ', URL: ' + getUrlParameter('SteamId'));
        //$scope.steamId = getUrlParameter('SteamId');
        //if ($scope.steamId == '' || $scope.steamId == null) {
        //    $scope.steamId = '76561197971691578';
        //    //console.log("Steam Id not found");
        //}
        var isAdmin = $('#txtViewHiddenTabs').val() == 'true';
        albumsService.getAlbums(isAdmin).then(onGetAlbumsEventComplete, onError);
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
