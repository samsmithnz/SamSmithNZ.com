(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('teamController', teamController);
    teamController.$inject = ['$scope', '$http', 'teamService', 'gameService'];

    function teamController($scope, $http, teamService, gameService) {

        $scope.team = null;
        $scope.games = [];
        $scope.gamesExpectedWon = 0;
        $scope.gamesExpectedLoss = 0;
        $scope.gamesUnexpectedWin = 0;
        $scope.gamesUnexpectedLoss = 0;
        $scope.gamesUnexpectedDraw = 0;
        $scope.gamesUnknown = 0;

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetTeamEventComplete = function (response) {
            $scope.team = response.data;
            //console.log($scope.tabs);

            var lnkBreadCrumb2Visibility = 'false';
            var lnkBreadCrumb2Href = '';
            var lblBreadCrumbData2 = $scope.team.TeamName;
            var lblBreadCrumbSeperator2Visibility = 'hidden';
            var lblBreadCrumb3Visibility = 'hidden';
            var lblBreadCrumb3 = '';

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

            gameService.getGamesByTeam($scope.teamCode).then(onGetGamesEventComplete, onError);
        }

        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;

            for (var i = 0; i <= response.data.length - 1; i++) {
                if ($scope.team.TeamCode == response.data[i].Team1Code) {
                    $scope.gamesExpectedWon += response.data[i].Team1OddsCountExpectedWin;
                    $scope.gamesExpectedLoss += response.data[i].Team1OddsCountExpectedLoss;
                    $scope.gamesUnexpectedWin += response.data[i].Team1OddsCountUnexpectedWin;
                    $scope.gamesUnexpectedLoss += response.data[i].Team1OddsCountUnexpectedLoss;
                    $scope.gamesUnexpectedDraw += response.data[i].Team1OddsCountUnexpectedDraw;
                    $scope.gamesUnknown += response.data[i].Team1OddsCountUnknown;
                }
                else if ($scope.team.TeamCode == response.data[i].Team2Code) {
                    $scope.gamesExpectedWon += response.data[i].Team2OddsCountExpectedWin;
                    $scope.gamesExpectedLoss += response.data[i].Team2OddsCountExpectedLoss;
                    $scope.gamesUnexpectedWin += response.data[i].Team2OddsCountUnexpectedWin;
                    $scope.gamesUnexpectedLoss += response.data[i].Team2OddsCountUnexpectedLoss;
                    $scope.gamesUnexpectedDraw += response.data[i].Team2OddsCountUnexpectedDraw;
                    $scope.gamesUnknown += response.data[i].Team2OddsCountUnknown;
                }
            }
            //console.log($scope.games);
        }

        console.log("TeamCode: " + getUrlParameter('TeamCode'));
        $scope.teamCode = getUrlParameter('TeamCode');

        teamService.getTeam($scope.teamCode).then(onGetTeamEventComplete, onError);

        //Style the game rows to group game details with goal details
        $scope.getDidFavoriteWinStyle = function (team1Code, currentTeamCode, team1ChanceToWin, team1ResultWonGame, team2ResultWonGame) {
            var trStyle = "";
            var green = "#CCFF99";
            var red = "#FFCCCC";
            var yellow = "lightgoldenrodyellow";

            var currentTeamResultWonGame = false;
            var currentTeamChanceToWin = 0;

            if (team1Code == currentTeamCode) {
                currentTeamResultWonGame = team1ResultWonGame;
                //currentTeamChanceToWin = team1ChanceToWin;
            }
            else {
                currentTeamResultWonGame = team2ResultWonGame;
                //currentTeamChanceToWin = 100.0 - team1ChanceToWin;
            }

            if (team1ChanceToWin < 0) {
                trStyle = "#FFFFFF";
                //$scope.gamesUnknown++;
            }
            else {
                if (team1ResultWonGame == false && team2ResultWonGame == false) {
                    trStyle = yellow;
                }
                else {
                    if (currentTeamChanceToWin >= 50) {
                        if (currentTeamResultWonGame == true) {
                            trStyle = green;
                            //$scope.gamesExpectedWon++;
                        }
                        else {
                            trStyle = red;
                            //$scope.gamesExpectedLoss++;
                        }
                    }
                    else {
                        if (currentTeamResultWonGame == true) {
                            trStyle = green;
                            //$scope.gamesUnexpectedWin++;
                        }
                        else {
                            trStyle = red;
                            //$scope.gamesUnexpectedLoss++;
                        }
                    }
                }
            }
            //console.log("Style: " + trStyle);
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
