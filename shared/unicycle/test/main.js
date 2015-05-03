"use strict";

var replacePlugin = require('../');
var fs = require('fs');
var es = require('event-stream');
var should = require('should');
var File = require('vinyl');

describe('replacePlugin()', function() {
  it('should replace string on a stream', function(done) {
    var file = new File({
      path: 'test/fixtures/helloworld.txt',
      cwd: 'test/',
      base: 'test/fixtures',
      contents: fs.createReadStream('test/fixtures/helloworld.txt')
    });

    var stream = replacePlugin('world', 'person');
    stream.on('data', function(newFile) {
      should.exist(newFile);
      should.exist(newFile.contents);

      newFile.contents.pipe(es.wait(function(err, data) {
        should.not.exist(err);
        var expected = fs.readFileSync('test/expected/helloworld.txt', 'utf8');
        data.should.equal(expected);
        done();
      }));
    });

    stream.write(file);
    stream.end();
  });
});
