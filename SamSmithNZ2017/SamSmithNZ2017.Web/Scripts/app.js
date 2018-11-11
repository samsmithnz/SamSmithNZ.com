//var interceptor = ["$q", "$location", function ($q, $location) {
//    return {
//        request: function (config) {
//            //Adding authorization token to header of each request
//            if (config.url != $('#lblControllerErrorPage').text()) {
//                config.headers.CustomToken = encryptedValue;
//            }
//            return config;
//        }
//    }
//}];
var app = angular.module("SteamApp", []); //, ['PartnerDBApp.settings', 'ui.bootstrap', 'ngAnimate', 'ngTouch', 'ui.grid', 'ui.grid.pinning', 'ui.grid.edit',
//'ui.grid.cellNav', 'ui.grid.autoResize', 'checklist-model', 'toaster', 'ui.grid.selection', 'angular-svg-round-progress', 'blockUI', 'ui.tree', 'kendo.directives'])
//.config(["$httpProvider", function ($httpProvider) {
//    $httpProvider.interceptors.push(interceptor);
//}]);
var app = angular.module("GuitarTabApp", []);
var app = angular.module("FooFightersApp", ['ngSanitize']);
var app = angular.module("ITunesApp", ['angularMoment']);
var app = angular.module("IntFootballApp", ['angularMoment']);
var app = angular.module("MandMCounterApp", []);
//app.run(["$rootScope", function ($rootScope) {
//    $rootScope.popup = "Ari Lerner";
//    //$rootScope.partnerSetUpData = [];
//}]);
////This gives the stack for caught exception 
//app.factory("stacktraceService", function () {
//    return ({
//        print: printStackTrace
//    });
//});
////Decorating the exception handler to catch all exceptions(Unhandled + synchronous) 
//app.provider("$exceptionHandler", {
//    $get: ["errorLogService", function (errorLogService) {
//        return (errorLogService);
//    }]
//});
//app.factory("errorLogService",
//    ["$log", "errorHandlerService",
//        function ($log, errorHandlerService) {
//            //log the given error to the remote server.
//            function log(exception, cause) {
//                // Pass off the error to the default error handler
//                // on the AngualrJS logger. This will output the
//                // error to the console (and let the application
//                // keep running normally for the user).
//                $log.error.apply($log, arguments);
//                // Now, we need to show a user friendly message and log the error to the server.
//                errorHandlerService.errorHandler(exception, cause);
//            }
//            // Return the logging function.
//            return (log);
//        }
//    ]); 
//# sourceMappingURL=app.js.map