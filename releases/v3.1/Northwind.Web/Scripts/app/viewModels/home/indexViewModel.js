define(['kendo'],
    function(kendo) {

        var indexViewModel = new kendo.observable(
            {
                content: "",
            });

        return indexViewModel;
    });