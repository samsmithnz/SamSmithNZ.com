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

        }

        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        }

        console.log("TeamCode: " + getUrlParameter('TeamCode'));
        $scope.teamCode = getUrlParameter('TeamCode');

        teamService.getTeam($scope.teamCode).then(onGetTeamEventComplete, onError);
        gameService.getGamesByTeam($scope.teamCode).then(onGetGamesEventComplete, onError);

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
