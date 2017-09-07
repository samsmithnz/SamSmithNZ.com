(function () {
    'use strict';
    angular
        .module('FooFightersApp')
        .controller('albumListController', albumListController);
    albumListController.$inject = ['$scope', '$http', 'albumService'];
    function albumListController($scope, $http, albumService) {
        $scope.albums = [];
        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };
        var onGetAlbumsEventComplete = function (response) {
            //console.log($scope.albums);
            $scope.albums = response.data;
        };
        albumService.getAlbums().then(onGetAlbumsEventComplete, onError);
    }
    //function getUrlParameter(param: string) {
    //    var sPageURL: string = (window.location.search.substring(1));
    //    var sURLVariables: string[] = sPageURL.split(/[&||?]/);
    //    var res: string;
    //    for (var i = 0; i < sURLVariables.length; i += 1) {
    //        var paramName = sURLVariables[i], sParameterName = (paramName || '').split('=');
    //        //console.log(sParameterName[0].toLowerCase() + ' : ' + param.toLowerCase());
    //        if (sParameterName[0].toLowerCase() === param.toLowerCase()) {
    //            res = sParameterName[1];
    //            //console.log(sParameterName[0] + ' : ' + sParameterName[1]);
    //        }
    //    }
    //    return res;
    //}
})();
//# sourceMappingURL=albumListController.js.map