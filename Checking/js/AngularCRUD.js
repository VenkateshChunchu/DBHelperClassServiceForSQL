/// <reference path="angular.min.js" />
var myApp = angular.module("CRUDModule", []).controller("crudController", function ($scope, $http, $log) {
    $scope.message = "Hi .....";
    $http(
        {
            method: 'POST',    
            headers: {    
                'Content-Type': 'application/json; charset=utf-8'    
            },    
            url: "http://localhost:27315/NewsArticlesService.svc/newsarticles",
            data: {}    
        }).then(function (response) {
            $scope.productsList = response.data.records;
        }, function (response) {
            alert("error");
            $log.info(response);
        });
    //$scope.productsList = [
    //    { id: 01, name: 'Venkatesh', cost: 200, quantity: 1 },
    //    { id: 02, name: 'Ramesh', cost: 250, quantity: 1 },
    //    { id: 03, name: 'Ramu', cost: 300, quantity: 2 },
    //    { id: 04, name: 'Ravali', cost: 210, quantity: 1 }
    //];
    $scope.DeleteItem = function (id) {
        var index = GetSelectedElementIndex(id);
        if (index != -1)
        {
            if (confirm("Surely want to delete this item?"))
                $scope.productsList.splice(index, 1);
        }
        else
            alert("No Record Found");
    };
    $scope.EditItem = function (id) {
        var index = GetSelectedElementIndex(id);
        var product = $scope.productsList[index];
        $scope.id = product.id;
        $scope.name = product.name;
        $scope.cost = product.cost;
        $scope.quantity = product.quantity;
    };
    $scope.AddItem = function myfunction() {
        $scope.productsList.push(
            { id: $scope.id, name: $scope.name, cost: $scope.cost, quantity: $scope.quantity }
            );
        ClearFields();
    };
    function GetSelectedElementIndex(id) {
        for (var i = 0; i < $scope.productsList.length; i++)
            if ($scope.productsList[i].id == id)
                return i;
        return -1;
        
    };
    function ClearFields() {
        $scope.id = '';
        $scope.name = '';
        $scope.cost = '';
        $scope.quantity = '';
    }
})