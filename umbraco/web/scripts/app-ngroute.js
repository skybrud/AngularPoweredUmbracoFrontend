angular.module('app',['ngRoute','ngAnimate','ngSanitize'])
    .config(['$locationProvider', '$routeProvider', function($locationProvider, $routeProvider) {
        
        $locationProvider.html5Mode(true);
        $routeProvider
            .otherwise({
                resolve:{
                    getData:['$location', '$http', function($location, $http) {
                        return $http({
                            url: '/umbraco/api/contentApi/getData/',
                            params:{
                                url:encodeURIComponent($location.$$path)
                            }
                        });
                    }]
                },
                template:'<div ng-include="ctrl.pageData.templateUrl"></div>',
                controller:['getData',function(getData) {
                    var _this = this;
                    _this.pageData = getData.data.data;
                }],
                controllerAs:'ctrl'
            });

    }]);