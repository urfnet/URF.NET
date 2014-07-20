'use strict';

angular.module('northwindApp.filters', []).
  filter('interpolate', ['version', function (version)
  {
      return function (text)
      {
          return String(text).replace(/\%VERSION\%/mg, version);
      };
  }]);