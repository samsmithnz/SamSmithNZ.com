(function () {
    'use strict';

    angular
        .module('GuitarTabApp')
        .controller('searchController', searchController);
    searchController.$inject = ['$scope', '$http', 'searchService'];

    function searchController($scope, $http, searchService) {

        $scope.searchResults = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetSearchEventComplete = function (response) {            
            $scope.searchResults = response.data;
            //console.log($scope.searchResults);
        }

        //console.log("Steam Ids: Hidden: " + $('#txtSteamId').val() + ', URL: ' + getUrlParameter('SteamId'));
        $scope.searchText = getUrlParameter('searchText');
        //if ($scope.steamId == '' || $scope.steamId == null) {
        //    $scope.steamId = '76561197971691578';
        //    //console.log("Steam Id not found");
        //}

        searchService.getSearchResults($scope.searchText).then(onGetSearchEventComplete, onError);

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
