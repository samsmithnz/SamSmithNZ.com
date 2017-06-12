(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('playOffsController', playOffsController);
    playOffsController.$inject = ['$scope', '$http', 'gameService'];

    function playOffsController($scope, $http, gameService) {
        
        $scope.games = [];
        $scope.showDebugInformation = true;

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

         var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);

            $scope.show16s = true;
            $scope.showQuarters = true;
            $scope.showSemis = true;
            $scope.show3rdPlace = true;
            $scope.showFinals = true;

            console.log('games: ' + $scope.games.length);

            switch ($scope.games.length) {
                case 16:
                    //Show Everything!!!
                    break;
                case 8:
                    //Only show Quarter Finals
                    $scope.show16s = false;
                    break;
                case 7:
                    //Show Quarter Finals, hide 3rd place
                    $scope.show16s = false;
                    $scope.show3rdPlace = false;
                    break;
                case 4:
                    //Only show Semis
                    $scope.show16s = false;
                    $scope.showQuarters = false;
                    break;
                case 3:
                    //Show Semis and Finals, hide 3rd Place
                    $scope.show16s = false;
                    $scope.showQuarters = false;
                    $scope.show3rdPlace = false;
                    break;
                case 2:
                    //Show Finals & 3rd place
                    $scope.show16s = false;
                    $scope.showQuarters = false;
                    $scope.showSemis = false;
                    break;
                case 1:
                    //Only show Finals
                    $scope.show16s = false;
                    $scope.showQuarters = false;
                    $scope.showSemis = false;
                    $scope.show3rdPlace = false;
                    break;
            }
        }

         $scope.tournamentCode = getUrlParameter('TournamentCode');
         $scope.roundNumber = getUrlParameter('RoundNumber');

         gameService.getGamesForPlayoffs($scope.tournamentCode, $scope.roundNumber).then(onGetGamesEventComplete, onError);

         $scope.findGame = function (gameNumber){
             for (var i = 0; i <= $scope.games.length - 1; i++) {
                 if ($scope.games[i].GameNumber == gameNumber)
                 {
                     return $scope.games[i];
                 }
             }
             return null;
         }
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
