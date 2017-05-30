(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('showListController', showListController);
    showListController.$inject = ['$scope', '$http', 'yearService', 'showService'];
    function showListController($scope, $http, yearService, showService) {
        $scope.years = [];
        $scope.shows = [];
        $scope.selectedYear = null;
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetYearsEventComplete = function (response) {
            //console.log(response.data);
            $scope.years = response.data;
            if ($scope.years.length > 0) {
                $scope.selectedYear = $scope.years[0].YearCode;
                //console.log($scope.selectedYear);
                showService.getShows($scope.selectedYear).then(onGetShowsEventComplete, onError);
            }
        };
        var onGetShowsEventComplete = function (response) {
            //console.log($scope.shows);
            $scope.shows = response.data;
        };
        //$scope.ShowCode = getUrlParameter('ShowCode');
        yearService.getYears().then(onGetYearsEventComplete, onError);
        $scope.updateShows = function () {
            showService.getShows($scope.selectedYear).then(onGetShowsEventComplete, onError);
        };
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
