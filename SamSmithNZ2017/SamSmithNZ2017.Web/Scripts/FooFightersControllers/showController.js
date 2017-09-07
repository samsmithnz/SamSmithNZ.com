(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('showController', showController);
    showController.$inject = ['$scope', '$http', 'showService', 'songService'];
    function showController($scope, $http, showService, songService) {
        $scope.show = null;
        $scope.songs = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetShowsEventComplete = function (response) {
            //console.log($scope.show);
            $scope.show = response.data;
        };
        var onGetSongsEventComplete = function (response) {
            //console.log(response.data);
            $scope.songs = response.data;
        };
        $scope.showCode = getUrlParameter('ShowCode');
        var showKey = Number(getUrlParameter('ShowKey'));
        if (showKey > 0) {
            console.log("updating show key to show code");
            $scope.showCode = showKey;
        }
        showService.getShow($scope.showCode).then(onGetShowsEventComplete, onError);
        songService.getSongsByShow($scope.showCode).then(onGetSongsEventComplete, onError);
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
//# sourceMappingURL=showController.js.map