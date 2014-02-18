
define(['kendo'],
    function(kendo) {
        var router = new kendo.Router(),
            layout = new kendo.Layout("<div id='content'></div>");

        layout.render($("#app"));

        router.route("/", function() {
            require(['text!/home/index'], function(view) {
                loadView(null, view);
            });
        });

        router.route("/home/index", function() {
            require(['text!/home/index'], function(view) {
                loadView(null, view);
            });
        });

        router.route("/home/about", function() {
            require(['text!/home/about'], function(view) {
                loadView(null, view);
            });
        });

        router.route("/home/contact", function() {
            require(['text!/home/contact'], function(view) {
                loadView(null, view);
            });
        });

        router.route("/customer/index", function() {
            require(['customer-indexViewModel', 'text!/customer/index'], function(viewModel, view) {
                loadView(viewModel, view, function() {
                    kendo.bind($("#grid").find(".k-grid-toolbar"), viewModel);
                });
            });
        });

        router.route("/customer/edit/:id", function() {
            require(['customer-editViewModel', 'text!/customer/edit'], function(viewModel, view) {
                loadView(viewModel.loadData(), view);
            });
        });

        var loadView = function(viewModel, view, delegate) {
            var kendoView = new kendo.View(view, { model: viewModel });
            kendo.fx($("#content")).slideInRight().reverse().then(function() {
                layout.showIn("#content", kendoView);

                if (delegate != undefined)
                    delegate();

                kendo.fx($("#content")).slideInRight().play();
            });
        };

        return router;
    });