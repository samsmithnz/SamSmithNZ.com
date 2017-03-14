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
        }

        $scope.steamId = $('#txtSteamId').val();
        if ($scope.steamId == '' || $scope.steamId == null) {
            $scope.steamId = '76561197971691578';
        }

        playerGamesService.getPlayerGames($scope.steamId).then(onGetPlayerGamesEventComplete, onError);


    }
     
})();
