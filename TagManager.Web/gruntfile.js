/// <binding ProjectOpened='default' />
'use strict';

module.exports = function (grunt) {
   // load all grunt tasks
   grunt.loadNpmTasks('grunt-contrib-less');
   grunt.loadNpmTasks('grunt-contrib-watch');

   grunt.initConfig({
      watch: {
         files: "App/**/*.less",
         tasks: ["less"]
      },
      // "less"-task configuration
      less: {
         // production config is also available
         development: {
            options: {
               // Specifies directories to scan for @import directives when parsing. 
               // Default value is the directory of the source, which is probably what you want.
               paths: ["App"],
            },
            files: [{
               expand: true,
               cwd: "App",
               src: ["**/*.less"],
               dest: "App",
               ext: ".css"
            }]
         },
      },
   });
   // the default task (running "grunt" in console) is "watch"
   grunt.registerTask('default', ['watch']);
};