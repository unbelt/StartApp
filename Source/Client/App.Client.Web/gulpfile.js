// Include plug-ins
var argv = require('yargs').argv,
    isProduction = argv.env === 'prod',
    gulp = require('gulp'),
    gulpIf = require('gulp-if'),
	jshint = require('gulp-jshint'),
	concat = require('gulp-concat'),
	minifyCSS = require('gulp-minify-css'),
	autoprefixer = require('gulp-autoprefixer'),
	uglify = require('gulp-uglify'),
	rename = require('gulp-rename'),
	inject = require('gulp-inject'),
	minifyHTML = require('gulp-minify-html'),
	del = require('del'),
    addStream = require('add-stream'),
	angularFilesort = require('gulp-angular-filesort'),
	angularTemplateCache = require('gulp-angular-templatecache');

// File paths
var config = {
    vendorJsSrc: [
        'Scripts/jquery-2.2.0.js',
        'Scripts/bootstrap.js',
        'Scripts/toastr.js',
        'Scripts/scripts.js',
        'Scripts/angular.js',
        'Scripts/angular-route.js',
        'Scripts/angular-cookies.js',
        'Scripts/angular-animate.js',
        'Scripts/ckeditor.js'
    ],
    vendorCssSrc: [
        'content/bootstrap.css',
        'content/bootstrap-theme.css',
        'content/toastr.css'
    ],
    appJsSrc: ['app/**/*.js', '!app/build/*'],
    appCssSrc: ['content/styles.css'],
    appTemplatesHtml: 'app/views/*.html',
    appIndexHtml: 'index-template.html'
}

// For browser caching
var getStamp = function () {
    var myDate = new Date();

    var myYear = myDate.getFullYear().toString();
    var myMonth = ('0' + (myDate.getMonth() + 1)).slice(-2);
    var myDay = ('0' + myDate.getDate()).slice(-2);
    var mySeconds = myDate.getSeconds().toString();

    var myFullDate = myYear + myMonth + myDay + mySeconds;

    return myFullDate;
};

// For angular templates
var prepareTemplates = function () {
    return gulp.src(config.appTemplatesHtml)
        .pipe(angularTemplateCache());
};

// Minify, prefix and concatenate CSS
gulp.task('css', function () {
    del.sync(['app/build/css*']);

    var css = config.vendorCssSrc.concat(config.appCssSrc);

    return gulp.src(css)
        .pipe(gulpIf(isProduction, minifyCSS()))
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
        .pipe(concat('css' + (isProduction ? getStamp() : '') + '.min.css', { newLine: '' }))
        .pipe(gulp.dest('app/build'));
});

// Lint Task
gulp.task('lint', function () {
    return gulp.src(config.appJsSrc)
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});

// Combine and minify all library JS files
gulp.task('vendors', function () {
    del.sync(['app/build/vendorjs*']);

    return gulp.src(config.vendorJsSrc)
		.pipe(gulpIf(isProduction, uglify()))
		.pipe(concat('vendorjs' + (isProduction ? getStamp() : '') + '.min.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'));
});

// Combine and minify all JS files from the app folder
gulp.task('scripts', function () {
    del.sync(['app/build/js*']);

    return gulp.src(config.appJsSrc)
		.pipe(angularFilesort())
		.pipe(gulpIf(isProduction, uglify()))
        .pipe(addStream.obj(prepareTemplates()))
		.pipe(concat('js' + (isProduction ? getStamp() : '') + '.min.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'));
});

// Inject minified files into new HTML
gulp.task('html', ['css', 'scripts'], function () {
    del.sync(['index.html']);
    var target = gulp.src(config.appIndexHtml);
    var vendorSources = gulp.src(['app/build/vendorjs*'], { read: false });
    var appSources = gulp.src(['app/build/js*', 'app/build/css*'], { read: false });

    return target
        .pipe(inject(vendorSources, { starttag: '<!-- inject:vendors:{{ext}} -->' }))
        .pipe(inject(appSources))
		.pipe(gulpIf(isProduction, minifyHTML({ conditionals: true })))
		.pipe(rename('index.html'))
		.pipe(gulp.dest('./'));
});

// Watch for changes
gulp.task('watch', ['lint', 'css', 'vendors', 'scripts', 'html'], function () {
    gulp.watch(config.appCssSrc, ['css', 'html']);
    gulp.watch(config.appJsSrc, ['lint', 'scripts', 'html']);
    gulp.watch(config.appTemplatesHtml, ['lint', 'scripts', 'html']);
    gulp.watch(config.appIndexHtml, ['html']);
});

// Set default tasks
gulp.task('default', ['lint', 'css', 'vendors', 'scripts', 'html'], function () { });