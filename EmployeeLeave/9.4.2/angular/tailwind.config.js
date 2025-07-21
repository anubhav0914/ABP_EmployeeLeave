/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"
  ],
  safelist: [
  'bg-blue-600', 'hover:bg-blue-700', 'text-white', // Add all dynamic classes used
],
  theme: {
    extend: {},
  },
  plugins: [],
}
