'use strict';

var through = require('through2');
var rs = require('replacestream');
var gutil = require('gulp-util');
var PluginError = gutil.PluginError;

module.exports = function(search, replacement, options) {
  var doReplace = function(file, enc, callback) {
    if (file.isNull()) {
      return callback(null, file);
    }

    function doReplace() {
      if (file.isStream()) {
        this.emit('error', new PluginError("gulp-unicycle",
            "Streams are not supported!"));
      }

      if (file.isBuffer()) {
        file.contents = new Buffer(String(file.contents).replace(search, replacement));
        return callback(null, file);
      }
      callback(null, file);
    }
    doReplace();
  };
  return through.obj(doReplace);
};
