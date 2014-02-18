
define(['kendo', 'customerDatasource', 'router'],
    function(kendo, customerDatasource, router) {
        var lastSelectedDataItem = null;

        var onClick = function(event, delegate) {
            event.preventDefault();
            var grid = $("#grid").data("kendoGrid");
            var selectedRow = grid.select();
            var dataItem = grid.dataItem(selectedRow);
            if (selectedRow.length > 0)
                delegate(grid, selectedRow, dataItem);
            else
                alert("Please select a row.");
        };

        var indexViewModel = new kendo.data.ObservableObject({
            save: function(event) {
                onClick(event, function(grid) {
                    grid.saveRow();
                    $(".toolbar").toggle();
                });
            },

            cancel: function(event) {
                onClick(event, function(grid) {
                    grid.cancelRow();
                    $(".toolbar").toggle();
                });
            },

            details: function(event) {
                onClick(event, function(grid, row, dataItem) {
                    router.navigate('/customer/edit/' + dataItem.CustomerID);
                });
            },

            edit: function(event) {
                onClick(event, function(grid, row) {
                    grid.editRow(row);
                    $(".toolbar").toggle();
                });
            },

            destroy: function(event) {
                onClick(event, function(grid, row, dataItem) {
                    grid.dataSource.remove(dataItem);
                    grid.dataSource.sync();
                });
            },

            onChange: function(arg) {
                var grid = arg.sender;
                lastSelectedDataItem = grid.dataItem(grid.select());
            },

            dataSource: customerDatasource,

            onDataBound: function(arg) {
                if (lastSelectedDataItem == null) return; // check if there was a row that was selected
                var view = this.dataSource.view(); // get all the rows
                for (var i = 0; i < view.length; i++) { // iterate through rows
                    if (view[i].CustomerID == lastSelectedDataItem.CustomerID) { // find row with the lastSelectedProductd
                        var grid = arg.sender; // get the grid
                        grid.select(grid.table.find("tr[data-uid='" + view[i].uid + "']")); // set the selected row
                        break;
                    }
                }
            },
        });

        return indexViewModel;

    });