/// <reference path="reference.d.ts" />

angular.module('application', ['application.component', 'ngCookies', 'ngRoute', 'ui', 'ui.router']);
angular.module('application.component', []);

angular.module('application')
    .config([
        '$httpProvider', ($httpProvider: ng.IHttpProvider) => {
            // FIX: x-domain request fail. see: https://github.com/angular/angular.js/pull/1454.
            delete $httpProvider.defaults.headers.common["X-Requested-With"];
        }
    ])
    .config([
        '$routeProvider', '$urlRouterProvider', '$locationProvider', ($routeProvider, $urlRouterProvider, $locationProvider) => {
            $locationProvider.html5Mode(false).hashPrefix();
            $urlRouterProvider.otherwise('/');
        }
    ])
    .config([
        '$stateProvider', ($stateProvider: any) => {
            $stateProvider
                .state('index', {
                    url: '/',
                    templateUrl: 'App/Platform/app/part/platform/Index.html'
                });
        }
    ])
    .run(($rootScope: ng.IRootScopeService) => {

    });

