(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('layoutController', layoutController);
    layoutController.$inject = ['$scope', '$http', 'artistsService', '$window'];

    function layoutController($scope, $http, artistsService, $window) {

        //console.log("here!");

        $scope.artists = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetArtistsEventComplete = function (response) {
            $scope.artists = response.data;
        };

        //var onGetPlayerEventComplete = function (response) {
        //    $scope.player = response.data;
        //}

        //var steamId = getUrlParameter('SteamId');
        //if (steamId == '' || steamId == null) {
        //    steamId = '76561197971691578';
        //    //console.log("Steam Id not found");
        //}

        artistsService.getArtists(true).then(onGetArtistsEventComplete, onError);
        //playerService.getPlayer(steamId).then(onGetPlayerEventComplete, onError);

        $scope.searchGuitarTabs = function () {
            var searchText = 'foo';
            //alert(searchText);
            console.log("Stuff!");
            $window.open('GuitarTab/SearchResults?searchText=' + searchText, "_self");
        };
    }

    function getUrlParameter(param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
            if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
                res = sParameterName[1];
                //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
            }
        }

        return res;
    }

})();
