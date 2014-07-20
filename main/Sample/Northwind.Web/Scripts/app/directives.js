'use strict';

angular.module('northwindApp.directives', []).
  directive('appVersion', ['version', function (version)
  {
      return function (scope, elm, attrs)
      {
          elm.text(version);
      };
  }]);