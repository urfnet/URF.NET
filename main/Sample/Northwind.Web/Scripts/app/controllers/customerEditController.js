'use strict';

northwindApp.controller('customerEditController',
    function ($scope, $routeParams, $location, customerDataSource)
    {
        var customerId = $routeParams.id;

        customerDataSource.filter({ field: "CustomerID", operator: "eq", value: customerId });
        $scope.customer = customerDataSource.at(0);

        $scope.save = function ()
        {
            customerDataSource.view()[0].dirty = true;
            customerDataSource.sync();
            $location.url('/customer');
        };

        $scope.cancel = function ()
        {
            $location.url('/customer');
        }
    });