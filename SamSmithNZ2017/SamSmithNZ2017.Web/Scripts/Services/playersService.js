//(function () {
//    'use strict';

//    angular
//        .module('PartnerDBApp')
//        .service('sessionYearService', sessionYearService);
//    sessionYearService.$inject = ['$http', '$q', 'configSettings'];
//    //var baseUrl = 'http://localhost:1555/';

//    function sessionYearService($http, $q, configSettings) {
//        //Read config settings
//        var baseUrl = configSettings.webApiBaseUrl;
//        var cacheExpiry = parseInt(configSettings.cacheExpiry);
//        var sessionYearCacheName = configSettings.sessionYearCacheName;

//        this.getSessionYears = function (employeeCode) {
//            var url = baseUrl + 'api/SessionYear/GetSessionYears?employeeCode=' + employeeCode + '&applicationCode=PartnerDB';

//            var deferred = $q.defer();

//            //If data is in cache and cache is not dirty
//            if (amplify.store(sessionYearCacheName)) {
//                // Resolve the deferred $q object before returning the promise
//                console.log("Getting Session Years from local storage");
//                deferred.resolve(amplify.store(sessionYearCacheName));
//            }
//            else {
//                // else- not in cache 
//                $http.get(url).then(function (response) {
//                    // Store data in the cache and then resolve
//                    console.log("Storing Session Years into local storage");
//                    amplify.store(sessionYearCacheName, response.data, { expires: cacheExpiry, type: 'localStorage' });
//                    deferred.resolve(response.data);
//                });
//            }

//            return deferred.promise;
//        }

//    }
//})();