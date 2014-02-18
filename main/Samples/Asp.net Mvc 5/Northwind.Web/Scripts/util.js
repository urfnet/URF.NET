define([],
    function() {

        var util;

        util = {
            getId:
                function() {
                    var array = window.location.href.split('/');
                    var id = array[array.length - 1];
                    return id;
                }
        };

        return util;

    });