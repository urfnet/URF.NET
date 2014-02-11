define(['kendo', 'customerModel'],
    function(kendo, customerModel) {
        var crudServiceBaseUrl = "/odata/Customer";

        var customerDatasource = new kendo.data.DataSource({
            type: "odata",
            transport: {
                read: {
                    async: false,
                    url: crudServiceBaseUrl,
                    dataType: "json"
                },
                update: {
                    url: function(data) {
                        return crudServiceBaseUrl + "(" + data.CustomerID + ")";
                    },
                    dataType: "json"
                },
                destroy: {
                    url: function(data) {
                        return crudServiceBaseUrl + "(" + data.CustomerID + ")";
                    },
                    dataType: "json"
                }
            },
            batch: false,
            serverPaging: true,
            serverSorting: true,
            serverFiltering: true,
            pageSize: 10,
            schema: {
                data: function(data) {
                    return data.value;
                },
                total: function(data) {
                    return data["odata.count"];
                },
                errors: function(data) {
                },
                model: customerModel
            },
            error: function(e) {
                if (e.errors) {
                    var message = "Errors:\n";
                    $.each(e.errors, function(key, value) {
                        if ('errors' in value) {
                            $.each(value.errors, function() {
                                message += this + "\n";
                            });
                        }
                    });
                    alert(message);
                }
            }
        });

        return customerDatasource;

    });