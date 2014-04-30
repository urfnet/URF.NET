'use strict';

northwindApp.factory('customerModel', function ()
{
    return kendo.data.Model.define({
        id: "CustomerID",
        fields: {
            CustomerID: { type: "string", editable: false, nullable: false },
            CompanyName: { title: "Company", type: "string" },
            ContactName: { title: "Contact", type: "string" },
            ContactTitle: { title: "Title", type: "string" },
            Address: { type: "string" },
            City: { type: "string" },
            PostalCode: { type: "string" },
            Country: { type: "string" },
            Phone: { type: "string" },
            Fax: { type: "string" },
            State: { type: "string" }
        }
    });
});