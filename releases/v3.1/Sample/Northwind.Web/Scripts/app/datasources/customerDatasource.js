define(['kendo', 'customerModel'],
    function (kendo, customerModel) {
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
                    url: function (data) {
                        return crudServiceBaseUrl + "(" + data.CustomerID + ")";
                    },
                    dataType: "json"
                },
                destroy: {
                    url: function (data) {
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
                data: function (data) { return data.value; },
                total: function (data) { return data["odata.count"]; },
                model: customerModel
            },
            error: function (e) {
                alert(e.xhr.responseJSON["odata.error"]
                    .innererror.internalexception.internalexception.message);
            }
        });

        return customerDatasource;

    });