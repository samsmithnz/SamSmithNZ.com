(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('tournamentController', tournamentController);
    tournamentController.$inject = ['$scope', '$http', 'tournamentService', 'tournamentTeamService'];

    function tournamentController($scope, $http, tournamentService, tournamentTeamService) {

        $scope.tournament = null;
        $scope.R1GroupArray = [];
        $scope.R2GroupArray = [];
        $scope.R3GroupArray = [];
        $scope.tournamentTeams = [];

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

            var targets2 = document.querySelector('#lblBreadCrumbLocation');
            //console.log(targets2.innerText);
            targets2.innerHTML = $scope.tournament.TournamentName;
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

        console.log("TournamentCode: " + getUrlParameter('TournamentCode'));
        $scope.tournamentCode = getUrlParameter('TournamentCode');

        tournamentService.getTournament($scope.tournamentCode).then(onGetTournamentEventComplete, onError);
        tournamentTeamService.getTournamentPlacingTeams($scope.tournamentCode).then(onGetTournamentPlacingTeamsEventComplete, onError);

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
