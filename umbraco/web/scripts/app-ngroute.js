/**
 * This does exactly the same as app.js but uses ngRoute instead.
 * 
 * You will need to change the <div ui-view> in the startview to
 * be <div ng-view> as well. 
 */
 
angular.module('app',['ngRoute','ngAnimate','ngSanitize'])
    .config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
        
        $locationProvider.html5Mode(true);
        $routeProvider
            /* the otherwise route matches all other routes - since we have defined no other, this means any route */
            .otherwise({
                resolve:{
                    getData:['$location', '$http', function($location, $http) {
                        return $http({
                            url: '/umbraco/api/contentApi/getData/',
                            params:{
                                /* When using ngRoute we need $location to read the current path */
                                url:encodeURIComponent($location.$$path)
                            }
                        });
                    }]
                },
                template:'<div ng-include="ctrl.pageData.templateUrl"></div>',
                controller: ['getData','$rootScope', function(getData,$rootScope) {
                    var _this = this;
                    _this.pageData = getData.data.data;
                    $rootScope.pageTitle = getData.data.data.name;
                }],
                controllerAs:'ctrl'
            });

    }]);