define(['kendo'],
    function(kendo) {

        var aboutViewModel = new kendo.observable(
            {
                content: "",
            });

        return aboutViewModel;
    });