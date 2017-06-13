(function () {
    'use strict';
    angular
        .module('IntFootballApp')
        .controller('teamController', teamController);
    teamController.$inject = ['$scope', '$http', 'teamService', 'gameService'];
    function teamController($scope, $http, teamService, gameService) {
        $scope.team = null;
        $scope.games = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetTeamEventComplete = function (response) {
            $scope.team = response.data;
            //console.log($scope.tabs);
        };
        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        };
        console.log("TeamCode: " + getUrlParameter('TeamCode'));
        $scope.teamCode = getUrlParameter('TeamCode');
        teamService.getTeam($scope.teamCode).then(onGetTeamEventComplete, onError);
        gameService.getGamesByTeam($scope.teamCode).then(onGetGamesEventComplete, onError);
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
//# sourceMappingURL=teamController.js.map