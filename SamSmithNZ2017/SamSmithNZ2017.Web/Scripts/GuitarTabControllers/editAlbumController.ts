(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('editAlbumController', editAlbumController);
    editAlbumController.$inject = ['$scope', '$http', 'albumsService', 'tabsService', '$window'];

    function editAlbumController($scope, $http, albumsService, tabsService, $window) {

        $scope.album = null;
        $scope.tabs = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetAlbumEventComplete = function (response) {
            var targets = angular.element(document).find('h2');
            if (targets.length > 0) {
                //console.log(targets);
                //console.log(response.data.ArtistName + ' - ' + response.data.AlbumName);
                targets[0].innerText = response.data.ArtistName + ' - ' + response.data.AlbumName;
            }
            //console.log(response.data);
            $scope.album = response.data;
        }

        var onGetTabsEventComplete = function (response) {
            $scope.tabs = response.data;
            //console.log($scope.tabs);
        }

        var onSaveAlbumEventComplete = function (response) {
            $window.location.href = '/GuitarTab/Album?AlbumCode=' + $scope.albumCode;
            //initialize();
        }

        console.log("AlbumCode: " + getUrlParameter('AlbumCode'));
        $scope.albumCode = getUrlParameter('AlbumCode');

        var initialize = function () {
            albumsService.getAlbum($scope.albumCode, true).then(onGetAlbumEventComplete, onError);
            tabsService.getTabs($scope.albumCode, true).then(onGetTabsEventComplete, onError);
        }
        initialize();

        $scope.saveAlbum = function () {
            $scope.album.ArtistName = $('#txtArtist').val();
            $scope.album.AlbumName = $('#txtAlbumName').val();
            $scope.album.AlbumYear = $('#txtYear').val();
            $scope.album.IsBassTab = (<HTMLInputElement>$('#chkIsBassTab')[0]).checked;
            $scope.album.IncludeInIndex = (<HTMLInputElement>$('#chkIncludeInIndex')[0]).checked;
            $scope.album.IncludeOnWebsite = (<HTMLInputElement>$('#chkIncludeOnWebsite')[0]).checked;
            $scope.album.IsMiscCollectionAlbum = (<HTMLInputElement>$('#chkIsMiscCollectionAlbum')[0]).checked;

            console.log($scope.album);

            albumsService.saveAlbum($scope.album).then(onSaveAlbumEventComplete, onError);
        };

    }

    function getUrlParameter(param: string) {
        var sPageURL: string = (window.location.search.substring(1));
        var sURLVariables: string[] = sPageURL.split(/[&||?]/);
        var res: string;

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
