(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('groupController', groupController);
    groupController.$inject = ['$scope', '$http', 'groupCodeService', 'groupService', 'gameService'];

    function groupController($scope, $http, groupCodeService, groupService, gameService) {

        $scope.groupCodes = [];
        $scope.groups = [];
        $scope.games = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetGroupCodesEventComplete = function (response) {
            $scope.groupCodes = response.data;
            //console.log($scope.tabs);
            if ($scope.groupCodes != null && $scope.groupCodes.length > 0) {
                $scope.updateGroupDetails($scope.tournamentCode, $scope.roundNumber, $scope.groupCodes[0].RoundCode);
            }
        }

        var onGetGroupsEventComplete = function (response) {
            $scope.groups = response.data;            
        }

        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        }

        //console.log("TeamCode: " + getUrlParameter('TeamCode'));
        $scope.tournamentCode = getUrlParameter('TournamentCode');
        $scope.roundNumber = getUrlParameter('RoundNumber');
        $scope.roundCode = getUrlParameter('RoundCode');

        groupCodeService.getGroupCodes($scope.tournamentCode, $scope.roundNumber).then(onGetGroupCodesEventComplete, onError);

        $scope.updateGroupDetails = function (tournamentCode, roundNumber, roundCode) {
            groupService.getGroups(tournamentCode, roundNumber, roundCode).then(onGetGroupsEventComplete, onError);
            gameService.getGamesForGroup(tournamentCode, roundNumber).then(onGetGamesEventComplete, onError);
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
