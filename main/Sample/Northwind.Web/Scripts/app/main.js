require.config({
    paths: {
        //packages
        'jquery': '/scripts/jquery-2.1.0.min',
        'jquerymigrate': '/scripts/jquery-migrate-1.2.1.min',

        'kendo': '/scripts/kendo/2013.3.1119/kendo.web.min',
        //'kendo': '/scripts/kendo/2014.1.318/kendo.web.min',

        'text': '/scripts/text',
        'router': '/scripts/app/router',
        //models
        'customerModel': '/scripts/app/models/customerModel',
        //viewModels
        'customer-indexViewModel': '/scripts/app/viewmodels/customer/indexViewModel',
        'customer-editViewModel': '/scripts/app/viewmodels/customer/editViewModel',
        //datasources
        'customerDatasource': '/scripts/app/datasources/customerDatasource',
        // utils
        'util': '/scripts/util'
    },
    shim: {
        'kendo': ['jquery', 'jquerymigrate']
    },
    priority: ['text', 'router', 'app'],
    jquery: '2.1.0',
    waitSeconds: 30
});

require([
        'app'
    ], function(app) {
        app.initialize();
    });