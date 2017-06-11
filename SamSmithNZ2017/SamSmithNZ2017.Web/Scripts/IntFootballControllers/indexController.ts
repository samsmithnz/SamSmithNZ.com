(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'tournamentService'];

    function indexController($scope, $http, tournamentService) {

        $scope.tournament = [];
        $scope.currentDate = new Date();

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetTournamentsEventComplete = function (response) {
            $scope.tournaments = response.data;
            //console.log($scope.tournaments);
        }

        tournamentService.getTournaments().then(onGetTournamentsEventComplete, onError);
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
