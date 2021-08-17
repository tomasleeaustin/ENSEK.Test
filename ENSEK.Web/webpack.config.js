const webpack = require("webpack");
const path = require("path");

module.exports = {
    mode: "development",
    entry: {
        "base": [
            "@babel/polyfill",
            "./Scripts/site.js"
        ],
        "react/ensekAdminApp": "./Scripts/react/ensekAdminApp.js",
    },
    output: {
        filename: 'js/[name].min.js',
        path: path.join(__dirname, './wwwroot')
    },
    devtool: "inline-source-map",
    module: {
        rules: [
            {
                test: /\.(js)$/,
                exclude: /node_modules/,
                use: ["babel-loader"]
            },
            {
                test: /\.(sc|c)ss$/,
                use: [
                    {
                        loader: "style-loader"
                    },
                    {
                        loader: "css-loader"
                    },
                    {
                        loader: "sass-loader",
                        options: {
                            sourceMap: true
                        }
                    }
                ]
            }
        ]
    }
};