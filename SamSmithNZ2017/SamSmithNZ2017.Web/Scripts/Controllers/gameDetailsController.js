(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('gameDetailsController', gameDetailsController);
    gameDetailsController.$inject = ['$scope', '$http', 'gameDetailsService', 'friendsService'];

    function gameDetailsController($scope, $http, gameDetailsService, friendsService) {

        $scope.gameDetails = [];
        $scope.appFriends = null;
        $scope.error = null;

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
            error = data;
        };

        var onGetGameDetailsEventComplete = function (response) {
            $scope.gameDetails = response.data;
        }

        var onGetFriendsWithSameGameEventComplete = function (response) {
            $scope.appFriends = response.data;
        }

        $scope.steamId = getUrlParameter('SteamId');
        if ($scope.steamId == '' || $scope.steamId == null) {
            $scope.steamId = '76561197971691578';
            //console.log("Steam Id not found");
        }

        var appId = getUrlParameter('AppId');
        if (appId == '' || appId == null) {
            appId = '420';
            //console.log("Error in app id!");
        }

        //console.log('$scope.steamId: ' + $scope.steamId + ', appId: ' + appId);

        gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);
        friendsService.getFriendsWithSameGame($scope.steamId, appId).then(onGetFriendsWithSameGameEventComplete, onError);

        $scope.$watchGroup(['selectedFriend'], function (newValues, oldValues, scope) {
            if (newValues[0] != oldValues[0]) {
                console.log("newValue:" + newValues[0]);
                console.log("selectedFriend:" + $scope.selectedFriend);
                if (newValues[0] == null) {
                    gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);
                }
                else {
                    gameDetailsService.getGameWithFriendDetails($scope.steamId, appId, newValues[0]).then(onGetGameDetailsEventComplete, onError);
                }
            }
        });

    }

    function getUrlParameter(param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
            if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
                res = sParameterName[1];
                //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
            }
        }

        return res;
    }

})();
