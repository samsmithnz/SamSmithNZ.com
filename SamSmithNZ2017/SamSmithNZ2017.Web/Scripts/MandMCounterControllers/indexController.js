(function () {
    'use strict';
    angular
        .module('MandMCounterApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'unitService', 'mandMCounterService', 'peanutMandMCounterService', 'skittlesMandMCounterService'];
    function indexController($scope, $http, unitService, mandMCounterService, peanutMandMCounterService, skittlesMandMCounterService) {
        $scope.result = null;
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetUnitsForVolumeEventComplete = function (response) {
            $scope.Units = response.data;
            console.log($scope.Units);
        };
        var onGetMandMCounterForUnitEventComplete = function (response) {
            $scope.MandMsResult = response.data;
            console.log($scope.MandMsResult);
        };
        var onGetPeanutMandMCounterForUnitEventComplete = function (response) {
            $scope.PeanutMandMsResult = response.data;
            console.log($scope.PeanutMandMsResult);
        };
        var onGetSkittlesCounterForUnitEventComplete = function (response) {
            $scope.SkittlesResult = response.data;
            console.log($scope.SkittlesResult);
        };
        unitService.getUnitsForVolume().then(onGetUnitsForVolumeEventComplete, onError);
        $scope.UpdateCounters = function () {
            //let mystring :string = 1;
            //mandMCounterService.getDataForUnit(mystring, 1).then(onGetMandMCounterForUnitEventComplete, onError);
            mandMCounterService.getDataForUnit('cup', 1).then(onGetMandMCounterForUnitEventComplete, onError);
            peanutMandMCounterService.getDataForUnit('cup', 1).then(onGetPeanutMandMCounterForUnitEventComplete, onError);
            skittlesMandMCounterService.getDataForUnit('cup', 1).then(onGetSkittlesCounterForUnitEventComplete, onError);
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
//# sourceMappingURL=indexController.js.map