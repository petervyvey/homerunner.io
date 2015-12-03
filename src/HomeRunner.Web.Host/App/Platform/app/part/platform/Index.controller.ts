/// <reference path="../../core/reference.d.ts" />

module Attentia.Evolution.Home.Part.Home {
    
    export interface IIndexControllerScope extends ng.IRootScopeService {
        widgetTemplateUrlList: Array<string>;
    }

    export class IndexControllerScope {
        static $inject = ['$scope'];

        constructor($scope: any) {
            this.$scope = this.$localScope = $scope;

            this.initializeScope();
        }

        private $scope: any;
        private $localScope: IIndexControllerScope;

        private initializeScope(): void {
            this.$localScope.widgetTemplateUrlList = [
                'App/Home/app/part/widget/TaskWidget.html'
            ];
        }
    }
}

angular.module('application.component').controller('Attentia.Evolution.Home.Part.Home.IndexControllerScope', Attentia.Evolution.Home.Part.Home.IndexControllerScope);