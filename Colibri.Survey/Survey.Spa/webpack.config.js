const path = require('path');
const webpack = require('webpack');
const merge = require('webpack-merge');
const forkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const parallelUglifyPlugin = require('webpack-parallel-uglify-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

module.exports = (env) => {
    // Configuration in common to both client-side and server-side bundles
    const isDevBuild = !(env && env.prod);
    const sharedConfig = {
        stats: { modules: false, assets: false },
        context: __dirname,
        mode: isDevBuild ? 'development' : 'production',
        resolve: {
            extensions: ['.js', '.ts', 'scss', 'css'],
            modules: [
                path.resolve('./node_modules'),
                path.resolve('./ClientApp/app')
            ]
        },
        output: {
            filename: '[name].js',
            publicPath: 'dist/' // Webpack dev middleware, if enabled, handles requests for this URL prefix
        },
        module: {
            rules: [
                {
                    test: /\.ts$/, include: /ClientApp/, exclude: /node_modules/,
                    use: [
                        'cache-loader',
                        'angular2-template-loader',
                        {
                            loader: 'ng-router-loader',
                            options: { loader: 'async-import', genDir: 'compiled' }
                        },
                        {
                            loader: 'ts-loader',
                            options: { happyPackMode: true }
                        }
                    ]
                },
                { test: /\.html$/, use: 'html-loader?minimize=false' },
                { test: /\.css$/, use: ['to-string-loader', 'css-loader'] },
                { test: /\.scss$/, use: ['to-string-loader', 'css-loader', 'sass-loader'] },
                { test: /\.(jpg|png|gif)$/, use: 'file-loader' },
                { test: /\.(woff|woff2|eot|ttf|svg)$/, use: 'file-loader' }
            ]
        },
        plugins: [
            new forkTsCheckerWebpackPlugin({
                checkSyntacticErrors: true,
                workers: 2,
                async: true,
                tslint: true
            })            
        ]
    };

    // Configuration for client-side bundle suitable for running in browsers
    const clientBundleOutputDir = './wwwroot/dist';
    const clientBundleConfig = merge(sharedConfig, {
        entry: { 'main-client': './ClientApp/boot.browser.ts' },
        output: { path: path.join(__dirname, clientBundleOutputDir) },
        plugins: [
            // new BundleAnalyzerPlugin(),
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./wwwroot/dist/vendor-manifest.json')
            })
        ].concat(isDevBuild ? [
            // Plugins that apply in development builds only
            new webpack.SourceMapDevToolPlugin({
                filename: '[file].map', // Remove this line if you prefer inline source maps
                moduleFilenameTemplate: path.relative(clientBundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
            })
        ] : [
                // Plugins that apply in production builds only
                new ExtractTextPlugin('styles.css'),
                new parallelUglifyPlugin({}),
                new webpack.optimize.ModuleConcatenationPlugin()                
            ])
    });

    // Configuration for server-side (prerendering) bundle suitable for running in Node
    const serverBundleConfig = merge(sharedConfig, {
        resolve: { mainFields: ['main'] },
        entry: { 'main-server': './ClientApp/boot.server.ts' },
        target: 'node',
        plugins: [            
            new webpack.DllReferencePlugin({
                context: __dirname,
                manifest: require('./ClientApp/dist/vendor-manifest.json'),
                sourceType: 'commonjs2',
                name: './vendor'
            })
        ].concat(isDevBuild ? [] : [
            // Plugins that apply in production builds only
        ]),
        output: {
            libraryTarget: 'commonjs',
            path: path.join(__dirname, './ClientApp/dist')
        },
    });

    return [clientBundleConfig, serverBundleConfig];
};
