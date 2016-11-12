import gulp from 'gulp'

const dirs = {
    css: './Src/Styles/**/*.css',
    js: './Src/Scripts/**/*.js'
}

gulp.task('watch', () => {
    gulp.watch([dirs.css], ['styles'])
    gulp.watch([dirs.js], ['scripts'])
})
