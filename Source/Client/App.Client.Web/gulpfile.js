/// <binding ProjectOpened='watch' />
// Include plug-ins
var argv = require('yargs').argv,
    isProduction = argv.env === 'prod',
    addStream = require('add-stream'),
    del = require('del'),
    gulp = require('gulp'),
	angularFilesort = require('gulp-angular-filesort'),
	angularTemplateCache = require('gulp-angular-templatecache'),
	autoprefixer = require('gulp-autoprefixer'),
	concat = require('gulp-concat'),
    gulpIf = require('gulp-if'),
	inject = require('gulp-inject'),
	jshint = require('gulp-jshint'),
    less = require('gulp-less'),
    livereload = require('gulp-livereload'),
	minifyCSS = require('gulp-minify-css'),
    minifyHTML = require('gulp-minify-html'),
	rename = require('gulp-rename'),
	uglify = require('gulp-uglify');


// File paths
var config = {
    vendorJsSrc: [
        'Scripts/angular.js',
        'Scripts/angular-animate.js',
        'Scripts/angular-cookies.js',
        'Scripts/angular-route.js',
        'Scripts/jquery-2.2.4.js',
        'Scripts/loading-bar.js',
        'Scripts/bootstrap.js',
        'Scripts/toastr.js'
    ],
    vendorCssSrc: [
        'content/bootstrap.css',
        'content/bootstrap-theme.css',
        'content/toastr.css'
    ],
    appJsSrc: ['app/**/*.js', '!app/build/*'],
    appLessSrc: ['app/**/*.less', '!app/build/*'],
    appTemplatesHtml: 'app/views/*.html',
    appIndexHtml: 'index-template.html'
}


// For browser caching
function getStamp() {
    var myDate = new Date(),
        myYear = myDate.getFullYear().toString(),
        myMonth = ('0' + (myDate.getMonth() + 1)).slice(-2),
        myDay = ('0' + myDate.getDate()).slice(-2),
        mySeconds = myDate.getSeconds().toString(),
        myFullDate = myYear + myMonth + myDay + mySeconds;

    return myFullDate;
};

// For angular templates
function prepareTemplates() {
    return gulp.src(config.appTemplatesHtml)
        .pipe(angularTemplateCache());
};

// Minify, prefix and concatenate CSS
gulp.task('styles', function () {
    del.sync(['app/build/css*']);

    var styles = config.vendorCssSrc.concat(config.appLessSrc);

    return gulp.src(styles)
        .pipe(less())
        .pipe(gulpIf(isProduction, minifyCSS()))
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
        .pipe(concat('css' + (isProduction ? getStamp() + '.min' : '') + '.css', { newLine: '' }))
        .pipe(gulp.dest('app/build'))
        .pipe(livereload());
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
		.pipe(concat('vendorjs' + (isProduction ? getStamp() + '.min' : '') + '.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'));
});

// Combine and minify all JS files from the app folder
gulp.task('scripts', function () {
    del.sync(['app/build/js*']);

    return gulp.src(config.appJsSrc)
		.pipe(angularFilesort())
		.pipe(gulpIf(isProduction, uglify()))
        .pipe(addStream.obj(prepareTemplates()))
		.pipe(concat('js' + (isProduction ? getStamp() + '.min' : '') + '.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'))
        .pipe(livereload());
});

// Inject minified files into new HTML
gulp.task('html', ['styles', 'scripts'], function () {
    del.sync(['index.html']);

    var target = gulp.src(config.appIndexHtml),
        vendorSources = gulp.src(['app/build/vendorjs*'], { read: false }),
        appSources = gulp.src(['app/build/js*', 'app/build/css*'], { read: false });

    return target
        .pipe(inject(vendorSources, { starttag: '<!-- inject:vendors:{{ext}} -->' }))
        .pipe(inject(appSources))
		.pipe(gulpIf(isProduction, minifyHTML({ conditionals: true })))
		.pipe(rename('index.html'))
		.pipe(gulp.dest('./'));
});

// Watch for changes
gulp.task('watch', ['lint', 'styles', 'vendors', 'scripts', 'html'], function () {
    livereload.listen();

    gulp.watch(config.appLessSrc, ['styles', 'html']);
    gulp.watch(config.appJsSrc, ['lint', 'scripts', 'html']);
    gulp.watch(config.appTemplatesHtml, ['lint', 'scripts', 'html']);
    gulp.watch(config.appIndexHtml, ['html']);
});

// Set default tasks
gulp.task('default', ['lint', 'styles', 'vendors', 'scripts', 'html'], function () { });
