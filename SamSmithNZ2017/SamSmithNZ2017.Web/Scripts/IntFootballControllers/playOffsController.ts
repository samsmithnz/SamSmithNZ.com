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
            console.log($scope.games);

            $scope.show16s = true;
            $scope.showQuarters = true;
            $scope.showSemis = true;
            $scope.show3rdPlace = true;
            $scope.showFinals = true;

            var gameCount = 0;
            var firstRow3 = true;
            $scope.RowType3FirstRow = 0;
            for (var i = 0; i < $scope.games.length; i++) {
                if ($scope.games[i].RowType == 1) {
                    gameCount++;
                }
                else if ($scope.games[i].RowType == 3 && firstRow3 == true) {
                    //Mark the first row of PK's with a tag so we can use a label in the UI
                    firstRow3 = false;
                    if ($scope.games[i].Team1PenaltiesScore != null) {
                        $scope.games[i].FifaRanking = $scope.games[i].Team1Code;
                    }
                    else if ($scope.games[i].Team2PenaltiesScore != null) {
                        $scope.games[i].FifaRanking = $scope.games[i].Team2Code;
                    }
                }
                else if ($scope.games[i].RowType != 3 && firstRow3 == false) {
                    firstRow3 = true;
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
            };
        };

        var onGetTournamentEventComplete = function (response) {
            $scope.tournament = response.data;
            //console.log($scope.tournament);

            if ($scope.tournament != null) {
                var lnkBreadCrumb2Visibility = 'true';
                var lnkBreadCrumb2Href = '/IntFootball/Tournament?tournamentCode=' + $scope.tournament.TournamentCode;
                var lblBreadCrumbData2 = $scope.tournament.TournamentName;
                var lblBreadCrumbSeperator2Visibility = 'visible';
                var lblBreadCrumb3Visibility = 'visible';
                var lblBreadCrumb3 = 'Playoffs';

                //lnkBreadCrumb2
                if (lnkBreadCrumb2Visibility == 'true') {
                    var target1 = <HTMLAnchorElement>document.querySelector('#lnkBreadCrumb2');
                    target1.innerHTML = lblBreadCrumbData2;
                    target1.href = lnkBreadCrumb2Href;
                    //console.log(target1.innerHTML);
                }
                else {
                    var target1a = document.querySelector('#lnkBreadCrumb2');
                    var c = $('#lnkBreadCrumb2').contents().unwrap();
                    console.log(c[0].nodeValue);
                    c[0].nodeValue = lblBreadCrumbData2;
                    //console.log(lblBreadCrumbData2);
                }

                //lblBreadCrumbSeperator2
                var target3 = <HTMLElement>document.querySelector('#lblBreadCrumbSeperator2');
                target3.style.visibility = lblBreadCrumbSeperator2Visibility;
                //console.log(target3.innerHTML);

                //lblBreadCrumb3
                var target4 = <HTMLElement>document.querySelector('#lblBreadCrumb3');
                target4.style.visibility = lblBreadCrumb3Visibility;
                target4.innerHTML = lblBreadCrumb3;
                //console.log(target4.innerHTML);
            };
        };

        $scope.tournamentCode = getUrlParameter('TournamentCode');
        $scope.roundNumber = getUrlParameter('RoundNumber');

        tournamentService.getTournament($scope.tournamentCode).then(onGetTournamentEventComplete, onError);
        gameService.getGamesForPlayoffs($scope.tournamentCode, $scope.roundNumber).then(onGetGamesEventComplete, onError);

        $scope.findGame = function (gameNumber) {
            for (var i = 0; i <= $scope.games.length - 1; i++) {
                if ($scope.games[i].GameNumber == gameNumber && ($scope.games[i].Team1Code > 0 || $scope.games[i].Team2Code > 0)) {
                    return $scope.games[i];
                }
            }
            return null;
        };

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
    };

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
    };

})();
