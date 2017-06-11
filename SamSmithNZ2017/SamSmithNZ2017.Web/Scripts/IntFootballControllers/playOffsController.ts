(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('playOffsController', playOffsController);
    playOffsController.$inject = ['$scope', '$http', 'gameService'];

    function playOffsController($scope, $http, gameService) {
        
        $scope.games = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

         var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        }

        console.log("TeamCode: " + getUrlParameter('TeamCode'));
        $scope.teamCode = getUrlParameter('TeamCode');

        gameService.getGamesForPlayoffs($scope.teamCode).then(onGetGamesEventComplete, onError);

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
