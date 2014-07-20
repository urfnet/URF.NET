'use strict';

northwindApp.controller('customerController',
    function ($scope, $rootScope, $location, customerDataSource)
    {
        customerDataSource.filter({}); // reset filter on dataSource everytime view is loaded

        var onClick = function (event, delegate)
        {
            var grid = event.grid;
            var selectedRow = grid.select();
            var dataItem = grid.dataItem(selectedRow);

            if (selectedRow.length > 0)
            {
                delegate(grid, selectedRow, dataItem);
            }
            else
            {
                alert("Please select a row.");
            }
        };

        $scope.toolbarTemplate = kendo.template($("#toolbar").html());

        $scope.save = function (e)
        {
            onClick(e, function (grid)
            {
                grid.saveRow();
                $(".toolbar").toggle();
            });
        };

        $scope.cancel = function (e)
        {
            onClick(e, function (grid)
            {
                grid.cancelRow();
                $(".toolbar").toggle();
            });
        },

        $scope.details = function (e)
        {
            onClick(e, function (grid, row, dataItem)
            {
                $location.url('/customer/edit/' + dataItem.CustomerID);
            });
        },

        $scope.edit = function (e)
        {
            onClick(e, function (grid, row)
            {
                grid.editRow(row);
                $(".toolbar").toggle();
            });
        },

        $scope.destroy = function (e)
        {
            onClick(e, function (grid, row, dataItem)
            {
                grid.dataSource.remove(dataItem);
                grid.dataSource.sync();
            });
        },

        $scope.onChange = function (e)
        {
            var grid = e.sender;

            $rootScope.lastSelectedDataItem = grid.dataItem(grid.select());
        },

        $scope.dataSource = customerDataSource;

        $scope.onDataBound = function (e)
        {
            // check if there was a row that was selected
            if ($rootScope.lastSelectedDataItem == null)
            {
                return;
            }

            var view = this.dataSource.view(); // get all the rows

            for (var i = 0; i < view.length; i++)
            {
                // iterate through rows
                if (view[i].CustomerID == $rootScope.lastSelectedDataItem.CustomerID)
                {
                    // find row with the lastSelectedProductd
                    var grid = e.sender; // get the grid

                    grid.select(grid.table.find("tr[data-uid='" + view[i].uid + "']")); // set the selected row
                    break;
                }
            }
        };
    });