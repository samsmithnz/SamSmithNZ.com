(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('albumController', albumController);
    albumController.$inject = ['$scope', '$http', 'albumsService', 'tabsService'];

    function albumController($scope, $http, albumsService, tabsService) {

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
        }

        var onGetTabsEventComplete = function (response) {
            $scope.tabs = response.data;
            //console.log($scope.tabs);
        }

        console.log("AlbumCode: " + getUrlParameter('AlbumCode'));
        $scope.albumCode = getUrlParameter('AlbumCode');
        var isAdmin = $('#txtViewHiddenTabs').val() == 'true';

        albumsService.getAlbum($scope.albumCode, isAdmin).then(onGetAlbumEventComplete, onError);
        tabsService.getTabs($scope.albumCode, isAdmin).then(onGetTabsEventComplete, onError);
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
