define([
        'router'
    ], function(router) {
        var initialize = function() {
            router.start();
        };

        return {
            initialize: initialize
        };
    });