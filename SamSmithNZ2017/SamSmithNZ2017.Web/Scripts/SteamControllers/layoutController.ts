(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('layoutController', layoutController);
    layoutController.$inject = ['$scope', '$http', 'friendsService', 'playerService'];

    function layoutController($scope, $http, friendsService, playerService) {

        //console.log("here!");

        $scope.friends = [];

        var onErrorFriends = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error friends!!");
            //console.log(data);
        };

        var onErrorPlayer = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error player!!");
            //console.log(data);
        };

        var onGetFriendsEventComplete = function (response) {
            $scope.friends = response.data;
            //console.log("friends:");
            //console.log($scope.friends);
        }

        var onGetPlayerEventComplete = function (response) {
            $scope.player = response.data;
            //console.log("player:");
            //console.log($scope.player);
        }

        var steamId = getUrlParameter('SteamId');
        if (steamId == '' || steamId == null) {
            steamId = '76561197971691578';
            //console.log("Steam Id not found");
        }

        friendsService.getFriends(steamId).then(onGetFriendsEventComplete, onErrorFriends);
        playerService.getPlayer(steamId).then(onGetPlayerEventComplete, onErrorPlayer);
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
