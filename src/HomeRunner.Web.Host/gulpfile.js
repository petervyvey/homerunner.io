/// <vs AfterBuild='default' Clean='clean' />

var gulp = require('gulp');
var fs = require('fs');
var path = require('path');
var rimraf = require('gulp-rimraf');
var less = require('gulp-less');
var minifyCSS = require('gulp-minify-css');
var run = require('gulp-run');

var bootstrapFolder = 'Vendor/bootstrap/style';
var brandingFolder = 'Vendor/platform/branding';
var spaFolder = 'App';
var distFolder = 'Dist';
var nodeModulesFolder = 'node_modules';

gulp.task('watch', function () {
    gulp.watch(path.join(brandingFolder, '/**/*.less'), ['branding']);
});

gulp.task('clean', function () {
    return gulp.src([path.join(distFolder, 'branding', '*'), path.join(distFolder, 'bootstrap', '*')], { read: false })
        .pipe(rimraf({ force: true }));
});

gulp.task('bootstrap', ['clean'], function() {
    gulp.src(path.join(bootstrapFolder, 'bootstrap.less'))
        .pipe(less())
        .pipe(minifyCSS({ keepBreaks: false }))
        .pipe(gulp.dest(path.join(distFolder, 'bootstrap', 'style')));
});

gulp.task('branding', ['clean'], function () {
    var folders = fs.readdirSync(path.join(brandingFolder))
        .filter(function (file) {
            return fs.statSync(path.join(brandingFolder, file)).isDirectory();
        });

    try {
        fs.mkdirSync(path.join(distFolder));
    } catch (e) {
        if (e.code != 'EEXIST') throw e;
    }

    try {
        fs.mkdirSync(path.join(distFolder, 'branding'));
    } catch (e) {
        if (e.code != 'EEXIST') throw e;
    }

    for (var i = 0; i < folders.length; i++) {
        try {
            fs.mkdirSync(path.join(distFolder, 'branding', folders[i]));
            fs.mkdirSync(path.join(distFolder, 'branding', folders[i], 'style'));
            fs.mkdirSync(path.join(distFolder, 'branding', folders[i], 'image'));

            console.log('COPY HERE -->');

            gulp.src([path.join(brandingFolder, folders[i], 'image', '**/*')]).pipe(gulp.dest(path.join(distFolder, 'branding', folders[i], 'image')));

            gulp.src(path.join(brandingFolder, folders[i], 'style', 'theme.less'))
                .pipe(less())
                .pipe(minifyCSS({ keepBreaks: false }))
                .pipe(gulp.dest(path.join(distFolder, 'branding', folders[i], 'style')));
        } catch (e) {
            if (e.code != 'EEXIST') throw e;
        }
    }
});

gulp.task('spa', ['clean'], function() {
    var folders = fs.readdirSync(path.join(spaFolder))
        .filter(function(file) {
            return fs.statSync(path.join(spaFolder, file)).isDirectory();
        });

    for (var i = 0; i < folders.length; i++) {
        var files = fs.readdirSync(path.join(spaFolder, folders[i]));
        for (var j = 0; j < files.length; j++) {
            if (files[j] === 'gulpfile.js') {
                var cmd = new run.Command('gulp', { cwd: path.join(spaFolder, folders[i]) });
                cmd.exec();
            }
        }
    }
});

gulp.task('default', ['clean', 'branding', 'bootstrap', 'spa']);