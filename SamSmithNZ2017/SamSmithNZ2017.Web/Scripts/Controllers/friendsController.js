(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('friendsController', friendsController);
    friendsController.$inject = ['$scope', '$http', 'friendsService'];

    function friendsController($scope, $http, friendsService) {

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

        var steamId = $('#txtSteamId').val();
        if (steamId == '' || steamId == null) {
            steamId = '76561197971691578';
        }
        friendsService.getFriends(steamId).then(onGetFriendsEventComplete, onError);
        
    }
})();
