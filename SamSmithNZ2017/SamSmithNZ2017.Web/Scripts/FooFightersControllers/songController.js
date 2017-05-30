(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('songController', songController);
    songController.$inject = ['$scope', '$http', 'songService', 'showService'];
    function songController($scope, $http, songService, showService) {
        $scope.song = null;
        $scope.shows = [];
        //This looks a bit weird, but I need to start with the 0 month of 1971 and minus of 1970 years to get 0001-01-01
        var d = new Date(1971, 0, 1);
        d.setFullYear(d.getFullYear() - 1970);
        $scope.minDate = d;
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetSongsEventComplete = function (response) {
            //console.log(response.data);
            $scope.song = response.data;
        };
        var onGetShowsEventComplete = function (response) {
            console.log($scope.shows);
            $scope.shows = response.data;
        };
        $scope.SongCode = getUrlParameter('SongCode');
        songService.getSong($scope.SongCode).then(onGetSongsEventComplete, onError);
        showService.getShowsBySong($scope.SongCode).then(onGetShowsEventComplete, onError);
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
