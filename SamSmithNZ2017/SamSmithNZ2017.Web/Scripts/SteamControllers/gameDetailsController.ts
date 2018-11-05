(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('gameDetailsController', gameDetailsController);
    gameDetailsController.$inject = ['$scope', '$http', 'gameDetailsService', 'friendsService'];

    function gameDetailsController($scope, $http, gameDetailsService, friendsService) {

        $scope.gameDetails = [];
        //$scope.appFriends = null;
        $scope.error = null;
        //$scope.onlyShowFriendChanges = null;
        //$scope.Math = window.Math; //inject Math into my scope

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
            $scope.error = data;
        };

        var onGetGameDetailsEventComplete = function (response) {
            $scope.gameDetails = response.data;
        };

        //var onGetFriendsWithSameGameEventComplete = function (response) {
        //    $scope.appFriends = response.data;
        //};

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
        //$scope.onlyShowFriendChanges = false;
        gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);
        //friendsService.getFriendsWithSameGame($scope.steamId, appId).then(onGetFriendsWithSameGameEventComplete, onError);

        //$scope.$watchGroup(['selectedFriend'], function (newValues, oldValues, scope) {
        //    if (newValues[0] != oldValues[0]) {
        //        //console.log("newValue:" + newValues[0]);
        //        console.log("selectedFriend:" + $scope.selectedFriend);
        //        if (newValues[0] == null) {
        //            gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);
        //        }
        //        else {
        //            gameDetailsService.getGameWithFriendDetails($scope.steamId, appId, newValues[0]).then(onGetGameDetailsEventComplete, onError);
        //        }
        //    }
        //});

        //$scope.$watchGroup(['selectedAchievementFilter'], function (newValues, oldValues, scope) {
        //    if (newValues[0] != oldValues[0]) {
        //        //console.log("newValue:" + newValues[0]);
        //        console.log("selectedAchievementFilter:" + $scope.selectedAchievementFilter);
        //        if (newValues[0] == null) {
        //            gameDetailsService.getGameDetails($scope.steamId, appId).then(onGetGameDetailsEventComplete, onError);
        //        }
        //        else {
        //            gameDetailsService.getGameWithFriendDetails($scope.steamId, appId, newValues[0]).then(onGetGameDetailsEventComplete, onError);
        //        }
        //    }
        //});

        //$scope.showFriendChanges = function () {
        //    //$scope.onlyShowFriendChanges = !$scope.onlyShowFriendChanges;
        //    //console.log($scope.onlyShowFriendChanges);
        //    //gameDetailsService.getGameWithFriendDetails($scope.steamId, appId, $scope.selectedFriend).then(onGetGameDetailsEventComplete, onError);
        //};

        $scope.achievementFilter = function (a) {
            if ($scope.showAllAchievements == true) {
                return true;
            }
            else {
                if (a.Achieved == true) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        //$scope.filterFunction = function (item) {
        //    //return item.FriendsAchieved == true;
        //    if ($scope.showFriendChanges == true) {
        //        return item.FriendsAchieved != item.Achieved;
        //    }
        //    else {
        //        return true;
        //    }
        //};

        //$scope.getFriendStyle = function (selectedFriend, achieved, friendAchieved) {

        //    if (selectedFriend) {
        //        if (achieved == true && friendAchieved == false) {
        //            return "lightgreen";
        //        }
        //        else if (achieved == false && friendAchieved == true) {
        //            return "#F75D59";
        //        }
        //    }
        //};

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
