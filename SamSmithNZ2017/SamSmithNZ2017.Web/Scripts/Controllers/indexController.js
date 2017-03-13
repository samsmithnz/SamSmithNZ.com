(function () {
    'use strict';

    angular
        .module('SteamApp')
        .controller('indexController', indexController);
    indexController.$inject = ['$scope', '$http', 'playerGamesService'];

    function indexController($scope, $http, playerGamesService) {

        $scope.games = [];

        var onError = function (data) {
            //errorHandlerService.errorHandler(data);
            console.log("Error!!");
            console.log(data);
        };

        var onGetPlayerGamesEventComplete = function (response) {
            $scope.games = response.data;
        }

        $scope.steamId = $('#txtSteamId').val();
        if ($scope.steamId == '' || $scope.steamId == null) {
            $scope.steamId = '76561197971691578'
        }

        playerGamesService.getPlayerGames($scope.steamId).then(onGetPlayerGamesEventComplete, onError);


    }
    {

        //        $scope.data = [];
        //        $scope.sessionAccessLevelCode = $('#lblSessionAccessLevelCode').text();
        //        $scope.loggedInEmployeeCode = $('#lblLoggedInEmployeeCode').text();
        //        $scope.sessionSecurityData = [];
        //        $scope.SessionAccessLevelAll = [];
        //        $scope.selectedFeature = '';
        //        $scope.featureQuickLinks = 0;
        //        $scope.MaxYear = 0;
        //        $scope.Years = [];
        //        $scope.hideOfficeHierarchySection();
        //        //Removing add employee and filters
        //        $('#AddPartner').hide();
        //        $('#search').hide();
        //        $('#searchImage').hide();
        //        $scope.isEditable = utilityService.checkReadWriteAccess();


        //        //GRID COLUMNS AND TEMPLATE DEFINITIONS
        //        {
        //            //Templates to to UI grid columns to achieve complex UI columns.
        //            var CellAnchortemplate = $templateCache.get('CellAnchortemplatePartnerLocation');

        //            var cellCheckTemplate = $templateCache.get('cellCheckTemplate');
        //            var CellEditableNumberTemplate = $templateCache.get('CellEditableNumberTemplate');
        //            var SessionAccessLevelDropDown = $templateCache.get('SessionAccessLevelDropDown');
        //            var SessionAccessLevelStatic = $templateCache.get('SessionAccessLevelStatic');

        //            $scope.sessionSecurityGrid = {
        //                data: 'sessionSecurityData',
        //                enableSorting: true,
        //                enableCellEditOnFocus: true,
        //                showColumnFooter: true,
        //                useExternalSorting: true,
        //                columnDefs: [
        //                    { name: 'Role', field: 'SecurityRoleName', enableCellEdit: false, enableColumnMenu: false, minWidth: 200 },
        //                    { name: 'Feature', field: 'SecurityFeatureName', enableCellEdit: false, enableColumnMenu: false, minWidth: 200 },
        //                    {
        //                        name: 'Access', field: 'SessionAccessLevelName', cellTemplate: SessionAccessLevelStatic, editableCellTemplate: SessionAccessLevelDropDown,
        //                        cellEditableCondition: function (scope) {
        //                            if ($scope.isEditable == true)
        //                                return true;
        //                            else
        //                                return false;
        //                        }, cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
        //                            if ($scope.isEditable == true)
        //                                return 'editableCell';
        //                            else
        //                                return 'left'
        //                        }, editDropdownValueLabel: 'Name', editDropdownIdLabel: 'Name', enableCellEdit: true, enableColumnMenu: false, minWidth: 200
        //                    },
        //                ]
        //            };
        //        }

        //        //ROUTINE FOR SAVING PARTNER SETUP
        //        $scope.sessionSecurityGrid.onRegisterApi = function (gridApi) {
        //            //set gridApi on scope
        //            var lastRowEntity; var lastColDef;
        //            var validationFailed = false;
        //            $scope.gridApi = gridApi;

        //            gridApi.edit.on.afterCellEdit($scope, function (rowEntity, colDef, newValue, oldValue) {
        //                lastRowEntity = rowEntity;
        //                lastColDef = colDef;
        //                $scope.oldVal = oldValue;
        //                $scope.newVal = newValue;
        //                $scope.selectedRowEntity = rowEntity;
        //                $scope.selectedColumn = colDef.field;

        //                if (newValue != oldValue) {
        //                    //Setting up defaults before hand

        //                    var sessionSecurity = {};
        //                    switch (colDef.field) {
        //                        case 'SessionAccessLevelName':
        //                            sessionSecurity.Id = rowEntity.Id;
        //                            sessionSecurity.SessionAccessLevelCode = findSessionAccessLevelCode(rowEntity.SessionAccessLevelName);
        //                            sessionSecurity.LastUpdatedBy = $scope.loggedInEmployeeCode;
        //                            validationFailed = false;
        //                            break;
        //                    }
        //                    if (!validationFailed) {
        //                        sessionSecurityService.saveSessionSecurity(sessionSecurity).then(onSaveSessionSecurityEventComplete, onError);
        //                    }
        //                }
        //                $scope.$apply();
        //            });
        //            gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
        //                if (sortColumns[0] != undefined) {
        //                    if (sortColumns[0].sort.direction == 'asc')
        //                        $scope.sessionSecurityData.sort(utilityService.sortBy(sortColumns[0].field, false));
        //                    else
        //                        $scope.sessionSecurityData.sort(utilityService.sortBy(sortColumns[0].field, true));
        //                }
        //            });
        //        };

        //        $scope.saveSessionSecurity = function (id, item) {
        //            var sessionSecurity = {};
        //            //console.log('saving id: ' + id + ', accesscode: ' + item);
        //            sessionSecurity.Id = id;
        //            sessionSecurity.SessionAccessLevelCode = item;
        //            sessionSecurity.LastUpdatedBy = $('#lblEmployeeCode').text();
        //            sessionSecurityService.saveSessionSecurity(sessionSecurity).then(onSaveSessionSecurityEventComplete, onError);
        //        }

        //        $scope.sessionSecurityFeatureChangedValue = function (item) {
        //            //console.log('securityRoleChangedValue:' + item)

        //            $scope.selectedFeature = item;
        //            if (item === null) {
        //                $scope.sessionSecurityData = [];
        //                $scope.featureQuickLinks = 0;
        //            }
        //            else {
        //                $scope.featureQuickLinks = 1;
        //                sessionSecurityService.GetSessionSecuritysByYearByFeature($scope.selectedYear, $scope.selectedFeature).then(onGetSessionSecuritysByYearByFeatureComplete, onError);
        //            }
        //        }

        //        $scope.bindSessionAccessLevelList = function (rowEntity) {
        //            var maxSessionAccessLevelCode = rowEntity.MaxSessionAccessLevelCode;
        //            var sessionAccessLevel = [];

        //            angular.forEach($scope.SessionAccessLevelAll, function (item) {
        //                if (item.SessionAccessLevelCode <= maxSessionAccessLevelCode) {
        //                    sessionAccessLevel.push(item);
        //                }
        //            });

        //            $scope.sessionSecurityGrid.columnDefs[2].editDropdownOptionsArray = sessionAccessLevel;
        //        };

        //        //PAGE HELPERS
        //        {

        //            var onError = function (data) {
        //                errorHandlerService.errorHandler(data);
        //            };

        //            var findSessionAccessLevelCode = function (Name) {
        //                var obj = $scope.SessionAccessLevelAll;
        //                var result = $.grep(obj, function (e) { return e.Name == Name; });
        //                return result[0].SessionAccessLevelCode;
        //            }

        //            //Routine to cater add session request
        //            $scope.AddNewSession = function () {
        //                if ($scope.MaxYear != null) {
        //                    var newSessionYear = $scope.MaxYear + 1;
        //                    var sessionSecurity = {};
        //                    sessionSecurity.SessionYearCode = newSessionYear;
        //                    sessionSecurity.LoadPartners = true;
        //                    sessionSecurity.LastUpdatedBy = $('#lblEmployeeCode').text();
        //                    sessionSecurityService.saveSessionYear(sessionSecurity).then(onSaveSessionYearComplete, onError);
        //                }
        //            };

        //            //Routine cater quick links
        //            $scope.CaterQuickLink = function (quickId) {
        //                if ($scope.selectedYear != null) {
        //                    var sessionSecurity = {};
        //                    sessionSecurity.SessionYearCode = $scope.selectedYear;
        //                    sessionSecurity.IsBatchUpdate = true; //This indicates we are going to update a batch of items
        //                    if ($scope.selectedFeature != null) {
        //                        sessionSecurity.SecurityFeatureId = $scope.selectedFeature;
        //                    }

        //                    switch (quickId) {
        //                        case 1:
        //                            sessionSecurity.SessionAccessLevelCode = 2
        //                            break;
        //                        case 2:
        //                            sessionSecurity.SessionAccessLevelCode = 1
        //                            break;
        //                        case 3:
        //                            sessionSecurity.SessionAccessLevelCode = 0
        //                            break;
        //                        case 4:
        //                            sessionSecurity.SessionAccessLevelCode = 1;
        //                            sessionSecurity.SecurityFeatureId = null;
        //                            break;
        //                    }
        //                    sessionSecurity.LastUpdatedBy = $('#lblEmployeeCode').text();
        //                    sessionSecurityService.saveSessionSecurity(sessionSecurity).then(onSaveSessionSecurityEventComplete, onError);
        //                }
        //            };
        //        }

        //        //COMPLETE FUNCTIONS
        //        {
        //            var onSaveSessionYearComplete = function (response) {
        //                //Success! 
        //                toaster.pop('success', "", "Session added successfully");
        //            };

        //            var onGetSessionSecuritysByYearByFeatureComplete = function (response) {
        //                //Success! 
        //                //Filter out all the roles with no acess
        //                response.data = jQuery.grep(response.data, function (a) { return a['SessionAccessLevelCode'] != -1; });
        //                $scope.sessionSecurityData = response.data;
        //            };

        //            var onGetSessionSecuritysByYearComplete = function (response) {
        //                //Success! 
        //                $scope.sessionSecurityData = response.data;

        //            };
        //            var onGetSessionSecuritysComplete = function (response) {
        //                //Success! 
        //                $scope.data = response.data;
        //                angular.forEach($scope.data, function (item) {
        //                    item.SessionAccessLevelAll = {};
        //                    item.SessionAccessLevelAll.AccessLevelCode = item.SessionAccessLevelCode;
        //                });
        //            };

        //            var ondownloadToExcelComplete = function (response) {
        //                var fileName = response.data;
        //                var downloadPath = configSettings.excelFileLocationPath + fileName;

        //                $("body").append("<iframe src='" + downloadPath + "' style='display: none;' ></iframe>");
        //                console.log('Download complete');

        //            };

        //            var onGetSessionYearsComplete = function (data) {
        //                //Success! 
        //                $scope.Years = data; //This is data instead of response.data, as we are caching it
        //                $scope.MaxYear = Math.max.apply(Math, $scope.Years.map(function (o) { return o.SessionYearCode; }));
        //            };

        //            var onSaveSessionSecurityEventComplete = function (response) {
        //                //TODO: do something to notify the user that this row has saved
        //                console.log("session access type code saved successfully");
        //                sessionSecurityService.GetSessionSecuritysByYearByFeature($scope.selectedYear, $scope.selectedFeature).then(onGetSessionSecuritysByYearByFeatureComplete, onError);
        //                toaster.pop('success', "", "Session access level(s) saved successfully");
        //            }

        //            var onGetSecurityFeaturesComplete = function (response) {
        //                $scope.securityFeatures = response.data;
        //            }

        //            var onGetAccessLevelsComplete = function (response) {
        //                $scope.sessionSecurityGrid.columnDefs[2].editDropdownOptionsArray = response.data;
        //                $scope.SessionAccessLevelAll = response.data;
        //            };

        //            var onGetAccessInfoComplete = function (response) {
        //                //Success! 
        //                if (response.data) {
        //                    if (response.data.SessionAccessLevelCode == 2) {
        //                        $scope.isEditable = true;
        //                    }
        //                    else {
        //                        $scope.isEditable = false;
        //                    }
        //                }

        //                if ($scope.gridApi != null) {
        //                    $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
        //                }
        //            };
        //       }

        //        //Watch the layout page session year dropdown for changes
        //        //WATCH THE LAYOUT PAGE SESSION YEAR DROPDOWN FOR CHANGES AND OFFICE DROPDOWN FOR CHANGES
        //        $scope.$watchGroup(['selectedYear'], function (newValues, oldValues, scope) {
        //            // newValues array contains the current values of the watch expressions
        //            // with the indexes matching those of the watchExpression array   

        //            if ($scope.selectedYear != null) {
        //                commonDataService.getAccessInfo($scope.selectedYear, $('#lblSecurityRoleId').text(), currentPageFeatureID).then(onGetAccessInfoComplete, onError);
        //                if ($scope.selectedFeature === '' || $scope.selectedFeature === null) {
        //                    //sessionSecurityService.GetSessionSecuritysByYear($scope.selectedYear).then(onGetSessionSecuritysByYearComplete, onError);
        //                }
        //                else {
        //                    sessionSecurityService.GetSessionSecuritysByYearByFeature($scope.selectedYear, $scope.selectedFeature).then(onGetSessionSecuritysByYearByFeatureComplete, onError);
        //                }
        //                securityFeatureService.getSecurityFeatures().then(onGetSecurityFeaturesComplete, onError);
        //                sessionSecurityAccessLevelService.getSessionSecurityAccessLevels().then(onGetAccessLevelsComplete, onError);
        //                sessionYearService.getSessionYears($('#lblEmployeeCode').text()).then(onGetSessionYearsComplete, onError);
        //            }
        //        });

        //        //  ROUTINE FOR EXCEL DOWNLOAD
        //        $scope.$on('downloadExcelEvent', function (event) {
        //            if ($scope.selectedFeature === '' || $scope.selectedFeature === null) {
        //                validationService.raiseNotification('featureRequired');
        //            }
        //            else {
        //                var filter = null;
        //                filter = "featureID=" + $scope.selectedFeature;

        //                excelExportService.downloadToExcel($scope.selectedYear, 35, 'SessionSecurity', filter).then(ondownloadToExcelComplete, onError);
        //            }
        //        })

        //        //Only show the session access to the max level of the security and remove the -1
        //        //e.g. If the role only has read access to a feature, then the session security can never set that role to be Open to input.
        //        $scope.sessionAccessFilter = function (prop, val) {
        //            return function (item) {
        //                if (item[prop] <= val && item[prop] >= 0) return true;
        //            }
        //        }
        //        var initialize = function () {
        //        };
        //        initialize();
        //    }

    }
})();
