(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('gameDetailsController', gameDetailsController);
    gameDetailsController.$inject = ['$scope', '$http', 'gameDetailsService'];

    function gameDetailsController($scope, $http, gameDetailsService) {

        $scope.gameDetails = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetGameDetailsEventComplete = function (response) {
            $scope.gameDetails = response.data;
        }

        $scope.steamId = $('#txtSteamId').val();
        if ($scope.steamId == '' || $scope.steamId == null) {
            $scope.steamId = '76561197971691578';
        }

        var appId = getUrlParameter('AppId')
        if (appId == '' || appId == null) {
            appId = '420';
            console.log("Error in app id!");
        }

        gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);

    }

    function getUrlParameter(param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            if (sParameterName[0] === param) {
                res = sParameterName[1];
            }
        }

        return res;
    }

})();
