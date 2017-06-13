(function () {
    'use strict';
    angular
        .module('ITunesApp')
        .controller('layoutController', layoutController);
    layoutController.$inject = ['$scope', '$http', 'playlistService'];
    function layoutController($scope, $http, playlistService) {
        $scope.playlists = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onPlaylistComplete = function (response) {
            $scope.playlists = response.data;
            //console.log($scope.playlists);
        };
        playlistService.getPlaylists().then(onPlaylistComplete, onError);
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
//# sourceMappingURL=layoutController.js.map