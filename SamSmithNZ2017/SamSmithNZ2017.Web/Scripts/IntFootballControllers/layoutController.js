(function () {
    'use strict';
    angular
        .module('IntFootballApp')
        .controller('layoutController', layoutController);
    layoutController.$inject = ['$scope', '$http'];
    function layoutController($scope, $http) {
        //console.log("here!");
        // $scope.artists = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        //var onGetArtistsEventComplete = function (response) {
        //    $scope.artists = response.data;
        //};
        //var onGetPlayerEventComplete = function (response) {
        //    $scope.player = response.data;
        //}
        //var steamId = getUrlParameter('SteamId');
        //if (steamId == '' || steamId == null) {
        //    steamId = '76561197971691578';
        //    //console.log("Steam Id not found");
        //}
        //var isAdmin = $('#txtViewHiddenTabs').val() == 'true';
        //console.log($('#txtViewHiddenTabs').val());
        //console.log(isAdmin);
        //artistsService.getArtists(isAdmin).then(onGetArtistsEventComplete, onError);
        //playerService.getPlayer(steamId).then(onGetPlayerEventComplete, onError);
        //$scope.searchGuitarTabs = function () {
        //    var searchText = $('#txtSearchAction').val() + '?searchText=' + $('#txtSearch').val();
        //    //console.log(encodeURI(searchText));
        //    $window.open(encodeURI(searchText), "_self");
        //};
    }
    function getUrlParameter(param) {
        var sPageURL = (window.location.search.substring(1));
        var sURLVariables = sPageURL.split(/[&||?]/);
        var res;
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
//# sourceMappingURL=layoutController.js.map