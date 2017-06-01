(function () {
    'use strict';
    angular
        .module('SteamApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'playerGamesService'];
    function indexController($scope, $http, playerGamesService) {
        $scope.games = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetPlayerGamesEventComplete = function (response) {
            $scope.games = response.data;
        };
        console.log("Steam Ids: Hidden: " + $('#txtSteamId').val() + ', URL: ' + getUrlParameter('SteamId'));
        $scope.steamId = getUrlParameter('SteamId');
        if ($scope.steamId == '' || $scope.steamId == null) {
            $scope.steamId = '76561197971691578';
            //console.log("Steam Id not found");
        }
        playerGamesService.getPlayerGames($scope.steamId).then(onGetPlayerGamesEventComplete, onError);
        $scope.GetImagePath = function (appID, iconURL) {
            if (iconURL == null) {
                return null;
            }
            else {
                return 'http://media.steampowered.com/steamcommunity/public/images/apps/' + appID + '/' + iconURL + '.jpg';
            }
        };
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
