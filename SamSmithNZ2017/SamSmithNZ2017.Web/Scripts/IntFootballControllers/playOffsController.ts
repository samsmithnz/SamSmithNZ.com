(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('playOffsController', playOffsController);
    playOffsController.$inject = ['$scope', '$http', 'tournamentService', 'gameService'];

    function playOffsController($scope, $http, tournamentService, gameService) {

        $scope.games = [];
        $scope.showDebugInformation = false;

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

            var gameCount = 0;
            for (var i = 0; i < $scope.games.length; i++) {
                if ($scope.games[i].RowType == 1) {
                    gameCount++;
                }
            }
            //console.log('games: ' + gameCount);

            switch (gameCount) {
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

            var targets2 = document.querySelector('#lblBreadCrumbLocation');
            //console.log(targets2.innerText);
            if ($scope.tournament != null) {
                targets2.innerHTML = $scope.tournament.TournamentName + " - Playoffs ";
            }
        }

        var onGetTournamentEventComplete = function (response) {
            $scope.tournament = response.data;

            var targets2 = document.querySelector('#lblBreadCrumbLocation');
            //console.log(targets2.innerText);
            if ($scope.tournament != null) {
                targets2.innerHTML = $scope.tournament.TournamentName + " - Playoffs ";
            }
        }

        $scope.tournamentCode = getUrlParameter('TournamentCode');
        $scope.roundNumber = getUrlParameter('RoundNumber');

        tournamentService.getTournament($scope.tournamentCode).then(onGetTournamentEventComplete, onError);
        gameService.getGamesForPlayoffs($scope.tournamentCode, $scope.roundNumber).then(onGetGamesEventComplete, onError);

        $scope.findGame = function (gameNumber) {
            for (var i = 0; i <= $scope.games.length - 1; i++) {
                if ($scope.games[i].GameNumber == gameNumber) {
                    return $scope.games[i];
                }
            }
            return null;
        }

        //Style the game rows to group game details with goal details
        $scope.getGameRowStyle = function (gameCode) {
            var trStyle = "";
            if ((gameCode % 2) == 1) {
                trStyle = "#f9f9f9";
            }
            else {
                trStyle = "white";
            }
            //console.log("GameCode: " + gameCode + ", style:" + trStyle);
            return trStyle;
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
