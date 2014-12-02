/*
 * Example app.js for a angular-power one-pager
 * Made by @filipbech for the 24days.in/umbraco 
 * christmas calendar...
 *
 */

angular.module('app', ['ui.router', 'ngAnimate', 'ngSanitize'])
    .config(['$locationProvider', '$stateProvider', function($locationProvider, $stateProvider) {

        // This removes the # in browsers that support it (so we have real urls)
        $locationProvider.html5Mode(true);

        $stateProvider
            .state('all', {
                /* This matches any url, and exposes the path to $stateParams with the name myPath */
                url:'*myPath', 
                resolve: {
                    getData: ['$stateParams', '$http', function($stateParams, $http) {
                        return $http({
                            url: '/umbraco/api/contentApi/getData/',
                            params: {
                                /* Send the current path in the querystring with the key 'url' to the api */
                                url: encodeURIComponent($stateParams.myPath)
                            }
                        });
                    }]
                },
                template: '<div ng-include="ctrl.pageData.templateUrl"></div>',
                controller: ['getData','$rootScope', function(getData,$rootScope) {
                    var _this = this;
                    /* We assign the api-reponse to the instance of the controller, so it's accessible from the view */
                    _this.pageData = getData.data.data;
                    $rootScope.pageTitle = getData.data.data.name;
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