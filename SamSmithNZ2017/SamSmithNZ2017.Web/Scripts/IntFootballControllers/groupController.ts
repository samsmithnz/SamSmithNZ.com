(function () {
    'use strict';

    angular
        .module('IntFootballApp')
        .controller('groupController', groupController);
    groupController.$inject = ['$scope', '$http', 'tournamentService', 'groupCodeService', 'groupService', 'gameService'];

    function groupController($scope, $http, tournamentService, groupCodeService, groupService, gameService) {

        $scope.groupCodes = [];
        $scope.groups = [];
        $scope.games = [];
        $scope.roundCode = '';

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetGroupCodesEventComplete = function (response) {
            $scope.groupCodes = response.data;
            console.log('$scope.roundCode:' + $scope.roundCode);
            if ($scope.groupCodes != null && $scope.groupCodes.length > 0 && $scope.roundCode == '') {
                console.log('setting round code from ' + $scope.roundCode + ' to ' + $scope.groupCodes[0].RoundCode);
                $scope.roundCode = $scope.groupCodes[0].RoundCode;
            }
            $scope.updateGroupDetails($scope.tournamentCode, $scope.roundNumber, $scope.roundCode);
        }

        var onGetGroupsEventComplete = function (response) {
            $scope.groups = response.data;

            if ($scope.tournament != null) {
                var lnkBreadCrumb2Visibility = 'true';
                var lnkBreadCrumb2Href = '/IntFootball/Tournament?tournamentCode=' + $scope.tournament.TournamentCode;
                var lblBreadCrumbData2 = $scope.tournament.TournamentName;
                var lblBreadCrumbSeperator2Visibility = 'visible';
                var lblBreadCrumb3Visibility = 'visible';
                var lblBreadCrumb3 = 'Group ' + $scope.roundCode;

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

        }

        var onGetTournamentEventComplete = function (response) {
            $scope.tournament = response.data;

            //var targets2 = document.querySelector('#lblBreadCrumbData2');
            ////console.log(targets2.innerText);
            //if ($scope.tournament != null) {
            //    targets2.innerHTML = $scope.tournament.TournamentName + " - Group " + $scope.roundCode;
            //}
        }

        var onGetGamesEventComplete = function (response) {
            $scope.games = response.data;
            //console.log($scope.games);
        }

        $scope.tournamentCode = getUrlParameter('TournamentCode');
        $scope.roundNumber = getUrlParameter('RoundNumber');
        $scope.roundCode = getUrlParameter('RoundCode');
        if (!$scope.roundCode) {
            $scope.roundCode = '';
        }
        $scope.isLastRound = getUrlParameter('IsLastRound');
        //console.log("isLastRound: " + $scope.isLastRound);

        tournamentService.getTournament($scope.tournamentCode).then(onGetTournamentEventComplete, onError);
        groupCodeService.getGroupCodes($scope.tournamentCode, $scope.roundNumber).then(onGetGroupCodesEventComplete, onError);

        $scope.updateGroupDetails = function (tournamentCode, roundNumber, roundCode) {
            groupService.getGroups(tournamentCode, roundNumber, roundCode).then(onGetGroupsEventComplete, onError);
            gameService.getGamesForGroup(tournamentCode, roundNumber, roundCode).then(onGetGamesEventComplete, onError);
        };

        //Style the group rows depending on the the status of the group
        $scope.getGroupRowStyle = function (hasQualifiedForNextRound, groupRanking, isLastRound) {
            var trStyle = "";
            if (isLastRound == 'true') {
                switch (groupRanking) {
                    case 1:
                        trStyle = "gold";
                        break;
                    case 2:
                        trStyle = "silver";
                        break;
                    case 3:
                        trStyle = "#A67D3D";
                        break;
                }
            }
            else {
                if (hasQualifiedForNextRound == true) {
                    trStyle = "#CCFF99";
                }
            }
            return trStyle;
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
