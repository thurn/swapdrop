'use strict';

var through = require('through2');
var rs = require('replacestream');

module.exports = function(search, replacement, options) {
  var doReplace = function(file, enc, callback) {
    if (file.isNull()) {
      return callback(null, file);
    }

    function doReplace() {
      file.contents = file.contents.pipe(rs(search, replacement));
      return callback(null, file);
      callback(null, file);
    }

    doReplace();
  };

  return through.obj(doReplace);
};
