/// <reference path="angular.min.js" />
/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.8/angular-route.min.js"/>
var myApp = angular.module("myModule", [])
                    .controller("MyController", function($scope) {
                        $scope.cars = [
                        { model: 'Mustang', cost: 500, country: 'USA' },
                        { model: 'Suziki', cost: 700, country: 'india' },
                        { model: 'Nano', cost: 200, country: 'india' }];
                        $scope.names = ["Venkatesh", "naresh", "Suresh", "Santhosh"];
                        $scope.msg = "Hi this is Venkaetsh";
                    })
                    .controller("MyWCFServiceControl", function ($scope, $http, $log) {
                        $scope.AllArticles = "Hi this is Venkaetsh";
                            $http({
                            method: 'Get',
                            url: 'http://localhost:27315/NewsArticlesService.svc/NewsArticles',
                            //Content:''
                        }).then(function (response) {
                            //on Success
                            $scope.myData = response.data.records;
                        }, function (response) {
                            // on Error
                            $scope.error = "Something Went Wrong"
                            $log.info = response;
                        });
                    });