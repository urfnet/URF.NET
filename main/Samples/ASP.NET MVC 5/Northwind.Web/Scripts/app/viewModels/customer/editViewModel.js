
define(['customerDatasource', 'customerModel', 'util', 'router'],
    function(customerDatasource, customerModel, util, router) {

        var editViewModel = new kendo.data.ObservableObject({
            loadData: function() {
                var viewModel = new kendo.data.ObservableObject({
                    saveCustomer: function(s) {
                        customerDatasource.sync();
                        customerDatasource.filter({});
                        router.navigate('/customer/index');
                    },
                    cancel: function(s) {
                        customerDatasource.filter({});
                        router.navigate('/customer/index');
                    }
                });

                customerDatasource.filter({
                    field: "CustomerID",
                    operator: "equals",
                    value: util.getId()
                });

                customerDatasource.fetch(function() {
                    if (customerDatasource.view().length > 0) {
                        viewModel.set("Customer", customerDatasource.at(0));
                    } else
                        viewModel.set("Customer", new customerModel());
                });
                return viewModel;
            },
        });

        return editViewModel;
    });