import { withGriffelCSSExtraction } from '@griffel/next-extraction-plugin';
import type { NextConfig } from 'next';

const nextConfig: NextConfig = {
  reactStrictMode: false,
  // experimental: {
  //   swcPlugins: [
  //     ['fluentui-next-appdir-directive', { paths: ['@griffel', '@fluentui'] }],
  //   ],
  // },
  // transpilePackages: ["@fluentui/react-components"],
  turbopack: {},
//   webpack: (config) => {
//     config.module.rules.unshift(
//       {
//         test: /\.(js|jsx)$/,
//         exclude: /node_modules/,
//         use: [
//           {
//             loader: '@griffel/webpack-loader',
//           },
//         ],
//       },
//       {
//         test: /\.(ts|tsx)$/,
//         exclude: /node_modules/,
//         use: [
//           {
//             loader: '@griffel/webpack-loader',
//             options: {
//               babelOptions: {
//                 presets: ['next/babel'],
//               },
//             },
//           },
//         ],
//       }
//     );

//     return config;
//   },
};

export default nextConfig;
