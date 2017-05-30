(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'songService'];
    function indexController($scope, $http, songService) {
        $scope.songs = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetSongsEventComplete = function (response) {
            //console.log(response.data);
            $scope.songs = response.data;
        };
        //console.log("Steam Ids: Hidden: " + $('#txtSteamId').val() + ', URL: ' + getUrlParameter('SteamId'));
        //$scope.steamId = getUrlParameter('SteamId');
        //if ($scope.steamId == '' || $scope.steamId == null) {
        //    $scope.steamId = '76561197971691578';
        //    //console.log("Steam Id not found");
        //}
        //var isAdmin = $('#txtViewHiddenTabs').val() == 'true';
        songService.getSongs().then(onGetSongsEventComplete, onError);
    }
    //function getUrlParameter(param: string) {
    //    var sPageURL: string = (window.location.search.substring(1));
    //    var sURLVariables: string[] = sPageURL.split(/[&||?]/);
    //    var res: string;
    //    for (var i = 0; i < sURLVariables.length; i += 1) {
    //        var paramName = sURLVariables[i], sParameterName = (paramName || '').split('=');
    //        //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
    //        if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
    //            res = sParameterName[1];
    //            //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
    //        }
    //    }
    //    return res;
    //}
})();
