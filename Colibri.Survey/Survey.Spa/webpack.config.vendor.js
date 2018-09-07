const path = require('path');
const webpack = require('webpack');
const merge = require('webpack-merge');
const ParallelUglifyPlugin = require('webpack-parallel-uglify-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

const vendorModules = [
    // Angular core
    '@angular/common',
    '@angular/common/http',
    '@angular/http',
    '@angular/router',
    '@angular/animations',
    '@angular/platform-browser',
    '@angular/platform-browser-dynamic',
    '@angular/platform-browser/animations',
    // Pollyfils
    'core-js/es6/symbol',
    'core-js/es6/object',
    'core-js/es6/function',
    'core-js/es6/parse-int',
    'core-js/es6/parse-float',
    'core-js/es6/number',
    'core-js/es6/math',
    'core-js/es6/string',
    'core-js/es6/date',
    'core-js/es6/array',
    'core-js/es6/regexp',
    'core-js/es6/map',
    'core-js/es6/set',
    'core-js/es6/reflect',
    'core-js/es7/reflect',
    
    // RxJS
    // 'rxjs/Observable',
    // 'rxjs/observable/forkJoin',
    // 'rxjs/observable/empty',
    // 'rxjs/observable/timer',
    // 'rxjs/Subject',
    // 'rxjs/Subscription',
    // 'rxjs/add/operator/filter',
    // 'rxjs/add/operator/catch',
    // 'rxjs/add/operator/first',
    // 'rxjs/add/observable/throw',
    // 'rxjs/add/operator/skip',
    // 'rxjs/add/operator/mergeMap',

    // Lodash
    // 'lodash/mapValues',
    'lodash/groupBy',
    'lodash/omit',
    // Etc
    '@ngx-translate/core',
    'bootstrap',
    'moment',
    'jquery',
    'zone.js'
];

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const sharedConfig = {
        stats: { modules: false },
        resolve: { extensions: ['.js'] },
        mode: isDevBuild ? 'development' : 'production',
        module: {
            rules: [
                { test: /\.(png|gif|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
                { test: /\.css$/, use: ['to-string-loader', 'css-loader'] },
                { test: require.resolve('moment'), use: [{ loader: 'expose-loader', options: 'moment' }] },                
                { test: require.resolve('jquery'), use: [{ loader: 'expose-loader', options: '$' }, { loader: 'expose-loader', options: 'jQuery' }] },
            ]
        },
        output: {
            publicPath: 'dist/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            new webpack.ContextReplacementPlugin(/\@angular(\\|\/)core(\\|\/)esm5/, path.resolve(__dirname, "./ClientApp")), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.ContextReplacementPlugin(/\@angular\b.*\b(bundles|linker)/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/11580
            new webpack.ContextReplacementPlugin(/angular(\\|\/)core(\\|\/)@angular/, path.join(__dirname, './ClientApp')), // Workaround for https://github.com/angular/angular/issues/14898
            new webpack.ContextReplacementPlugin(/moment[\/\\]locale$/, /en|sv/), //MomentJS locales
            new webpack.IgnorePlugin(/^vertx$/), // Workaround for https://github.com/stefanpenner/es6-promise/issues/100
        ]
    };

    const clientBundleConfig = merge(sharedConfig, {
        entry: {
            vendor: vendorModules
        },
        output: { path: path.join(__dirname, 'wwwroot', 'dist') },
        plugins: [
            // new BundleAnalyzerPlugin(),
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            }),
            new webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery"
            })
        ].concat(isDevBuild ? [
        ] : [
                new ExtractTextPlugin('vendor-styles.css'),
                new ParallelUglifyPlugin({}),
            ])
    });

    const serverBundleConfig = merge(sharedConfig, {
        target: 'node',
        resolve: { mainFields: ['main'] },
        entry: { vendor: vendorModules.concat(['aspnet-prerendering']) },
        output: {
            path: path.join(__dirname, 'ClientApp', 'dist'),
            libraryTarget: 'commonjs2',
        },
        plugins: [
            new webpack.DllPlugin({
                path: path.join(__dirname, 'ClientApp', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            })
        ]
    });

    return [clientBundleConfig, serverBundleConfig];
};
