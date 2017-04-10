(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('editTabController', editTabController);
    editTabController.$inject = ['$scope', '$http', 'tabsService'];

    function editTabController($scope, $http, tabsService) {

        $scope.tab = null;

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetTabEventComplete = function (response) {
            $scope.tab = response.data;
            //console.log($scope.tabs);
        }

        console.log("TabCode: " + getUrlParameter('TabCode'));
        $scope.tabCode = getUrlParameter('TabCode');

        tabsService.getTab($scope.tabCode, true).then(onGetTabEventComplete, onError);

        $scope.saveTab = function () {
            alert("Saving Tab!");
        };

    }

    function getUrlParameter(param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;

        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
            if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
                res = sParameterName[1];
                //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
            }
        }

        return res;
    }

})();
