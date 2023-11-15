/** @type {import('tailwindcss').Config} */
const colors = require('tailwindcss/colors');

module.exports = {
    content: ["./**/*.{razor,html,cshtml}"],
    theme: {
        colors: {
            "bleh": "#d2bf9e",
            "white": "#ffffff",
            "stone": colors.stone,
        },
        extend: {},
    },
    plugins: [],
}

