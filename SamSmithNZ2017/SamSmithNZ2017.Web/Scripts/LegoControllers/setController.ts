(function () {
    'use strict';

    angular
        .module('LegoApp')
        .controller('setController', setController);
    setController.$inject = ['$scope', '$http', 'setService', 'setPartsService'];

    function setController($scope, $http, setService, setPartsService) {

        $scope.setParts = null;
        $scope.setNum = null;

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetSetEventComplete = function (response) {
            $scope.set = response.data;
        } 

        var onGetSetPartsEventComplete = function (response) {
            $scope.setParts = response.data;
        }

        $scope.setNum = getUrlParameter('SetNum');
        setService.getSet($scope.setNum).then(onGetSetEventComplete, onError);
        setPartsService.getSetParts($scope.setNum).then(onGetSetPartsEventComplete, onError);
    }

    function getUrlParameter(param: string) {
        var sPageURL: string = (window.location.search.substring(1));
        var sURLVariables: string[] = sPageURL.split(/[&||?]/);
        var res: string;

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
