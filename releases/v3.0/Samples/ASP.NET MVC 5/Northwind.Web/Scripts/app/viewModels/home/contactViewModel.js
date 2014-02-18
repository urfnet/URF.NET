define(['kendo'],
    function(kendo) {

        var contactViewModel = new kendo.observable(
            {
                content: "",
            });

        return contactViewModel;
    });