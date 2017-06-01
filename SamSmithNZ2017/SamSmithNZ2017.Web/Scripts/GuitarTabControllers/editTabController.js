(function () {
    'use strict';
    angular
        .module('GuitarTabApp')
        .controller('editTabController', editTabController);
    editTabController.$inject = ['$scope', '$http', 'tabsService', 'ratingsService', 'tuningsService', '$window'];
    function editTabController($scope, $http, tabsService, ratingsService, tuningsService, $window) {
        $scope.tab = null;
        $scope.ratings = [];
        $scope.tunings = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetTabEventComplete = function (response) {
            $scope.tab = response.data;
            //console.log($scope.tab);
        };
        var onGetRatingsEventComplete = function (response) {
            $scope.ratings = response.data;
            //console.log($scope.ratings);
            if ($scope.tunings.length > 0) {
                //console.log('Getting tab from ratings');
                getTab();
            }
        };
        var onGetTuningsEventComplete = function (response) {
            $scope.tunings = response.data;
            //console.log($scope.tunings);
            if ($scope.ratings.length > 0) {
                //console.log('Getting tab from tunings');
                getTab();
            }
        };
        var onSaveTabEventComplete = function (response) {
            $window.location.href = '/GuitarTab/Album?AlbumCode=' + $scope.tab.AlbumCode;
        };
        console.log("TabCode: " + getUrlParameter('TabCode'));
        $scope.tabCode = getUrlParameter('TabCode');
        ratingsService.getRatings().then(onGetRatingsEventComplete, onError);
        tuningsService.getTunings().then(onGetTuningsEventComplete, onError);
        var getTab = function () {
            tabsService.getTab($scope.tabCode, true).then(onGetTabEventComplete, onError);
        };
        $scope.saveTab = function () {
            $scope.tab.TabName = $('#txtTabName').val();
            $scope.tab.TabOrder = $('#txtOrder').val();
            $scope.tab.Rating = $('#cboRating').val();
            $scope.tab.RatingCode = $('#cboRating').val();
            $scope.tab.TuningCode = $('#cboTuning').val();
            $scope.tab.TabText = $('#txtTabText').val();
            console.log($scope.tab);
            tabsService.saveTab($scope.tab).then(onSaveTabEventComplete, onError);
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
