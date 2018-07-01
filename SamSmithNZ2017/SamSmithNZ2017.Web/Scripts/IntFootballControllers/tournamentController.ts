(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('tournamentController', tournamentController);
    tournamentController.$inject = ['$scope', '$http', 'tournamentService', 'tournamentTeamService', 'tournamentTopGoalScorerService'];

    function tournamentController($scope, $http, tournamentService, tournamentTeamService, tournamentTopGoalScorerService) {

        $scope.tournament = null;
        $scope.R1GroupArray = [];
        $scope.R2GroupArray = [];
        $scope.R3GroupArray = [];
        $scope.tournamentTeams = [];
        $scope.tournamentTopGoalScorers = [];
        $scope.tournamentTopGoalScorersAdjusted = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetTournamentEventComplete = function (response) {
            $scope.tournament = response.data;
            //console.log($scope.tabs);

            if ($scope.tournament.R1NumberOfGroupsInRound > 0) {
                for (var i = 1; i <= $scope.tournament.R1NumberOfGroupsInRound; i++) {
                    $scope.R1GroupArray.push(i);
                }
            }
            if ($scope.tournament.R2NumberOfGroupsInRound > 0) {
                for (var i = 1; i <= $scope.tournament.R2NumberOfGroupsInRound; i++) {
                    $scope.R2GroupArray.push(i);
                }
            }
            if ($scope.tournament.R3NumberOfGroupsInRound > 0) {
                for (var i = 1; i <= $scope.tournament.R3NumberOfGroupsInRound; i++) {
                    $scope.R3GroupArray.push(i);
                }
            }

            if ($scope.tournament != null) {
                var lnkBreadCrumb2Visibility = 'false';
                var lnkBreadCrumb2Href = '';
                var lblBreadCrumbData2 = $scope.tournament.TournamentName;
                var lblBreadCrumbSeperator2Visibility = 'hidden';
                var lblBreadCrumb3Visibility = 'hidden';
                var lblBreadCrumb3 = '';

                //lnkBreadCrumb2
                if (lnkBreadCrumb2Visibility == 'true') {
                    var target1 = <HTMLAnchorElement>document.querySelector('#lnkBreadCrumb2');
                    target1.innerHTML = lblBreadCrumbData2;
                    target1.href = lnkBreadCrumb2Href;
                    console.log(target1.innerHTML);
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
            }

        }

        var onGetTournamentPlacingTeamsEventComplete = function (response) {
            if (response.data.length == 0) {
                tournamentTeamService.getTournamentQualifyingTeams($scope.tournamentCode).then(onGetTournamentPlacingTeamsEventComplete, onError);
            }
            else {
                $scope.tournamentTeams = response.data;
            }
        }

        var onGetTournamentQualifyingTeamsEventComplete = function (response) {
            $scope.tournamentTeams = response.data;
        }

        var onGetTournamentTopGoalScorersEventComplete = function (response) {
            $scope.tournamentTopGoalScorers = response.data;
            var goals = 0;
            var currentGoalIndex = -1;
            $scope.tournamentTopGoalScorersAdjusted

            //Pivot the data so that for each goal it creates a new array of players for those goals
            for (var i = 0; i < $scope.tournamentTopGoalScorers.length; i++) {
                if (goals != $scope.tournamentTopGoalScorers[i].GoalsScored) {
                    goals = $scope.tournamentTopGoalScorers[i].GoalsScored;
                    currentGoalIndex++;
                    var newGoal = {
                        Goals: goals,
                        Players: []
                    }
                    $scope.tournamentTopGoalScorersAdjusted.push(newGoal);
                }
                $scope.tournamentTopGoalScorersAdjusted[currentGoalIndex].Players.push($scope.tournamentTopGoalScorers[i]);
            }
            //console.log($scope.tournamentTopGoalScorersAdjusted);
        }

        //console.log("TournamentCode: " + getUrlParameter('TournamentCode'));
        $scope.tournamentCode = getUrlParameter('TournamentCode');

        tournamentService.getTournament($scope.tournamentCode).then(onGetTournamentEventComplete, onError);
        tournamentTeamService.getTournamentPlacingTeams($scope.tournamentCode).then(onGetTournamentPlacingTeamsEventComplete, onError);
        tournamentTopGoalScorerService.getTournamentTopGoalScores($scope.tournamentCode).then(onGetTournamentTopGoalScorersEventComplete, onError);

        $scope.getCharacter = function (code) {
            return String.fromCharCode(code);
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
