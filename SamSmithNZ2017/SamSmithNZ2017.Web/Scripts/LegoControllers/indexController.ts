(function () {
    'use strict';

    angular
        .module('LegoApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'setService'];

    function indexController($scope, $http, setService) {

        //$scope.sets = [];

        var onError = function (data) {
            console.log("Error!!");
            console.log(data);
        };

        var onGetOwnedSetsEventComplete = function (response) {
            console.log(response.data);            
            $scope.sets = response.data;
        }

        setService.getOwnedSets().then(onGetOwnedSetsEventComplete, onError);
    }

})();
