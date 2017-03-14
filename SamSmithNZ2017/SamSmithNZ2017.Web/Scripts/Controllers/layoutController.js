(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('layoutController', layoutController);
    layoutController.$inject = ['$scope', '$http', 'friendsService', 'playerService'];

    function layoutController($scope, $http, friendsService, playerService) {

        //console.log("here!");

        $scope.friends = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetFriendsEventComplete = function (response) {
            $scope.friends = response.data;
        }

        var onGetPlayerEventComplete = function (response) {
            $scope.player = response.data;
        }

        var steamId = getUrlParameter('SteamId');
        if (steamId == '' || steamId == null) {
            steamId = '76561197971691578';
            console.log("Steam Id not found");
        }

        friendsService.getFriends(steamId).then(onGetFriendsEventComplete, onError);

        playerService.getPlayer(steamId).then(onGetPlayerEventComplete, onError);
    }

    function getUrlParameter(param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            if (sParameterName[0] === param) {
                res = sParameterName[1];
            }
        }

        return res;
    }
})();
