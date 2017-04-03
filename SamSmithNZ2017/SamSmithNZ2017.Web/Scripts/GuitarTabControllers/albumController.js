(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('albumController', albumController);
    albumController.$inject = ['$scope', '$http', 'tabsService'];

    function albumController($scope, $http, tabsService) {

        $scope.tabs = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetTabsEventComplete = function (response) {
            $scope.tabs = response.data;
        }

        console.log("AlbumCode: " + getUrlParameter('AlbumCode'));
        $scope.albumCode = getUrlParameter('AlbumCode');

        tabsService.getTabs($scope.albumCode, true).then(onGetTabsEventComplete, onError);

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
