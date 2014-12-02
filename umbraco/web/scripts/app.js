angular.module('app', ['ui.router', 'ngAnimate', 'ngSanitize'])
    .config(['$locationProvider', '$stateProvider', function($locationProvider, $stateProvider) {

        $locationProvider.html5Mode(true);
        $stateProvider
            .state('all', {
                url: '*path',
                resolve: {
                    getData: ['$location', '$http', function($location, $http) {
                        return $http({
                            url: '/umbraco/api/contentApi/getData/',
                            params: {
                                url: encodeURIComponent($location.$$path)
                            }
                        });
                    }]
                },
                template: '<div ng-include="ctrl.pageData.templateUrl"></div>',
                controller: ['getData', function(getData) {
                    var _this = this;
                    _this.pageData = getData.data.data;
                }],
                controllerAs: 'ctrl'
            });
    }])
    .animation('.view', function() {
        return {
            enter: function (element, done) {
                TweenLite.from(element[0], 1, {
                    opacity: 0,
                    onComplete: done
                });
            },
            leave: function(element, done) {
                TweenLite.set(element[0], {
                    position: 'absolute',
                    top: 0
                });
                 TweenLite.to(element[0], 1, {
                    opacity: 0,
                    onComplete: done
                });
            }
        }
	});